using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace YXCell
{
    /// <summary>
    /// 下载器 工具类
    /// </summary>
    public class Downloader : Singleton<Downloader>
    {
        /// <summary>
        /// 根据模块的配置，下载对应的模块
        /// </summary>
        /// <param name="moduleConfig">模块的配置对象</param>
        public async Task Download(ModuleConfig moduleConfig)
        {
            string updatePath = GetUpdatePath();

            string configUrl = GetServerUrl(moduleConfig, moduleConfig.moduleName.ToLower() + ".json");

            UnityWebRequest request = UnityWebRequest.Get(configUrl);

            request.downloadHandler = new DownloadHandlerFile($"{updatePath}/{moduleConfig.moduleName}_temp.json");

            YXUtils.EditorLogNormal($"{configUrl}下载到本地路径: {updatePath}");

            await request.SendWebRequest();

            if (string.IsNullOrEmpty(request.error) == false)
            {
                YXUtils.EditorLogError($"下载模块{moduleConfig.moduleName}的配置文件: {request.error}");

                bool result = await ShowMessageBox($"网络异常", "继续下载", "退出游戏");

                if (result == false)
                {
                    YXUtils.Quit();

                    return;
                }

                await Download(moduleConfig);

                return;
            }

            Tuple<List<BundleInfo>, BundleInfo[]> tuple = await GetDownloadList(moduleConfig.moduleName);

            List<BundleInfo> downloadList = tuple.Item1;

            BundleInfo[] removeList = tuple.Item2;

            long downloadSize = CaculateSize(downloadList);

            //YXUtils.EditorLogNormal($"下载大小: {downloadSize}");

            if (downloadSize == 0)
            {
                Clear(moduleConfig, removeList);

                return;
            }

            bool boxResult = await ShowMessageBox($"{moduleConfig.moduleName}发现新版本, 版号为{moduleConfig.moduleVersion}\n需要下载热更包,大小为: {SizeToString(downloadSize)}", "开始下载", "退出游戏");

            if (boxResult == false)
            {
                YXUtils.Quit();

                return;
            }

            await ExecuteDownload(moduleConfig, downloadList);

            Clear(moduleConfig, removeList);

            return;
        }

        private long CaculateSize(List<BundleInfo> downloadList)
        {
            long totalSize = 0;

            foreach (BundleInfo bundleInfo in downloadList)
            {
                totalSize += bundleInfo.size;
            }

            return totalSize;
        }

        private async Task ExecuteDownload(ModuleConfig moduleConfig, List<BundleInfo> bundleList)
        {
            while (bundleList.Count > 0)
            {
                BundleInfo bundleInfo = bundleList[bundleList.Count - 1];

                UnityWebRequest request = UnityWebRequest.Get(GetServerUrl(moduleConfig, bundleInfo.bundle_name));

                string updatePath = GetUpdatePath();

                request.downloadHandler = new DownloadHandlerFile($"{updatePath}/{bundleInfo.bundle_name}");

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    YXUtils.EditorLogNormal($"下载资源: {bundleInfo.bundle_name}至{updatePath}/{bundleInfo.bundle_name}成功");
                    bundleList.RemoveAt(bundleList.Count - 1);
                }
                else
                {
                    YXUtils.EditorLogError($"下载bundle {GetServerUrl(moduleConfig, bundleInfo.bundle_name)} 中断, {request.error}");
                    break;
                }
            }

            if (bundleList.Count > 0)
            {
                bool result = await ShowMessageBox("网络异常", "继续下载", "退出游戏");

                if (result == false)
                {
                    YXUtils.Quit();

                    return;
                }

                await ExecuteDownload(moduleConfig, bundleList);

                return;
            }
        }

        private async Task<Tuple<List<BundleInfo>, BundleInfo[]>> GetDownloadList(string moduleName)
        {
            ModuleABConfig serverConfig = await AssetLoader.Instance.LoadAssetBundleConfig(moduleName, moduleName.ToLower() + "_temp.json");

            if (serverConfig == null)
            {
                return null;
            }

            ModuleABConfig localConfig = await AssetLoader.Instance.LoadAssetBundleConfig(moduleName, moduleName.ToLower() + ".json");

            Tuple<List<BundleInfo>, BundleInfo[]> result = CaculateDiff(localConfig, serverConfig);

            return result;
        }

        private Tuple<List<BundleInfo>, BundleInfo[]> CaculateDiff(ModuleABConfig localConfig, ModuleABConfig serverConfig)
        {
            List<BundleInfo> bundleList = new List<BundleInfo>();

            Dictionary<string, BundleInfo> localBundleDic = new Dictionary<string, BundleInfo>();

            if (localConfig != null)
            {
                foreach (BundleInfo bundleInfo in localConfig.BundleArray.Values)
                {
                    localBundleDic.Add(bundleInfo.bundle_name, bundleInfo);
                }
            }

            //找到差异的bundle文件,放到bundleList容器中
            foreach (BundleInfo bundleInfo in serverConfig.BundleArray.Values)
            {
                BundleInfo localBundleInfo = localBundleDic.GetValueOrDefault(bundleInfo.bundle_name);

                if (localBundleInfo == null)
                {
                    bundleList.Add(bundleInfo);
                }
                else if (localBundleInfo != null)
                {
                    localBundleDic.Remove(bundleInfo.bundle_name);

                    if (localBundleInfo.crc != bundleInfo.crc)
                    {
                        bundleList.Add(bundleInfo);
                    }
                }
            }

            BundleInfo[] removeList = localBundleDic.Values.ToArray();

            return new Tuple<List<BundleInfo>, BundleInfo[]>(bundleList, removeList);
        }

        private void Clear(ModuleConfig moduleConfig, BundleInfo[] removeList)
        {
            string moduleName = moduleConfig.moduleName;

            string updatePath = GetUpdatePath();

            for (int i = removeList.Length - 1; i >= 0; i--)
            {
                BundleInfo bundleInfo = removeList[i];

                string filePath = $"{updatePath}/{bundleInfo.bundle_name}";

                File.Delete(filePath);
            }

            string oldFile = $"{updatePath}/{moduleName.ToLower()}.json";

            if (File.Exists(oldFile))
            {
                File.Delete(oldFile);
            }

            string newFile = $"{updatePath}/{moduleName.ToLower()}_temp.json";

            File.Move(newFile, oldFile);
        }

        /// <summary>
        /// 模块的热更资源存放地址
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        private string GetUpdatePath()
        {
            return $"{Application.persistentDataPath}/Bundles";
        }

        /// <summary>
        /// 获取模块内指定文件在服务器的url
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetServerUrl(ModuleConfig moduleConfig, string fileName)
        {
#if UNITY_ANDROID
        return $"{Main.Instance.globalConfig.UpdateServerUrl}/{moduleConfig.moduleName}/{moduleConfig.moduleVersion}/Android/{fileName}";
#elif UNITY_IOS
        return $"{Main.Instance.globalConfig.UpdateServerUrl}/{moduleConfig.moduleName}/{moduleConfig.moduleVersion}/IOS/{fileName}";
#elif UNITY_STANDALONE_WIN
            return $"{Main.Instance.globalConfig.UpdateServerUrl}/{moduleConfig.moduleName}/{moduleConfig.moduleVersion}/StandaloneWindows64/{fileName}";
#endif
        }

        private async Task<bool> ShowMessageBox(string messageInfo, string firstText, string secondText)
        {
            MessageBox messageBox = MessageBox.CreatMessageBox(messageInfo, firstText, secondText);

            MessageBox.BoxResult result = await messageBox.GetReplyAxync();

            messageBox.Close();

            if (result == MessageBox.BoxResult.First)
            {
                return true;
            }

            return false;
        }

        private string SizeToString(long totalSize)
        {
            int kb = 1 << 10;
            int mb = 1 << 20;

            StringBuilder stringBuilder = new StringBuilder();

            if (totalSize >= mb)
            {
                stringBuilder.Append(totalSize / mb);
                stringBuilder.Append("MB");

                totalSize = totalSize % mb;
            }

            if (totalSize >= kb)
            {
                stringBuilder.Append(totalSize / kb);
                stringBuilder.Append("KB");

                totalSize = totalSize % kb;
            }

            if (totalSize > 0)
            {
                stringBuilder.Append(totalSize);
                stringBuilder.Append("B");
            }

            return stringBuilder.ToString();
        }
    }
}
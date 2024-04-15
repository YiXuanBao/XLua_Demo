using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace YXCell
{
    /// <summary>
    /// 生成AssetBundle的编辑器工具
    /// </summary>
    public class ABEditor : MonoBehaviour
    {
        /// <summary>
        /// 热更资源的根目录
        /// </summary>
        public static string rootPath = Application.dataPath + "/GAssets";

        /// <summary>
        /// AB包文件的输出路径
        /// </summary>
        private static string abOutputPath;

        /// <summary>
        /// 所有需要打包的AB包信息：一个AssetBundle文件对应了一个AssetBundleBuild对象
        /// </summary>
        public static List<AssetBundleBuild> assetBundleBuildList;

        /// <summary>
        /// 记录每个asset资源依赖的AB包文件列表
        /// </summary>
        public static Dictionary<string, List<string>> asset2Dependencies;

        /// <summary>
        /// 记录哪个asset资源属于哪个AB包文件
        /// </summary>
        public static Dictionary<string, string> asset2bundle;

        [MenuItem("ABEditor/MoveABToDataPath")]
        public static void MoveABToDataPath()
        {
            YXUtils.EditorLogNormal("MoveABToDataPath开始");
            abOutputPath = Application.streamingAssetsPath;
            DirectoryInfo rootDir = new DirectoryInfo(abOutputPath);
            FileInfo[] files = rootDir.GetFiles("*", SearchOption.TopDirectoryOnly);

            string targetPath = $"{Application.persistentDataPath}/Bundles";

            if (Directory.Exists(targetPath))
            {
                Directory.Delete(targetPath, true);
            }

            Directory.CreateDirectory(targetPath);

            foreach (FileInfo fi in files)
            {
                if (fi.Name.EndsWith(".meta")) continue;
                fi.CopyTo($"{targetPath}/{fi.Name}");
            }
            abOutputPath = null;

            YXUtils.EditorLogNormal("MoveABToDataPath完成");
        }

        /// <summary>
        /// 打版本Bundle
        /// </summary>
        [MenuItem("ABEditor/BuildAssetBundle_Base")]
        public static void BuildAssetBundleBase()
        {
            abOutputPath = $"{Application.dataPath}/../AssetBundle_Base";
            BuildAssetBundle();
            abOutputPath = null;
        }

        /// <summary>
        /// 打热更Bundle
        /// </summary>
        [MenuItem("ABEditor/BuildAssetBundle_Update")]
        public static void BuildAssetBundleUpdate()
        {
            abOutputPath = $"{Application.dataPath}/../AssetBundle_Update";

            BuildAssetBundle();

            string baseABPath = $"{Application.dataPath}/../AssetBundle_Base";

            string updateABPath = abOutputPath;

            DirectoryInfo rootDir = new DirectoryInfo(rootPath);
            DirectoryInfo[] Dirs = rootDir.GetDirectories();

            foreach (DirectoryInfo moduleDir in Dirs)
            {
                string moduleName = moduleDir.Name;

                ModuleABConfig baseABConfig = LoadABConfig($"{baseABPath}/{moduleName.ToLower()}.json");
            
                ModuleABConfig updateABConfig = LoadABConfig($"{updateABPath}/{moduleName.ToLower()}.json");

                List<BundleInfo> removeList = Caculate(baseABConfig, updateABConfig);

                foreach (BundleInfo bundleInfo in removeList)
                {
                    string filePath = $"{updateABPath}/{bundleInfo.bundle_name}";

                    File.Delete(filePath);

                    updateABConfig.BundleArray.Remove(bundleInfo.bundle_name);
                }

                // string jsonPath = $"{updateABPath}/{moduleName.ToLower()}.json";

                // if (File.Exists(jsonPath) == true)
                // {
                //     File.Delete(jsonPath);
                // }

                // File.Create(jsonPath).Dispose();

                // string jsonData = JsonMapper.ToJson(updateABConfig);

                // File.WriteAllText(jsonPath, YXUtils.ConvertJsonString(jsonData));
            }

            abOutputPath = null;
        }

        /// <summary>
        /// 打开发Bundle
        /// </summary>
        [MenuItem("ABEditor/BuildAssetBundle_Dev")]
        public static void BuildAssetBundleDev()
        {
            abOutputPath = Application.streamingAssetsPath;

            BuildAssetBundle();

            abOutputPath = null;
        }

        /// <summary>
        /// 打ab包至abOutputPath目录
        /// </summary>
        private static void BuildAssetBundle()
        {
            YXUtils.EditorLogNormal("开始--->>>生成所有模块的AB包!");

            assetBundleBuildList = new List<AssetBundleBuild>();
            asset2Dependencies = new Dictionary<string, List<string>>();
            asset2bundle = new Dictionary<string, string>();

            Dictionary<string, string> totalAsset2Bundle = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> moduleAsset2BundleDict = new Dictionary<string, Dictionary<string, string>>();

            List<AssetBundleBuild> totalBundleBuildList = new List<AssetBundleBuild>();
            Dictionary<string, List<AssetBundleBuild>> moduleBundlesDict = new Dictionary<string, List<AssetBundleBuild>>();

            // 遍历所有模块，针对所有模块都分别打包
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);
            DirectoryInfo[] Dirs = rootDir.GetDirectories();

            foreach (DirectoryInfo moduleDir in Dirs)
            {
                assetBundleBuildList.Clear();
                asset2bundle.Clear();

                // 开始给这个模块生成AB包文件
                ScanChildDireations(moduleDir);

                foreach (var kv in asset2bundle)
                {
                    totalAsset2Bundle.Add(kv.Key, kv.Value);
                }
                moduleAsset2BundleDict.Add(moduleDir.Name, new Dictionary<string, string>(asset2bundle));

                totalBundleBuildList.AddRange(assetBundleBuildList);
                moduleBundlesDict.Add(moduleDir.Name, new List<AssetBundleBuild>(assetBundleBuildList));
            }

            //生成ab包
            if (Directory.Exists(abOutputPath) == true)
            {
                Directory.Delete(abOutputPath, true);
            }

            Directory.CreateDirectory(abOutputPath);

            BuildPipeline.BuildAssetBundles(abOutputPath, totalBundleBuildList.ToArray(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            //生成每个模块的依赖配置
            foreach (DirectoryInfo moduleDir in Dirs)
            {
                string moduleName = moduleDir.Name;
                
                asset2Dependencies.Clear();

                Dictionary<string, string> moduleAsset = moduleAsset2BundleDict[moduleName];
                List<AssetBundleBuild> moduleBundleList = moduleBundlesDict[moduleName];

                CalculateDependencies(moduleAsset, totalAsset2Bundle);

                SaveModuleABConfig(moduleName, moduleAsset, moduleBundleList);
            }

            DeleteManifest();

            string outputDirName = abOutputPath.Substring(abOutputPath.LastIndexOf('/') + 1);
            File.Delete( $"{abOutputPath}/{outputDirName}");

            AssetDatabase.Refresh();

            YXUtils.EditorLogNormal("结束--->>>生成所有模块的AB包!");

            assetBundleBuildList = null;
            asset2bundle = null;
            asset2Dependencies = null;
        }

        /// <summary>
        /// 删除生成的Manifest文件
        /// </summary>
        /// <param name="moduleOutputPath"></param>
        private static void DeleteManifest()
        {
            FileInfo[] files = new DirectoryInfo(abOutputPath).GetFiles();

            foreach (FileInfo fileInfo in files)
            {
                if (fileInfo.Name.EndsWith(".manifest"))
                {
                    fileInfo.Delete();
                }
            }
        }

        /// <summary>
        /// 根据指定的文件夹
        /// 1. 将这个文件夹下的所有一级子文件打成一个AssetBundle
        /// 2. 并且递归遍历这个文件夹下的所有子文件夹
        /// </summary>
        /// <param name="directoryInfo"></param>
        private static void ScanChildDireations(DirectoryInfo directoryInfo)
        {
            if (directoryInfo.Name.EndsWith("CSProject~"))
            {
                return;
            }

            // 收集当前路径下的文件 把它们打成一个AB包

            ScanCurrDirectory(directoryInfo);

            // 遍历当前路径下的子文件夹

            DirectoryInfo[] dirs = directoryInfo.GetDirectories();

            foreach (DirectoryInfo info in dirs)
            {
                ScanChildDireations(info);
            }
        }

        /// <summary>
        /// 遍历当前路径下的文件 把它们打成一个AB包
        /// </summary>
        /// <param name="directoryInfo"></param>
        private static void ScanCurrDirectory(DirectoryInfo directoryInfo)
        {
            List<string> assetNames = new List<string>();

            FileInfo[] fileInfoList = directoryInfo.GetFiles();

            int subIndex = Application.dataPath.Length - "Assets".Length;

            foreach (FileInfo fileInfo in fileInfoList)
            {
                if (fileInfo.FullName.EndsWith(".meta"))
                {
                    continue;
                }

                // assetName的格式类似 "Assets/GAssets/Launch/Sphere.prefab"

                string assetName = fileInfo.FullName.Substring(subIndex).Replace('\\', '/');

                assetNames.Add(assetName);
            }

            if (assetNames.Count > 0)
            {
                // 格式类似 gassets_Launch

                string assetbundleName = directoryInfo.FullName.Substring(Application.dataPath.Length + 1).Replace('\\', '_').ToLower();

                AssetBundleBuild build = new AssetBundleBuild();

                build.assetBundleName = assetbundleName;

                build.assetNames = new string[assetNames.Count];

                for (int i = 0; i < assetNames.Count; i++)
                {
                    build.assetNames[i] = assetNames[i];

                    // 记录单个资源属于哪个bundle文件

                    asset2bundle.Add(assetNames[i], assetbundleName);
                }

                assetBundleBuildList.Add(build);
            }
        }

        /// <summary>
        /// 计算每个资源所依赖的ab包文件列表
        /// </summary>
        private static void CalculateDependencies(Dictionary<string, string> moduleAsset2Bundle, Dictionary<string, string> totalAsset2Bundle)
        {
            foreach (string asset in moduleAsset2Bundle.Keys)
            {
                // 这个资源自己所在的bundle
                string assetBundle = moduleAsset2Bundle[asset];

                string[] dependencies = AssetDatabase.GetDependencies(asset);

                //YXUtils.EditorLogNormal(asset + "    dependencies:" + LitJson.JsonMapper.ToJson(dependencies));

                List<string> assetList = new List<string>();

                if (dependencies != null && dependencies.Length > 0)
                {
                    foreach (string oneAsset in dependencies)
                    {
                        if (oneAsset == asset || oneAsset.EndsWith(".cs"))
                        {
                            continue;
                        }

                        assetList.Add(oneAsset);
                    }
                }

                if (assetList.Count > 0)
                {
                    List<string> abList = new List<string>();

                    foreach (string oneAsset in assetList)
                    {
                        bool result = totalAsset2Bundle.TryGetValue(oneAsset, out string bundle);

                        if (result == true)
                        {
                            if (bundle != assetBundle)
                            {
                                abList.Add(bundle);
                            }
                        }
                    }

                    asset2Dependencies.Add(asset, abList);
                }
            }
        }

        /// <summary>
        /// 将一个模块的资源依赖关系数据保存成json格式的文件
        /// </summary>
        /// <param name="moduleName">模块名字</param>
        private static void SaveModuleABConfig(string moduleName, Dictionary<string, string> moduleAsset2Bundle, List<AssetBundleBuild> moduleBundles)
        {
            ModuleABConfig moduleABConfig = new ModuleABConfig(moduleAsset2Bundle.Count);

            // 记录AB包信息

            foreach (AssetBundleBuild build in moduleBundles)
            {
                BundleInfo bundleInfo = new BundleInfo();

                bundleInfo.bundle_name = build.assetBundleName;

                bundleInfo.assets = new List<string>();

                foreach (string asset in build.assetNames)
                {
                    bundleInfo.assets.Add(asset);
                }

                // 计算一个bundle文件的CRC散列码

                string abFilePath = abOutputPath + "/" + bundleInfo.bundle_name;

                using (FileStream stream = File.OpenRead(abFilePath))
                {
                    bundleInfo.crc = AssetUtility.GetCRC32Hash(stream);

                    bundleInfo.size = (int)stream.Length;
                }

                moduleABConfig.AddBundle(bundleInfo.bundle_name, bundleInfo);
            }

            // 记录每个资源的依赖关系

            int assetIndex = 0;

            foreach (var item in moduleAsset2Bundle)
            {
                AssetInfo assetInfo = new AssetInfo();
                assetInfo.asset_path = item.Key;
                assetInfo.bundle_name = item.Value;
                assetInfo.dependencies = new List<string>();

                bool result = asset2Dependencies.TryGetValue(item.Key, out List<string> dependencies);

                if (result == true)
                {
                    for (int i = 0; i < dependencies.Count; i++)
                    {
                        string bundleName = dependencies[i];

                        assetInfo.dependencies.Add(bundleName);
                    }
                }

                moduleABConfig.AddAsset(assetIndex, assetInfo);

                assetIndex++;
            }

            // 开始写入Json文件

            string moduleConfigName = moduleName.ToLower() + ".json";

            string jsonPath = abOutputPath + "/" + moduleConfigName;

            if (File.Exists(jsonPath) == true)
            {
                File.Delete(jsonPath);
            }

            File.Create(jsonPath).Dispose();

            string jsonData = LitJson.JsonMapper.ToJson(moduleABConfig);

            File.WriteAllText(jsonPath, YXUtils.ConvertJsonString(jsonData));
        }

        private static ModuleABConfig LoadABConfig(string abConfigPath)
        {
            string text = File.ReadAllText(abConfigPath);
            return JsonMapper.ToObject<ModuleABConfig>(text);
        }

        private static List<BundleInfo> Caculate(ModuleABConfig baseABConfig, ModuleABConfig updateABConfig)
        {
            //base版本的bundle
            Dictionary<string, BundleInfo> baseBundleDic = new Dictionary<string, BundleInfo>();

            if (baseABConfig != null)
            {
                foreach (BundleInfo bundleInfo in baseABConfig.BundleArray.Values)
                {
                    string uniqueId = $"{bundleInfo.bundle_name}|{bundleInfo.crc}";

                    baseBundleDic.Add(uniqueId, bundleInfo);
                }
            }

            List<BundleInfo> removeList = new List<BundleInfo>();

            foreach (BundleInfo bundleInfo in updateABConfig.BundleArray.Values)
            {
                string uniqueId = $"{bundleInfo.bundle_name}|{bundleInfo.crc}";

                if (baseBundleDic.ContainsKey(uniqueId) == true)
                {
                    removeList.Add(bundleInfo);
                }
            }

            return removeList;
        }
    }
}
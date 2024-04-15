using LitJson;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace YXCell
{
    /// <summary>
    /// 模块资源加载器
    /// </summary>
    public class AssetLoader : Singleton<AssetLoader>
    {
        /// <summary>
        /// 平台对应只读路径下的Bundle资源
        /// </summary>
        public Dictionary<string, Dictionary<string, AssetRef>> base2Assets;

        /// <summary>
        /// 平台可读写目录下的Bundle资源
        /// </summary>
        public Dictionary<string, Dictionary<string, AssetRef>> update2Assets;

        public Dictionary<string, BundleRef> bundleCache;

        public AssetLoader()
        {
            base2Assets = new Dictionary<string, Dictionary<string, AssetRef>>();
            update2Assets = new Dictionary<string, Dictionary<string, AssetRef>>();

            bundleCache = new Dictionary<string, BundleRef>();
        }

        BundleRef GetBundleRef(string bundleName)
        {
            return bundleCache.GetValueOrDefault(bundleName, null);
        }

        #region 资源加载
        public GameObject Clone(string moduleName, string assetPath)
        {
            AssetRef assetRef = LoadAssetRef<GameObject>(moduleName, assetPath);

            if (assetRef == null || assetRef.asset == null)
            {
                return null;
            }

            GameObject gameObject = UnityEngine.Object.Instantiate(assetRef.asset) as GameObject;

            if (assetRef.children == null)
            {
                assetRef.children = new List<GameObject>();
            }

            assetRef.children.Add(gameObject);

            return gameObject;
        }

        /// <summary>
        /// 加载资源对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName"></param>
        /// <param name="assetPath"></param>
        /// <param name="gameObject">资源要挂载的gameobject</param>
        /// <returns></returns>
        public T CreatAsset<T>(string moduleName, string assetPath, GameObject gameObject) where T : UnityEngine.Object
        {
            if (typeof(T) == typeof(GameObject) || (!string.IsNullOrEmpty(assetPath) && assetPath.EndsWith(".prefab")))
            {
                YXUtils.EditorLogError("不可加载GameObject类型，请使用Clone接口, path: " + assetPath);
                return null;
            }

            if (gameObject == null)
            {
                YXUtils.EditorLogError("CreatAsset必须传递一个将要挂载的gameobject");
                return null;
            }

            AssetRef assetRef = LoadAssetRef<T>(moduleName, assetPath);

            if (assetRef == null || assetRef.asset == null)
            {
                return null;
            }

            if (assetRef.children == null)
            {
                assetRef.children = new List<GameObject>();
            }

            assetRef.children.Add(gameObject);

            return assetRef.asset as T;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName"></param>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private AssetRef LoadAssetRef<T>(string moduleName, string assetPath) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (Main.Instance.globalConfig.BundleMode == false)
            {
                return LoadAssetRef_Editor<T>(moduleName, assetPath);
            }
            else
            {
                return LoadAssetRef_Runtime<T>(moduleName, assetPath);
            }
#else
        return LoadAssetRef_Runtime<T>(moduleName, assetPath);
#endif
        }

        /// <summary>
        /// 编辑器模式加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName"></param>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private AssetRef LoadAssetRef_Editor<T>(string moduleName, string assetPath) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(assetPath))
            {
                return null;
            }

            AssetRef assetRef = new AssetRef(null);
            assetRef.asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            return assetRef;
#else
        return null;
#endif
        }

        /// <summary>
        /// bundle模式加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName"></param>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private AssetRef LoadAssetRef_Runtime<T>(string moduleName, string assetPath) where T : UnityEngine.Object
        {
            Dictionary<string, AssetRef> assets;

            if (Main.Instance.globalConfig.HotUpdate)
                assets = update2Assets.GetValueOrDefault(moduleName);
            else
                assets = base2Assets.GetValueOrDefault(moduleName);

            if (assets == null)
            {
                YXUtils.EditorLogError(moduleName + "模块资源不存在");
                return null;
            }

            AssetRef assetRef = assets.GetValueOrDefault(assetPath);
            if (assetRef == null)
            {
                YXUtils.EditorLogError($"模块{moduleName}内不存在{assetPath}");
                return null;
            }

            if (assetRef.asset != null)
            {
                return assetRef;
            }

            //加载依赖的bundleRef列表
            foreach (string oneBundleName in assetRef.assetInfo.dependencies)
            {
                BundleRef oneBundle = GetBundleRef(oneBundleName);

                if (oneBundle == null)
                {
                    oneBundle = new BundleRef(oneBundleName);
                    bundleCache.Add(oneBundleName, oneBundle);
                }

                if (oneBundle.bundle == null)
                {
                    string bundlePath = BundlePath(moduleName, oneBundle.bundleName);
                    oneBundle.bundle = AssetBundle.LoadFromFile(bundlePath);
                }

                if (oneBundle.children == null)
                {
                    oneBundle.children = new List<AssetRef>();
                }

                oneBundle.children.Add(assetRef);
            }

            string selfBundleName = assetRef.assetInfo.bundle_name;
            //加载自身bundle
            BundleRef bundleRef = GetBundleRef(selfBundleName); ;

            if (bundleRef == null)
            {
                bundleRef = new BundleRef(selfBundleName);
                bundleCache.Add(selfBundleName, bundleRef);
            }

            if (bundleRef.bundle == null)
            {
                string bundlePath = BundlePath(moduleName, bundleRef.bundleName);

                bundleRef.bundle = AssetBundle.LoadFromFile(bundlePath);
            }

            if (bundleRef.children == null)
            {
                bundleRef.children = new List<AssetRef>();
            }

            bundleRef.children.Add(assetRef);

            assetRef.asset = bundleRef.bundle.LoadAsset<T>(assetRef.assetInfo.asset_path);

            if (typeof(T) == typeof(GameObject) && assetRef.assetInfo.asset_path.EndsWith(".prefab"))
            {
                assetRef.isGameObject = true;
            }
            else
            {
                assetRef.isGameObject = false;
            }

            return assetRef;
        }

        /// <summary>
        /// bundle路径
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        private string BundlePath(string moduleName, string bundleName)
        {
            if (Main.Instance.globalConfig.HotUpdate)
                return $"{Application.persistentDataPath}/Bundles/{bundleName}";
            else
                return $"{Application.streamingAssetsPath}/{bundleName}";
        }

        /// <summary>
        /// 全局卸载方法
        /// </summary>
        /// <param name="module2Assets"></param>
        public void Unload()
        {
            // foreach (string moduleName in module2Assets.Keys)
            // {
            //     Dictionary<string, AssetRef> path2AssetRef = module2Assets[moduleName];

            //     if (path2AssetRef == null)
            //     {
            //         continue;
            //     }

            //     bool needUnload = false;

            //     foreach (AssetRef assetRef in path2AssetRef.Values)
            //     {
            //         if (assetRef.children == null || assetRef.children.Count == 0)
            //         {
            //             continue;
            //         }

            //         //检查依赖此资源的对象是否被销毁
            //         for (int i = assetRef.children.Count - 1; i >= 0; i--)
            //         {
            //             GameObject go = assetRef.children[i];

            //             if (go == null)
            //             {
            //                 assetRef.children.RemoveAt(i);
            //             }
            //         }

            //         //没有gameobject依赖此资源,可以卸载了
            //         if (assetRef.children.Count == 0)
            //         {
            //             assetRef.asset = null;
            //             needUnload = true;

            //             //检查资源依赖的bundle是否可以卸载
            //             assetRef.bundleRef.children.Remove(assetRef);
            //             if (assetRef.bundleRef.children.Count == 0)
            //             {
            //                 assetRef.bundleRef.bundle.Unload(true);
            //             }

            //             foreach (BundleRef bundleRef in assetRef.dependencies)
            //             {
            //                 bundleRef.children.Remove(assetRef);
            //                 if (bundleRef.children.Count == 0)
            //                 {
            //                     bundleRef.bundle.Unload(true);
            //                 }
            //             }
            //         }
            //     }

            //     if (needUnload)
            //     {
            //         Resources.UnloadUnusedAssets();
            //     }
            // }
        }
        #endregion

        #region 配置初始化
        public Dictionary<string, AssetRef> ConfigAssembly(ModuleABConfig moduleABConfig)
        {
            Dictionary<string, AssetRef> Path2AssetRef = new Dictionary<string, AssetRef>();

            for (int i = 0; i < moduleABConfig.AssetArray.Length; i++)
            {
                AssetInfo assetInfo = moduleABConfig.AssetArray[i];

                AssetRef assetRef = new AssetRef(assetInfo);

                Path2AssetRef.Add(assetInfo.asset_path, assetRef);
            }

            return Path2AssetRef;
        }

        public async Task<ModuleABConfig> LoadAssetBundleConfig(string moduleName, string bundleConfigName)
        {
            string url = BundlePath(moduleName, bundleConfigName);

            UnityWebRequest request = UnityWebRequest.Get(url);

            await request.SendWebRequest();

            if (string.IsNullOrEmpty(request.error) == true)
            {
                return JsonMapper.ToObject<ModuleABConfig>(request.downloadHandler.text);
            }
            return null;
        }
        #endregion
    }
}
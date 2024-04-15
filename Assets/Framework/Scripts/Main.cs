using System.Collections;
using System.IO;
using UnityEngine;
using static XLua.LuaEnv;

namespace YXCell
{
    public class Main : MonoSingleton<Main>
    {
        public YXABConfig abConfig;

        public GlobalConfig globalConfig;

        public XLua.LuaEnv luaEnv { get; } = new XLua.LuaEnv();

        protected async override void Awake()
        {
            base.Awake();

            InitGlobal();

            InitCustomLoader();

            foreach (var moduleConfig in abConfig.modules)
            {
                ModuleManager.Instance.ResiterModuleConfig(moduleConfig);
            }

            await ModuleManager.Instance.Load("Public");
            await ModuleManager.Instance.Load("Launch");

            YXUtils.EditorLogNormal("Lua代码开始...");

            gameObject.AddComponent<MonoProxy>().BindScript("Launch", "Main");

            var go = AssetLoader.Instance.Clone("Launch", "Assets/GAssets/Launch/Res/Cube.prefab");
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            StartCoroutine(UnloadUnuseResource());
        }

        IEnumerator UnloadUnuseResource()
        {
            while (true)
            {
                AssetLoader.Instance.Unload();
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// ��ʼ��ȫ�ֱ���
        /// </summary>
        private void InitGlobal()
        {

            DontDestroyOnLoad(gameObject);

            abConfig = Resources.Load<YXABConfig>("Data/ABConfig");

            abConfig.modules.Sort();

            globalConfig = Resources.Load<GlobalConfig>("Data/GlobalConfig");
        }

        private void InitCustomLoader()
        {
            DirectoryInfo baseDir = new DirectoryInfo(Application.dataPath + "/GAssets");

            DirectoryInfo[] Dirs = baseDir.GetDirectories();

            foreach (DirectoryInfo moduleDir in Dirs)
            {
                string moduleName = moduleDir.Name;

                CustomLoader loader = (ref string scriptPath) =>
                {
                    string assetPath = $"Assets/GAssets/{moduleName}/Src/{scriptPath.Trim()}.lua";
                    TextAsset asset = AssetLoader.Instance.CreatAsset<TextAsset>("Launch", assetPath, Main.Instance.gameObject);

                    if (asset != null)
                    {
                        string scriptstring = asset.text;
                        byte[] result = System.Text.Encoding.UTF8.GetBytes(scriptstring);
                        return result;
                    }

                    return null;
                };

                luaEnv.AddLoader(loader);
            }

        }
    }
}
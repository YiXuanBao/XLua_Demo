using UnityEngine;

namespace YXCell
{
    [CreateAssetMenu(fileName ="GlobalConfig", menuName ="YXCell/Global Config", order=2)]
    public class GlobalConfig : ScriptableObject
    {
        public bool HotUpdate;

        public bool BundleMode;

        public string UpdateServerUrl = "http://127.0.0.1:8080";

        public string Version = "V1";
    }
}
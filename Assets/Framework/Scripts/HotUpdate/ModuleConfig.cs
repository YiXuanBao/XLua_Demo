using System;

namespace YXCell
{
    [System.Serializable]
    public class ModuleConfig : IComparable<ModuleConfig>
    {
        public string moduleName;
        public string moduleVersion;

        public int order;

        public bool isSub;

        public int CompareTo(ModuleConfig other)
        {
            return order - other.order;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
namespace YXCell
{
    public class BundleRef
    {
        public string bundleName;

        public AssetBundle bundle;

        public List<AssetRef> children;

        public BundleRef(string bundleName)
        {
            this.bundleName = bundleName;
        }
    }
}
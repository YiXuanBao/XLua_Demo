using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YXCell
{
    [CreateAssetMenu(fileName ="YXABConfig", menuName ="YXCell/AB Config", order=1)]
    public class YXABConfig : ScriptableObject
    {
        public List<ModuleConfig> modules;
    }
}

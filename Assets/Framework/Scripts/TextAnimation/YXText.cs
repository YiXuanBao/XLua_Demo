using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YXCell
{
    [DisallowMultipleComponent]
    public class YXText : TextMeshProUGUI
    {
        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void Initialize()
        {
            
        }
    }
}

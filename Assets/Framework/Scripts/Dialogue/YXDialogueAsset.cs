using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YXCell
{
    [CreateAssetMenu(fileName ="YXDialogueAsset", menuName ="YXCell/Dialogue Asset", order=5)]
    public class YXDialogueAsset : ScriptableObject
    {
        public string m_name;
        public List<YXDialogueEditorNode> editorNodes = new List<YXDialogueEditorNode>();
        public List<YXDE_IntProperty> int_properties = new List<YXDE_IntProperty>();  
        public List<YXDE_EventProperty> event_properties = new List<YXDE_EventProperty>();  
    }
}

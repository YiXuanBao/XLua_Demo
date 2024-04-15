using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YXCell
{
    [System.Serializable]
    public class YXDialogueEditorNode 
    {
        public string name;
        public int uid;
        public Rect rect, oRect;
        public YXDialogueEditorNodeType type;
        public YXDialogueNodeInfo info;
        
        private List<YXDialogueEditorNode> m_linkedNodes, m_linkedFromNodes;
        private int m_orderInAsset;
        public int orderInAsset
        {
            get
            {
                int i = 0;
                foreach(var item in YXDialogueManager.curEditorAsset.editorNodes)
                {
                    if (item == this)
                        break;
                    i++;
                }
                m_orderInAsset = i;
                return m_orderInAsset;
            }
        }

        public List<YXDialogueEditorNode> linkedNodes { get
            {
                ConstructLinks();
                return m_linkedNodes;
            }}
        public List<YXDialogueEditorNode> linkedFromNodes
        {
            get
            {
                ConstructLinks();
                return m_linkedFromNodes;
            }
        }
        public List<int> linkedNodesID = new List<int>();
        public List<int> linkedFromNodesID = new List<int>();

        public string event_name = "<event name>";
        public float event_arg0, event_arg1;

        public string int_property_name = "<variable>";
        public int int_property_value = 0;

        public string if_property_name = "<variable>";
        public int if_property_value = 0;
        public YXDE_Comparator if_comparator = 0;


        public YXDialogueEditorNode()
        {
            info = new YXDialogueNodeInfo();
            info.content = "<content>";
            info.speaker = "<speaker>";
        }

        private void ConstructLinks()
        {
            if (YXDialogueManager.curEditorAsset == null) return;
            m_linkedNodes = new List<YXDialogueEditorNode>();
            m_linkedFromNodes = new List<YXDialogueEditorNode>();   
            foreach (var item in YXDialogueManager.curEditorAsset.editorNodes)
            {
                if (linkedNodesID.Contains(item.uid))
                {
                    m_linkedNodes.Add(item);
                }
                if (linkedFromNodesID.Contains(item.uid))
                {
                    m_linkedFromNodes.Add(item);
                }
            }
        }

        public string GetSpeakerString()
        {
            return info.speaker;
        }
        public string GetContentString()
        {
            return info.content;
        }
        public Vector2 GetSize()
        {
            switch (type)
            {
                case YXDialogueEditorNodeType.Sentence:
                    return new Vector2(100, 130);
                case YXDialogueEditorNodeType.Choice:
                    return new Vector2(100, 80);
                case YXDialogueEditorNodeType.Start:
                    return new Vector2(100, 80);
                case YXDialogueEditorNodeType.Set:
                    return new Vector2(100, 80);
                case YXDialogueEditorNodeType.Event:
                    return new Vector2(100, 80);
                case YXDialogueEditorNodeType.Random:
                    return new Vector2(100, 80);
                case YXDialogueEditorNodeType.If:
                    return new Vector2(100, 80);
                default:
                    return new Vector2(100, 80);
            }
        }
    }

    [System.Serializable]
    public class YXDE_IntProperty
    {
        public string name;
        public int value;
    }

    [System.Serializable]
    public class YXDE_EventProperty
    {
        public string name;
        public Action<float, float> action;
    }
    public enum YXDialogueEditorNodeType
    {
        Sentence,
        Choice,
        Random,
        Event,
        Start,
        Set,
        If,
        End
    }

    public enum YXDE_Comparator
    {
        Equals,
        GreaterThan,
        LessThan,
    }

}

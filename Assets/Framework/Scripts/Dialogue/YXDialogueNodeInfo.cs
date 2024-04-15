using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YXCell
{
    [System.Serializable]
    public class YXDialogueNodeInfo
    {
        public int content_localID, speaker_localID;
        public string content;
        public string speaker;
        public Texture2D avatar; 
        public AudioClip audio; 
    }
}

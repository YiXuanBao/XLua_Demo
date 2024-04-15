using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YXCell
{
    [CustomEditor(typeof(YXDialoguePlayer))]
    public class YXDialoguePlayerEditor : Editor
    {
        YXDialoguePlayer player;
        YXDialogueEditor editor;
        private void OnEnable()
        {
            player = (YXDialoguePlayer)target;
        }

        public override void OnInspectorGUI()
        {
            GUI.color = new Color(.9f, .8f, .7f);
            if(GUILayout.Button("<Open Dialogue Editor>"))
            {
                editor = (YXDialogueEditor)EditorWindow.GetWindow<YXDialogueEditor>("YX Dialogue Editor", typeof(UnityEditor.SceneView));
                editor.LoadAsset(player.asset);
            }
            GUI.color = Color.white;
            if (GUILayout.Button("<New Dialogue Asset>"))
            {
                string path = EditorUtility.SaveFilePanel("Create new dialogue asset", "Assets/", "New Dialogue Asset", "asset");
                if (path.IndexOf("Assets") > 0)
                {
                    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<YXDialogueAsset>(), path.Substring(path.IndexOf("Assets")));
                    player.asset = AssetDatabase.LoadAssetAtPath<YXDialogueAsset>(path.Substring(path.IndexOf("Assets")));
                    editor = (YXDialogueEditor)EditorWindow.GetWindow(typeof(YXDialogueEditor));
                    editor.LoadAsset(player.asset);
                }
            }
            
            base.OnInspectorGUI();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace YXCell
{
    public class YXDialogueEditor : EditorWindow
    {
        static Color COL_BACKGROUND = new Color(.1f, .1f, .1f);
        static Color COL_WIRE = new Color(1, 1, 1, .07f);
        static Color COL_TITLE = new Color(1, 1, 1, .6f);
        static Color COL_YELLOW = new Color(1f, .95f, .6f, 0.9f);
        static Color COL_BLUE = new Color(.4f, .7f, .9f, 0.8f);
        static Color COL_ORANGE = new Color(1f, .7f, .7f, 0.8f);
        static Rect BG_RECT = new Rect(-1000, -1000, 10000, 10000);

        static GUIStyle titleStyle, node_whiteStyle, node_yellowStyle, node_greenStyle, node_greyStyle, node_purpleStyle, node_titleStyle, inspector_bgStyle, inspector_buttonStyle;
        static GUIStyle node_subtitleStyle, node_centerStyle, node_centerStyle2, node_centerStyle3, node_subtitle2Style, inspector_titleStyle, text_orangeStyle, node_blueStyle;

        Vector2 inspector_scrollPos_0, inspector_scrollPos_1;

        public YXDialogueAsset asset;
        public List<YXDialogueEditorNode> nodes;
        public YXDialogueEditorNode inspectorNode;

        //connection state
        bool isConnecting = false;
        YXDialogueEditorNode connectFromNode;

        private GameObject prevSelection;
        private YXDialogueAsset f_asset;


        float panX = 0;
        float panY = 0;

        [MenuItem("Tools/YXCell/Diagolue Editor", priority = 2)]
        static void OpenWindow()
        {
            YXDialogueEditor editor = EditorWindow.GetWindow<YXDialogueEditor>("YX Dialogue Editor", typeof(UnityEditor.SceneView));
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnBecameVisible()
        {
            InitializeGUIStyles();
        }

        private void OnLostFocus()
        {
            if (asset != null)
                EditorUtility.SetDirty(asset);
        }
        public void Init()
        {
            if (asset != null)
                LoadAsset(asset);
            else
                LoadAsset(null);
            inspectorNode = null;
        }

        private void InitializeGUIStyles()
        {
            titleStyle = new GUIStyle()
            {
                alignment = TextAnchor.UpperLeft,
                ////font = YXAssetLibrary.DefaultFont,
                fontSize = 21,
            };
            titleStyle.normal.textColor = COL_TITLE;

            node_titleStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                ////font = YXAssetLibrary.DefaultFont,
                fontSize = 10,
            };
            node_titleStyle.normal.textColor = COL_TITLE;

            text_orangeStyle = new GUIStyle();
            text_orangeStyle.normal.textColor = COL_ORANGE;
            text_orangeStyle.alignment = TextAnchor.MiddleLeft;

            node_subtitleStyle = new GUIStyle()
            {
                alignment = TextAnchor.UpperLeft,
                ////font = YXAssetLibrary.DefaultFont,
                fontSize = 11,
                wordWrap = true
            };
            node_subtitleStyle.normal.textColor = COL_TITLE;
            node_centerStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                //font = YXAssetLibrary.DefaultFont,
                fontSize = 12,
                wordWrap = true
            };
            node_centerStyle.normal.textColor = COL_TITLE;
            node_centerStyle2 = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                //font = YXAssetLibrary.DefaultFont,
                fontSize = 12,
                wordWrap = true
            };
            node_centerStyle2.normal.textColor = COL_BLUE;
            node_centerStyle3 = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                //font = YXAssetLibrary.DefaultFont,
                fontSize = 12,
                wordWrap = true
            };
            node_centerStyle3.normal.textColor = COL_ORANGE;

            node_subtitle2Style = new GUIStyle()
            {
                alignment = TextAnchor.UpperLeft,
                //font = YXAssetLibrary.DefaultFont,
                fontSize = 11,
                wordWrap = true
            };
            node_subtitle2Style.normal.textColor = COL_YELLOW;

            node_whiteStyle = new GUIStyle();
            node_whiteStyle.normal.background = MakeTex(2, 2, new Color(1, 1, 1, 0.6f));

            node_yellowStyle = new GUIStyle();
            node_yellowStyle.normal.background = MakeTex(2, 2, new Color(1f, .95f, .5f, 0.6f));
            node_greyStyle = new GUIStyle();
            node_greyStyle.normal.background = MakeTex(2, 2, new Color(1f, 1f, 1f, 0.6f));
            node_purpleStyle = new GUIStyle();
            node_purpleStyle.normal.background = MakeTex(2, 2, new Color(.7f, .85f, .25f, 0.8f));
            node_greenStyle = new GUIStyle();
            node_greenStyle.normal.background = MakeTex(2, 2, new Color(.6f, 1f, .8f, 0.6f));
            node_blueStyle = new GUIStyle();
            node_blueStyle.normal.background = MakeTex(2, 2, new Color(.4f, .7f, .9f, 0.8f));

            inspector_titleStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                //font = YXAssetLibrary.DefaultFont,
                fontSize = 14,
            };
            inspector_titleStyle.normal.textColor = new Color(1f, .95f, .7f, 0.75f);

            inspector_bgStyle = new GUIStyle();
            //inspector_bgStyle.font = YXAssetLibrary.DefaultFont;
            inspector_bgStyle.fontSize = 11;
            inspector_bgStyle.alignment = TextAnchor.MiddleCenter;
            inspector_bgStyle.normal.background = MakeTex(2, 2, new Color(.18f, .18f, .18f, 0.9f));
            inspector_bgStyle.normal.textColor = COL_TITLE;

            inspector_buttonStyle = new GUIStyle();
            //inspector_buttonStyle.font = YXAssetLibrary.DefaultFont;
            inspector_buttonStyle.fontSize = 11;
            inspector_buttonStyle.alignment = TextAnchor.MiddleCenter;
            inspector_buttonStyle.normal.background = MakeTex(2, 2, new Color(.18f, .18f, .18f, 0.9f));
            inspector_buttonStyle.normal.textColor = COL_TITLE;
            inspector_buttonStyle.hover.background = MakeTex(2, 2, new Color(.3f, .3f, .3f, 0.9f));
            inspector_buttonStyle.hover.textColor = new Color(1f, .95f, .5f, 0.6f);
        }

        public void SaveAsset()
        {

        }
        public void LoadAsset(YXDialogueAsset asset)
        {
            if (asset == null)
            {
                this.asset = null;
                nodes = null;
                YXDialogueManager.curEditorAsset = null;
                return;
            }
            this.asset = asset;
            this.f_asset = asset;
            nodes = asset.editorNodes;
            YXDialogueManager.curEditorAsset = asset;

            if (asset.editorNodes.Count == 0)
            {
                YXDialogueEditorNode start = AddNewNode(YXDialogueEditorNodeType.Start, new Vector2(100, 100));
                YXDialogueEditorNode end = AddNewNode(YXDialogueEditorNodeType.End, new Vector2(400, 100));
            }
        }
        private void Update()
        {
            if (Selection.activeGameObject != null && Selection.activeGameObject != prevSelection)
            {
                YXDialoguePlayer player = Selection.activeGameObject.GetComponent<YXDialoguePlayer>();
                if (player != null)
                {
                    if (asset != player.asset)
                    {
                        LoadAsset(player.asset);
                        Repaint();
                    }
                }
            }
            prevSelection = Selection.activeGameObject;
        }
        void OnGUI()
        {
            GUI.depth = 0;
            DrawBackground();
            HandleMouseInput();
            HandleConnectionState();

            GUI.BeginGroup(new Rect(panX - 1000, panY - 1000, 100000, 100000));
            BeginWindows();
            if (nodes != null && YXDialogueManager.curEditorAsset != null)
                for (int i = 0; i < nodes.Count; i++)
                {
                    DrawNode(nodes[i], i);
                }
            EndWindows();

            GUI.EndGroup();

            DrawInspector();

            if (EditorWindow.mouseOverWindow == this)
            {
                Repaint();
            }
        }

        void DrawBackground()
        {
            GUI.backgroundColor = new Color(.8f, .8f, .8f);
            EditorGUI.DrawRect(BG_RECT, COL_BACKGROUND);

            //EditorGUI.DrawRect(new Rect(0, position.yMax - 300, 200, 300), Color.cyan);

            if (IsMouseInInspector())
                return;

            Event e = Event.current;
            if (e.type == EventType.MouseDrag)
            {
                if (GetMouseOverNode() != null) return;
                panX += e.delta.x;
                panY += e.delta.y;
            }
        }

        void DrawInspector()
        {
            f_asset = EditorGUI.ObjectField(new Rect(2, 5, 150, 20), f_asset, typeof(YXDialogueAsset), false) as YXDialogueAsset;
            if (GUI.Button(new Rect(160, 5, 40, 20), "Load"))
            {
                if (asset == null || asset != f_asset)
                    LoadAsset(f_asset);
            }

            if (asset == null)
            {
                GUI.Label(new Rect(position.xMax / 4, position.yMax / 2 - 20, 100, 50), "Create a new asset to continue.", titleStyle);
            }
            if (inspectorNode == null) return;
            YXDialogueEditorNode node = inspectorNode;
            float sizeX = 200, sizeY = 300;

            //avatar frame
            if (node.type == YXDialogueEditorNodeType.Sentence)
            {
                GUILayout.BeginArea(new Rect(sizeX + 20, this.position.yMax - sizeY * 0.5f - 20, sizeX * 0.7f, sizeY / 3 + 20));
                GUILayout.Box("", inspector_bgStyle, GUILayout.Width(sizeX), GUILayout.Height(sizeY / 3));
                GUILayout.Space(-sizeY / 3);
                GUILayout.Label("Avatar sprite: ");
                node.info.avatar = (Texture2D)EditorGUILayout.ObjectField(node.info.avatar, typeof(Texture2D), false);
                GUILayout.Label("Audio: ");
                node.info.audio = (AudioClip)EditorGUILayout.ObjectField(node.info.audio, typeof(AudioClip), false);
                GUILayout.EndArea();
            }

            GUILayout.BeginArea(new Rect(0, this.position.yMax - sizeY, sizeX, sizeY));
            GUILayout.Box("", inspector_bgStyle, GUILayout.Width(sizeX), GUILayout.Height(sizeY));
            GUILayout.Space(-sizeY);

            GUILayout.Label($"{node.type.ToString()} Node", inspector_titleStyle);
            node.name = GUILayout.TextField(node.name);

            //main inspector
            if (node.type == YXDialogueEditorNodeType.Sentence || node.type == YXDialogueEditorNodeType.Choice)
            {
                GUILayout.Space(5);
                GUILayout.Label("Speaker: ");
                inspector_scrollPos_0 = EditorGUILayout.BeginScrollView(inspector_scrollPos_0, GUILayout.Height(30));
                node.info.speaker = GUILayout.TextArea(node.info.speaker);
                EditorGUILayout.EndScrollView();
                GUILayout.Space(5);
                GUILayout.Label("Content: ");
                inspector_scrollPos_1 = EditorGUILayout.BeginScrollView(inspector_scrollPos_1, GUILayout.Height(60));
                node.info.content = GUILayout.TextArea(node.info.content);
                EditorGUILayout.EndScrollView();
            }
            else if (node.type == YXDialogueEditorNodeType.Event)
            {
                GUILayout.Space(8);
                GUILayout.Label("Event name: ");

                node.event_name = GUILayout.TextField(node.event_name);
                GUILayout.Label("Arg 0 (float): ");
                node.event_arg0 = EditorGUILayout.FloatField(node.event_arg0);
                GUILayout.Label("Arg 1 (float): ");
                node.event_arg1 = EditorGUILayout.FloatField(node.event_arg1);
            }
            else if (node.type == YXDialogueEditorNodeType.Set)
            {
                GUILayout.Space(8);
                GUILayout.Label("Variable name: ");

                node.int_property_name = GUILayout.TextField(node.int_property_name);
                GUILayout.Space(8);
                GUILayout.Label("Variable value: ");
                node.int_property_value = EditorGUILayout.IntField(node.int_property_value);
            }
            else if (node.type == YXDialogueEditorNodeType.If)
            {
                GUILayout.Space(8);
                GUILayout.Label("Variable name: ");
                node.if_property_name = GUILayout.TextField(node.if_property_name);
                GUILayout.Space(8);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Comparator: ");
                node.if_comparator = (YXDE_Comparator)EditorGUILayout.EnumPopup(node.if_comparator);
                GUILayout.EndHorizontal();
                GUILayout.Space(8);
                GUILayout.Label("Variable value: ");
                node.if_property_value = EditorGUILayout.IntField(node.if_property_value);
            }

            GUILayout.BeginArea(new Rect(sizeX / 2 - 25, sizeY - 100, 50, 120));
            if (GUILayout.Button("Close"))
            {
                inspectorNode = null;
            }
            GUILayout.EndArea();

            GUILayout.EndArea();
        }
        void DrawNode(YXDialogueEditorNode node, int id)
        {
            if (node == null)
                return;

            node.rect = GUI.Window(id, node.rect, DrawNodeWindow, node.name);

            if (node.type != YXDialogueEditorNodeType.Start &&
                node.type != YXDialogueEditorNodeType.Random &&
                node.type != YXDialogueEditorNodeType.End &&
                GUI.Button(new Rect(node.rect.xMin, node.rect.yMax + 5, node.rect.width, 15), "Edit", inspector_buttonStyle))
            {
                inspectorNode = node;
            }

            foreach (var link in node.linkedNodes)
            {
                DrawNodeCurve(node.rect, link.rect);
            }
        }

        void HandleMouseInput()
        {
            if (asset == null || IsMouseInInspector())
                return;

            Event e = Event.current;
            //right click
            if (e.type == EventType.MouseUp && e.button == 1)
            {
                YXDialogueEditorNode node = GetMouseOverNode();

                GenericMenu menu = new GenericMenu();
                if (node == null || node.type != YXDialogueEditorNodeType.End)
                {
                    //create nodes
                    menu.AddItem(new GUIContent("Create New Node/Sentence"), false, menu_AddNodeSentenceNode, (e.mousePosition, node));
                    menu.AddItem(new GUIContent("Create New Node/Choice"), false, menu_AddNodeOptionNode, (e.mousePosition, node));
                    menu.AddItem(new GUIContent("Create New Node/Event"), false, menu_AddNodeActionNode, (e.mousePosition, node));
                    menu.AddItem(new GUIContent("Create New Node/Random"), false, menu_AddNodeRandomNode, (e.mousePosition, node));
                    menu.AddSeparator("Create New Node/");
                    menu.AddItem(new GUIContent("Create New Node/Set"), false, menu_AddNodeSetNode, (e.mousePosition, node));
                    menu.AddItem(new GUIContent("Create New Node/If"), false, menu_AddNodeIfNode, (e.mousePosition, node));
                    menu.AddSeparator("Create New Node/");
                    menu.AddItem(new GUIContent("Create New Node/End"), false, menu_AddNodeEndNode, (e.mousePosition, node));
                }

                if (node != null)
                {
                    if (node.type != YXDialogueEditorNodeType.End)
                        menu.AddItem(new GUIContent("Link To..."), false, menu_ConnectTo, node);
                    if (node.type != YXDialogueEditorNodeType.End)
                        foreach (var link in node.linkedNodes)
                        {
                            menu.AddItem(new GUIContent($"Unlink.../{link.name}"), false, menu_Detach, (node, link));
                        }
                    if (node.type != YXDialogueEditorNodeType.Start)
                        menu.AddItem(new GUIContent("Delete"), false, menu_DeleteNode, node);
                }

                menu.ShowAsContext();
            }
        }

        void HandleConnectionState()
        {
            if (isConnecting)
            {
                Event e = Event.current;
                if (e != null && connectFromNode != null)
                {
                    Rect from = new Rect(connectFromNode.rect);
                    from.x -= 1000 - panX;
                    from.y -= 1000 - panY;
                    DrawNodeCurve(from, new Rect(e.mousePosition.x, e.mousePosition.y, 1, 1));
                }
                if (e != null && e.type == EventType.MouseUp)
                {
                    YXDialogueEditorNode node = GetMouseOverNode();
                    if (node == null)
                    {
                        isConnecting = false;
                        connectFromNode = null;
                        return;
                    }
                    else
                    {
                        LinkNodes(connectFromNode, node);
                        isConnecting = false;
                        connectFromNode = null;
                    }
                }
            }
        }
        void menu_Detach(object obj)
        {
            (YXDialogueEditorNode, YXDialogueEditorNode) nodes = ((YXDialogueEditorNode, YXDialogueEditorNode))obj;
            UnlinkNodes(nodes.Item1, nodes.Item2);
        }
        void menu_ConnectTo(object obj)
        {
            YXDialogueEditorNode node = (YXDialogueEditorNode)obj;
            connectFromNode = node;
            isConnecting = true;
        }
        void menu_DeleteNode(object obj)
        {
            YXDialogueEditorNode node = (YXDialogueEditorNode)obj;
            DeleteNode(node);
        }
        void menu_AddNodeSentenceNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.Sentence, value.Item1, value.Item2);
        }
        void menu_AddNodeOptionNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.Choice, value.Item1, value.Item2);
        }
        void menu_AddNodeActionNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.Event, value.Item1, value.Item2);
        }
        void menu_AddNodeRandomNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.Random, value.Item1, value.Item2);
        }
        void menu_AddNodeSetNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.Set, value.Item1, value.Item2);
        }
        void menu_AddNodeIfNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.If, value.Item1, value.Item2);
        }
        void menu_AddNodeEndNode(object obj)
        {
            (Vector2, YXDialogueEditorNode) value = ((Vector2, YXDialogueEditorNode))obj;
            AddNewNode(YXDialogueEditorNodeType.End, value.Item1, value.Item2);
        }
        YXDialogueEditorNode AddNewNode(YXDialogueEditorNodeType type, Vector2 position, YXDialogueEditorNode fromNode = null)
        {
            YXDialogueEditorNode node = new YXDialogueEditorNode();
            node.uid = Random.Range(1000, 1000000);
            if (type == YXDialogueEditorNodeType.Start)
                node.name = "Start";
            else if (type == YXDialogueEditorNodeType.Random)
                node.name = "Random";
            else if (type == YXDialogueEditorNodeType.Set)
                node.name = "Set";
            else if (type == YXDialogueEditorNodeType.If)
                node.name = "If";
            else if (type == YXDialogueEditorNodeType.End)
                node.name = "End";
            else
                node.name = type.ToString() + " " + nodes.Count;

            node.type = type;
            node.rect.x = position.x + 1000 - panX;
            node.rect.y = position.y + 1000 - panY;
            Vector2 size = node.GetSize();
            node.rect.width = size.x;
            node.rect.height = size.y;

            if (fromNode != null)
                LinkNodes(fromNode, node);
            nodes.Add(node);

            return node;
        }

        void DeleteNode(YXDialogueEditorNode node)
        {
            foreach (var from in node.linkedFromNodes)
            {
                from.linkedNodesID.Remove(node.uid);
            }
            foreach (var to in node.linkedNodes)
            {
                to.linkedNodesID.Remove(node.uid);
            }
            nodes.Remove(node);
        }

        void DrawNodeWindow(int id)
        {
            YXDialogueEditorNode node = nodes[id];
            GUI.DragWindow();

            if (node.type == YXDialogueEditorNodeType.Sentence)
            {
                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;


                GUILayout.Box("", node_yellowStyle, GUILayout.Width(500), GUILayout.Height(2));

                GUILayout.Space(8);
                GUILayout.Label(node.GetSpeakerString(), node_subtitle2Style);
                GUILayout.Space(8);

                GUILayout.TextArea(node.GetContentString(), node_subtitleStyle);
            }
            else if (node.type == YXDialogueEditorNodeType.Choice)
            {
                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_greenStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
                GUILayout.Space(5);
                GUILayout.Label(node.GetSpeakerString(), node_subtitle2Style);
                GUILayout.Space(5);

                GUILayout.TextArea(node.GetContentString(), node_subtitleStyle);
            }
            else if (node.type == YXDialogueEditorNodeType.Start)
            {
                GUI.contentColor = new Color(1, 1, 1, 0.07f);
                
                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_greenStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
            }
            else if (node.type == YXDialogueEditorNodeType.End)
            {
                GUI.contentColor = new Color(1, 1, 1, 0.07f);
                
                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_greenStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
            }
            else if (node.type == YXDialogueEditorNodeType.Event)
            {
                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_purpleStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
                GUILayout.Space(5);
                GUILayout.Label(node.event_name, node_subtitle2Style);
                GUILayout.Space(3);
                GUILayout.BeginHorizontal();
                GUILayout.Label(node.event_arg0.ToString("f2"), node_subtitleStyle);
                GUILayout.Label(node.event_arg1.ToString("f2"), node_subtitleStyle);
                GUILayout.EndHorizontal();
            }
            else if (node.type == YXDialogueEditorNodeType.Random)
            {
                GUI.contentColor = new Color(1, 1, 1, 0.07f);

                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_blueStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
            }
            else if (node.type == YXDialogueEditorNodeType.Set)
            {
                GUI.contentColor = new Color(1, 1, 1, .6f);

                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_greyStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
                GUILayout.Space(2);
                GUILayout.Label(node.int_property_name, node_centerStyle3);
                GUILayout.Space(3);
                GUILayout.Label("=", node_centerStyle2);
                GUILayout.Space(3);
                GUILayout.Label(node.int_property_value.ToString(), node_centerStyle2);
            }
            else if (node.type == YXDialogueEditorNodeType.If)
            {
                GUI.backgroundColor = new Color(.8f, .8f, .8f);
                GUI.contentColor = Color.white;

                GUILayout.BeginVertical();
                GUILayout.Box("", node_greyStyle, GUILayout.Width(500), GUILayout.Height(2));
                GUILayout.EndVertical();
                GUILayout.Space(2);
                GUILayout.Label(node.if_property_name, node_centerStyle3);
                GUILayout.Space(3);
                GUILayout.Label(node.if_comparator.ToString(), node_centerStyle);
                GUILayout.Space(3);
                GUILayout.Label(node.if_property_value.ToString(), node_centerStyle2);
            }
        }

        void DrawNodeCurve(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
            Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.gray, null, 1);
        }

        void UnlinkNodes(YXDialogueEditorNode from, YXDialogueEditorNode to)
        {
            from.linkedNodesID.Remove(to.uid);
            to.linkedFromNodesID.Remove(from.uid);
        }
        void LinkNodes(YXDialogueEditorNode from, YXDialogueEditorNode to)
        {
            from.linkedNodesID.Add(to.uid);
            to.linkedFromNodesID.Add(from.uid);
        }
        YXDialogueEditorNode GetMouseOverNode()
        {
            if (nodes == null) return null;
            foreach (var node in nodes)
            {
                if (IsMouseInRect(node.rect))
                    return node;
            }
            return null;
        }
        bool IsMouseInInspector()
        {
            Rect rect = new Rect(0, position.yMax - 300, 200, 300);
            Event e = Event.current;
            return rect.Contains(e.mousePosition);
        }
        bool IsMouseInRect(Rect rect)
        {
            Event e = Event.current;
            return rect.Contains(e.mousePosition - new Vector2(panX - 1000, panY - 1000));
        }
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace YXCell
{
    [AddComponentMenu("YXCell/Dialogue/YXDialoguePlayer")]
    public class YXDialoguePlayer : MonoBehaviour
    {
        public YXDialogueAsset asset;
        public bool playOnStart = false;
        [Header("Scene Components")]
        public GameObject panel;
        public YXText contentText;
        public YXText speakerText;
        public GameObject choicePanel;
        public GameObject[] choiceObjects;
        public YXText[] choiceTexts;
        public Image speakerImage;
        [Header("Buttons")]
        public Button contentButton;
        public Button[] choiceButtons;

        [Header("Events")]
        public UnityEvent onDialogueEnded;
        public UnityEvent onDialogueStart;

        private YXTextAnimator textAnimator;
        private YXDialogueEditorNode curNode, curSentence;
        private List<YXDialogueEditorNode> curChoices;
        private bool curSentenceLinkExecuted = false;

        private void Start()
        {
            //textAnimator = contentText.GetComponent<YXTextAnimator>();
            contentButton.onClick.AddListener(OnContentButtonPressed);
            if (playOnStart)
                Play();
        }
        private void OnApplicationQuit()
        {
            if (asset != null)
            {
                asset.event_properties = new List<YXDE_EventProperty>();
                asset.int_properties = new List<YXDE_IntProperty> { };
            }
        }

        #region Public Methods

        public void Play()
        {
            if (asset == null) return;
            YXDialogueManager.curEditorAsset = asset;
            curNode = asset.editorNodes[0];
            choicePanel.SetActive(false);

            if (curChoices != null)
                curChoices.Clear();
            if (curNode.type != YXDialogueEditorNodeType.Start)
            {
                YXUtils.EditorLogError("YXDialoguePlayer: First node is not start. Please make sure there is a start node to begin with.");
            }
            panel.SetActive(true);
            StartPlay();
        }

        public void Play(YXDialogueAsset asset)
        {
            this.asset = asset;
            Play();
        }


        public void SentenceNextStep()
        {
            OnContentButtonPressed();
        }

        #endregion

        #region Private Methods

        private void OnEndSequence()
        {
            panel.SetActive(false);
            onDialogueEnded.Invoke();
        }
        private void ExecuteNode(YXDialogueEditorNode node)
        {
            curNode = node;
            switch (node.type)
            {
                case YXDialogueEditorNodeType.Sentence:
                    if (node != curSentence)
                        curSentenceLinkExecuted = false;

                    curSentence = node;
                    contentText.text = node.info.content;
                    speakerText.text = node.info.speaker;

                    if (node.info.avatar != null)
                    {
                        speakerImage.sprite = Sprite.Create(node.info.avatar, new Rect(0, 0, node.info.avatar.width, node.info.avatar.height), Vector2.zero);
                    }
                    GotoChoices(curSentence);
                    ExecuteInstantNodes(curSentence);
                    break;
                case YXDialogueEditorNodeType.Choice:
                    break;
                case YXDialogueEditorNodeType.Random:
                    int count = node.linkedNodesID.Count;
                    ExecuteNode(node.linkedNodes[Random.Range(0, count)]);
                    break;
                case YXDialogueEditorNodeType.Event:
                    bool isNew = true;
                    foreach (var item in asset.event_properties)
                    {
                        if (item.name.Equals(node.event_name))
                        {
                            isNew = false;
                            item.action.Invoke(node.event_arg0, node.event_arg1);
                        }
                    }
                    if (isNew)
                    {
                        YXDE_EventProperty e = new YXDE_EventProperty()
                        {
                            name = node.event_name,
                            action = new System.Action<float, float>((a, b) => { }),
                        };
                        asset.event_properties.Add(e);
                        e.action.Invoke(node.event_arg0, node.event_arg1);
                    }
                    break;
                case YXDialogueEditorNodeType.Start:
                    ExecuteLinkedNodes(curNode);
                    break;
                case YXDialogueEditorNodeType.Set:
                    isNew = true;
                    foreach (var item in asset.int_properties)
                    {
                        if (item.name.Equals(node.int_property_name))
                        {
                            isNew = false;
                            item.value = node.int_property_value;
                        }
                    }
                    if (isNew)
                    {
                        asset.int_properties.Add(new YXDE_IntProperty()
                        {
                            name = node.int_property_name,
                            value = node.int_property_value,
                        });
                    }
                    break;
                case YXDialogueEditorNodeType.If:
                    bool success = true;
                    foreach (var item in asset.int_properties)
                    {
                        if (item.name.Equals(node.if_property_name))
                        {
                            switch (node.if_comparator)
                            {
                                case YXDE_Comparator.Equals:
                                    if (item.value != node.if_property_value)
                                        success = false;
                                    break;
                                case YXDE_Comparator.GreaterThan:
                                    if (item.value <= node.if_property_value)
                                        success = false;
                                    break;
                                case YXDE_Comparator.LessThan:
                                    if (item.value >= node.if_property_value)
                                        success = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (success)
                    {
                        ExecuteLinkedNodes(curNode);
                    }
                    break;
                case YXDialogueEditorNodeType.End:
                    OnEndSequence();
                    break;
                default:
                    break;
            }
        }
        private void StartPlay()
        {
            onDialogueStart.Invoke();
            ExecuteNode(curNode);
        }

        private void OnContentButtonPressed()
        {
            if (curSentence.linkedNodesID.Count == 0)
            {
                OnEndSequence();
            }

            if (!curSentenceLinkExecuted)
            {
                curSentenceLinkExecuted = true;
                ExecuteLinkedNodesExclueInstant(curSentence);
            }
        }
        private void OnChoiceButtonPressed(int id)
        {
            choicePanel.SetActive(true);
            ExecuteLinkedNodes(curChoices[id]);
        }
        private void ExecuteChoices(List<YXDialogueEditorNode> choices)
        {
            curChoices = choices;
            choicePanel.SetActive(choices.Count >= 0);

            foreach (var go in choiceObjects)
                go.SetActive(false);

            for (int i = 0; i < choices.Count; i++)
            {
                choiceObjects[i].SetActive(true);

                choiceTexts[i].text = choices[i].info.content;

                int j = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() =>
                {
                    OnChoiceButtonPressed(j);
                });
            }
        }

        private void GotoChoices(YXDialogueEditorNode node)
        {
            if (node.linkedNodesID.Count == 0) return;

            List<YXDialogueEditorNode> choices = new List<YXDialogueEditorNode>();
            foreach (var link in node.linkedNodes)
            {
                if (link.type == YXDialogueEditorNodeType.Choice)
                {
                    choices.Add(link);
                }
            }
            ExecuteChoices(choices);
        }

        private void ExecuteLinkedNodes(YXDialogueEditorNode node)
        {
            List<YXDialogueEditorNode> list = new List<YXDialogueEditorNode>();
            foreach (var link in node.linkedNodes)
            {
                if (link.type == YXDialogueEditorNodeType.Set || link.type == YXDialogueEditorNodeType.Event)
                {
                    list.Insert(0, link);
                }
                else if (link.type != YXDialogueEditorNodeType.Choice)
                {
                    list.Add(link);
                }
            }
            foreach (var link in list)
            {
                ExecuteNode(link);
            }
        }

        private void ExecuteInstantNodes(YXDialogueEditorNode node)
        {
            if (node.type != YXDialogueEditorNodeType.Sentence) return;
            if (node.linkedNodesID.Count == 0) return;
            List<YXDialogueEditorNode> list = new List<YXDialogueEditorNode>();
            foreach (var link in node.linkedNodes)
            {
                if (link.type == YXDialogueEditorNodeType.Set || link.type == YXDialogueEditorNodeType.Event)
                {
                    list.Add(link);
                }
            }
            foreach (var link in list)
            {
                ExecuteNode(link);
            }
        }

        private void ExecuteLinkedNodesExclueInstant(YXDialogueEditorNode node)
        {
            List<YXDialogueEditorNode> list = new List<YXDialogueEditorNode>();
            foreach (var link in node.linkedNodes)
            {
                if (link.type == YXDialogueEditorNodeType.Set || link.type == YXDialogueEditorNodeType.Event)
                {

                }
                else if (link.type != YXDialogueEditorNodeType.Choice)
                {
                    list.Add(link);
                }
            }
            foreach (var link in list)
            {
                ExecuteNode(link);
            }
        }
        #endregion
    }
}

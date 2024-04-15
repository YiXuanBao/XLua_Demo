using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YXCell
{
    public class MessageBox
    {
        public static MessageBox CreatMessageBox(string messageInfo, string firstText, string secondText)
        {
            MessageBox box = new MessageBox(messageInfo, firstText, secondText);
            return box;
        }

        public enum BoxResult
        {
            None,
            First,
            Second,
        }

        GameObject go;
        BoxResult Result;

        private MessageBox(string messageInfo, string firstText, string secondText)
        {
            UnityEngine.Object asset = Resources.Load("Prefabs/MessageBox");

            go = GameObject.Instantiate(asset) as GameObject;

            go.transform.Find("Bg/MessageBox/MessageInfo").GetComponent<TMPro.TMP_Text>().text = messageInfo;

            Transform first = go.transform.Find("Bg/MessageBox/First");

            first.Find("Text").GetComponent<TMPro.TMP_Text>().text = firstText;

            first.GetComponent<Button>().onClick.AddListener(() =>
            {
                Result = BoxResult.First;
            });

            Transform second = go.transform.Find("Bg/MessageBox/Second");

            second.Find("Text").GetComponent<TMPro.TMP_Text>().text = secondText;

            second.GetComponent<Button>().onClick.AddListener(() =>
            {
                Result = BoxResult.Second;
            });
        }

        public async Task<BoxResult> GetReplyAxync()
        {
            return await Task.Run<BoxResult>(() =>
            {
                while (true)
                {
                    if (BoxResult.None != Result)
                    {
                        return Result;
                    }
                }
            });
        }

        public void Close()
        {
            GameObject.Destroy(go);
        }
    }
}
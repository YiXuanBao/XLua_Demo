using UnityEngine;

namespace YXCell.Test
{
    public class Input_DialogueScene : MonoBehaviour
    {
        [SerializeField] YXDialoguePlayer player;
        void Start()
        {
            player.Play();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Novel.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public TMP_Text Text;

        public void SetDialogueText(string text)
        {
            Text.text = text;
        }
    }
}

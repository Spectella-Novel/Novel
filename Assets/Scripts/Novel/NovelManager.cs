using Novel.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Novel
{
    public class NovelManager : MonoBehaviour
    {
        // ==LAYERS==
        private GameObject Background;
        private GameObject Characters;
        private DialogueManager Dialogue;
        private GameObject Controls;
        // ==LAYERS==

        public void ShowDialogue(string Text)
        {
            Dialogue.SetDialogueText(Text);
        }
    }
}

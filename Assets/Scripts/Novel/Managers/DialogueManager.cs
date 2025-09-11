using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Novel.Managers
{
    public class DialogueManager
    {
        public TextMeshPro Text;

        public void SetDialogueText(string text)
        {
            Text.text = text;
        }
    }
}

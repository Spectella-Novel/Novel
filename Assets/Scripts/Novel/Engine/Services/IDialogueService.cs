using RenDisco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Engine.Services
{
    internal interface IDialogueService
    {
        public void ShowDialogue(string text, string? character = null);
        public void ShowChoices(List<MenuChoice> choices);

    }
}

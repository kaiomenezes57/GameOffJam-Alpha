using System;

namespace Game.Core.Dialogue
{
    public interface IDialogueViewUI
    {
        event Action OnNextDialogueRequested;

        void ShowLine(string characterName, string dialogueText);
        void SetNextDialogueButton(bool active);
        void Hide();
    }
}

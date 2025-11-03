using Game.Core.Dialogue;
using Game.Core.Extensions;
using Game.Core.GameState;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Domains.Dialogue
{
    public sealed class DialogueManager : IDialogueManager, IDisposable
    {
        private readonly IDialogueViewUI _dialogueViewUI;
        private readonly IDialogueAudioService _dialogueAudioService;
        private readonly IGameStateHandler _gameStateHandler;
        private readonly Queue<string> _dialogueLines;

        [Inject]
        public DialogueManager(IDialogueViewUI dialogueUI, 
            IDialogueAudioService dialogueAudioService, 
            IGameStateHandler gameStateHandler)
        {
            _dialogueViewUI = dialogueUI;
            _dialogueAudioService = dialogueAudioService;
            _gameStateHandler = gameStateHandler;
            
            _dialogueLines = new Queue<string>();

            _dialogueAudioService.OnLineEnd += GoToNextLine;
            _dialogueViewUI.OnNextDialogueRequested += GoToNextLine;
        }
        
        public async void StartDialogue(DialogueData content)
        {
            var entries = await content.DialogueTable.GetAllEntries();
            if (entries.Count == 0)
            {
#if DEBUG
                Debug.LogWarning("[Dialogue Manager] No dialogue lines found in the provided DialogueContent.");
#endif
                return;
            }

            _dialogueLines.Clear();
            _gameStateHandler.Change(new Dialogue_GameState(), this);

            foreach (var entry in entries)
                _dialogueLines.Enqueue(entry);

            bool hasAudio = !content.Audio.IsNull;
            _dialogueViewUI.SetNextDialogueButton(!hasAudio);
            if (hasAudio)
                _dialogueAudioService.Play(content.Audio);

            GoToNextLine();
        }

        private void GoToNextLine()
        {
            if (_dialogueLines.Count == 0)
            {
                EndDialogue();
                return;
            }

            string line = _dialogueLines.Dequeue();
            string characterName = line.GetSubstringBetween("[", "]").Trim();
            string dialogueText = line[(line.IndexOf("]") + 1)..].Trim();

            _dialogueViewUI.ShowLine(characterName, dialogueText);
        }

        private void EndDialogue()
        {
            _dialogueViewUI.Hide();
            _dialogueAudioService.Stop();

            _gameStateHandler.BackToPrevious(this);
        }

        public void Dispose()
        {
            _dialogueAudioService.OnLineEnd -= GoToNextLine;
            _dialogueViewUI.OnNextDialogueRequested -= GoToNextLine;
        }
    }
}
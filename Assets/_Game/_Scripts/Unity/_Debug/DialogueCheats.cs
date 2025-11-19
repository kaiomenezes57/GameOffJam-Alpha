using Game.Core.Dialogue;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Game.Unity.Debug
{
    public sealed class DialogueCheats : MonoBehaviour
    {
        [Inject] private readonly IDialogueSkip _dialogueSkip;

        private void Update()
        {
            if (Keyboard.current.f1Key.wasPressedThisFrame)
                _dialogueSkip?.Skip();
        }
    }
}

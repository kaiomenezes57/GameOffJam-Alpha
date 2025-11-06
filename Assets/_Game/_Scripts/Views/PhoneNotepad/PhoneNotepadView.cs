using Game.Core.PhoneNotepad;
using TMPro;
using UnityEngine;

namespace Game.Views.PhoneNotepad
{
    public sealed class PhoneNotepadView : MonoBehaviour, IPhoneNotepadView
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            if (string.IsNullOrEmpty(_text.text))
                Refresh(new string[] { "..." });
        }

        public void Refresh(string[] toDoList)
        {
            _text.text = string.Empty;
            
            foreach (var task in toDoList)
                _text.text += $"• {task}\n";
        }
    }
}

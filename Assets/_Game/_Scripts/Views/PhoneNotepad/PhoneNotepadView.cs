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
                _text.text = "...";
        }

        public void Refresh(string[] toDoList)
        {
            _text.text = string.Empty;

            for (int i = 0; i < toDoList.Length; i++)
            {
                string index = $"{i + 1}";
                string task = toDoList[i];

                _text.text += $"{index}. {task}\n";
            }
        }
    }
}

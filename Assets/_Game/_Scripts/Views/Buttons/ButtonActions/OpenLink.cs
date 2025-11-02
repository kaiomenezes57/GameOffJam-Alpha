using UnityEngine;

namespace Game.Views.Buttons
{
    [System.Serializable]
    public sealed class OpenLink : IButtonAction
    {
        [SerializeField] private string _link;

        public void OnClick()
        {
            if (string.IsNullOrEmpty(_link)) return;
            Application.OpenURL(_link);
        }
    }
}

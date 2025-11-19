using Game.Core.Showcase;
using UnityEngine;

namespace Game.Unity.Buttons
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

using UnityEngine;

namespace Game.Unity.Buttons
{
    [System.Serializable]
    public sealed class OpenLink : BaseButtonAction
    {
        [SerializeField] private string _link;

        public override void OnClick()
        {
            if (string.IsNullOrEmpty(_link)) return;
            Application.OpenURL(_link);
        }
    }
}

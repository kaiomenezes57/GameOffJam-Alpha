using Game.Unity.Showcase;
using UnityEngine;

namespace Game.Unity.Buttons
{
    public sealed class CloseShowcaseScreen : IButtonAction
    {
        [SerializeField] private ShowcaseText _showcaseText;

        public void OnClick()
        {
            _showcaseText.Hide();
        }
    }
}

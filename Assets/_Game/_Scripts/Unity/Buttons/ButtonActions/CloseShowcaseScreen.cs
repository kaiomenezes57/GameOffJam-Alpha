using Game.Unity.Showcase;
using UnityEngine;

namespace Game.Unity.Buttons
{
    public sealed class CloseShowcaseScreen : BaseButtonAction
    {
        [SerializeField] private ShowcaseText _showcaseText;

        public override void OnClick()
        {
            _showcaseText.Hide();
        }
    }
}

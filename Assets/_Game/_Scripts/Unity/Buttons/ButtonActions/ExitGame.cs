using UnityEngine;

namespace Game.Unity.Buttons
{
    public sealed class ExitGame : BaseButtonAction
    {
        public override void OnClick()
        {
            Application.Quit();
        }
    }
}

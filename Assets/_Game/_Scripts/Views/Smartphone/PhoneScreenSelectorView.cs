using Game.Core.Smartphone;
using System.Linq;
using UnityEngine;

namespace Game.Views.Smartphone
{
    public sealed class PhoneScreenSelectorView : MonoBehaviour, IPhoneScreenSelectorView
    {
        [SerializeField] private PhoneScreenData[] _screens;

        private void Start()
        {
            HideAllScreens();
        }

        public void ShowScreen(PhoneScreenType screenType)
        {
            foreach (var screen in _screens)
                screen.ScreenObject.SetActive(screen.ScreenType == screenType);
        }

        public void HideAllScreens()
        {
            foreach (var screen in _screens.Select(x => x.ScreenObject))
                screen.SetActive(false);
        }
    }
}

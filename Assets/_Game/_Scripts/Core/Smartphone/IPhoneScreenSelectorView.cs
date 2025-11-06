namespace Game.Core.Smartphone
{
    public interface IPhoneScreenSelectorView
    {
        void ShowScreen(PhoneScreenType screenType);
        void HideAllScreens();  
    }
}
namespace Game.Core.Smartphone
{
    public interface IPhoneManager
    {
        void SetNextMode(PhoneScreenType mode);
        void SetCanPutDownPhone();
    }
}
using VContainer;

namespace Game.Unity.Buttons
{
    public interface IButtonAction
    {
        void Inject(IObjectResolver resolver);
        void OnClick();
    }
}

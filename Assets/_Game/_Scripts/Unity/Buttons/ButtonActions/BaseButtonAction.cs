using VContainer;

namespace Game.Unity.Buttons
{
    public abstract class BaseButtonAction : IButtonAction
    {
        public virtual void Inject(IObjectResolver resolver)
        {
        }

        public abstract void OnClick();
    }
}

using Game.Core.Extensions;
using Game.Unity.Buttons;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public sealed class MainMenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterGameObjectsOfType<ButtonBehaviour>(ref autoInjectGameObjects);
        }
    }
}

using Game.Core.Extensions;
using Game.Unity.SplashArt;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public class SplashArtLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterGameObjectsOfType<SplashArtAnimation>(ref autoInjectGameObjects);
        }
    }
}

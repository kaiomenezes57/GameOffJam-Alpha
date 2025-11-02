using Game.Domains.GameState;
using Game.Core.GameState;
using VContainer.Unity;
using VContainer;
using Game.Core.Scene;
using Game.Domains.Scene;

namespace Game.Services.DI
{
    public sealed class BootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStateHandler>(Lifetime.Singleton)
                .As<IGameStateHandler>()
                .AsSelf();

            builder.Register<SceneController>(Lifetime.Singleton)
                .As<ISceneController>()
                .AsSelf();
        }
    }
}

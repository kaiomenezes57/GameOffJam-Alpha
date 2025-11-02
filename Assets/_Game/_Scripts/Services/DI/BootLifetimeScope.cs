using Game.Domains.GameState;
using Game.Core.GameState;
using VContainer.Unity;
using VContainer;
using Game.Core.Scene;
using Game.Domains.Scene;
using Game.Views.FadeTransition;
using UnityEngine;
using Game.Core.FadeTransition;
using Sirenix.OdinInspector;
using Game.Core.Scene.Data;

namespace Game.Services.DI
{
    public sealed class BootLifetimeScope : LifetimeScope
    {
        [Title("References")]
        [SerializeField] private FadeTransitionUI _fadeTransitionPrefab;
        [SerializeField] private SceneDataSO _firstScene;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStateHandler>(Lifetime.Singleton)
                .As<IGameStateHandler>()
                .AsSelf();

            builder.RegisterComponentInNewPrefab(_fadeTransitionPrefab, Lifetime.Singleton)
                .As<IFadeTransition>()
                .AsSelf();

            builder.Register<SceneController>(Lifetime.Singleton)
                .As<ISceneController>()
                .AsSelf();
        }

        private void Start()
        {
            var sceneController = Container.Resolve<ISceneController>();
            sceneController?.LoadScene(_firstScene);
        }
    }
}

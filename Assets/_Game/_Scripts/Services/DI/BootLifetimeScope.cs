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
            builder.Register<IGameStateHandler, GameStateHandler>(Lifetime.Singleton);
            builder.Register<ISceneController, SceneController>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_fadeTransitionPrefab, Lifetime.Singleton)
                .As<IFadeTransition>()
                .AsSelf();
        }

        private void Start()
        {
            var sceneController = Container.Resolve<ISceneController>();
            sceneController?.LoadScene(_firstScene);
        }
    }
}

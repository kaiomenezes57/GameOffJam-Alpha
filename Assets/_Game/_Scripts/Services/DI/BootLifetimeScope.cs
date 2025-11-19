using Game.Application.GameState;
using Game.Core.GameState;
using VContainer.Unity;
using VContainer;
using Game.Core.Scene;
using Game.Application.Scene;
using Game.Unity.FadeTransition;
using UnityEngine;
using Game.Core.FadeTransition;
using Sirenix.OdinInspector;
using Game.Core.Scene.Data;
using UnityEngine.SceneManagement;
using Game.Unity.Debug;

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

#if DEBUG
            builder.RegisterComponentInHierarchy<DebugInformation>();
#endif
        }

        private void Start()
        {
#if DEBUG
            if (SceneManager.sceneCount > 1)
            {
                var gameStateHandler = Container.Resolve<IGameStateHandler>();
                gameStateHandler?.TryChange(new Gameplay_GameState(), this);
                return;
            }
#endif

            var sceneController = Container.Resolve<ISceneController>();
            sceneController?.LoadScene(_firstScene);
        }
    }
}

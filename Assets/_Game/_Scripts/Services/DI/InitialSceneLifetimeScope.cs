using Game.Core.Dialogue;
using Game.Core.Extensions;
using Game.Application.Dialogue;
using Game.Application.Trigger;
using Game.Services.Audio;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public sealed class InitialSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Dialogue Services
            builder.Register<IDialogueAudioService, DialogueAudioService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IDialogueViewUI>();
            builder.Register<IDialogueManager, DialogueManager>(Lifetime.Singleton);

            builder.RegisterGameObjectsOfType<BaseGameTrigger>(ref autoInjectGameObjects);
        }
    }
}

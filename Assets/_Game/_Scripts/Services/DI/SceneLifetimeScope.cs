using Game.Domains.Trigger;
using VContainer.Unity;
using VContainer;
using Game.Core.Dialogue;
using Game.Domains.Dialogue;
using Game.Services.Audio;
using Game.Views.Dialogue;
using UnityEngine;

namespace Game.Services.DI
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private DialogueViewUI _dialogueUI;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDialogueAudioService, DialogueAudioService>(Lifetime.Singleton);
            builder.RegisterInstance<IDialogueViewUI>(_dialogueUI);
            builder.Register<IDialogueManager, DialogueManager>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<BaseGameTrigger>();
        }
    }
}

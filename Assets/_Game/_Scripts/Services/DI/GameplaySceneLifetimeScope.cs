using Game.Application.Dialogue;
using Game.Application.Fish;
using Game.Application.MessageChat;
using Game.Application.PhoneNotepad;
using Game.Application.Trigger;
using Game.Application.UINotification;
using Game.Core.Dialogue;
using Game.Core.Extensions;
using Game.Core.Fish;
using Game.Core.Interaction;
using Game.Core.MessageChat;
using Game.Core.PhoneNotepad;
using Game.Core.Showcase;
using Game.Core.Smartphone;
using Game.Core.Telephone;
using Game.Core.UINotification;
using Game.Services.Audio;
using Game.Unity.Fish;
using Game.Unity.Showcase;
using VContainer;
using VContainer.Unity;

namespace Game.Services.DI
{
    public sealed class GameplaySceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Dialogue Services
            builder.Register<IDialogueAudioService, DialogueAudioService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IDialogueViewUI>();
            builder.Register<DialogueManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            // Message Chat Services
            builder.RegisterComponentInHierarchy<IPlayerInputChatMessageViewUI>();
            builder.RegisterComponentInHierarchy<IMessageChatViewUI>();
            builder.Register<IMessageChatManager, MessageChatManager>(Lifetime.Singleton);

            // UI Notification Services
            builder.RegisterComponentInHierarchy<IUINotificationView>();
            builder.Register<IUINotificationManager, UINotificationManager>(Lifetime.Singleton);

            // Showcase Services
            builder.RegisterComponentInHierarchy<ShowcaseText>()
                .AsImplementedInterfaces();

            // Phone Services
            builder.RegisterComponentInHierarchy<IPhoneManager>();
            builder.RegisterComponentInHierarchy<IPhoneScreenSelectorView>();

            // Phone notepad Services
            builder.RegisterComponentInHierarchy<IPhoneNotepadView>();
            builder.Register<IPhoneNotepadManager, PhoneNotepadManager>(Lifetime.Singleton);

            // Telephone Services
            builder.RegisterComponentInHierarchy<ITelephone>();

            // Factory Services
            builder.Register<IFishStateMachineFactory, FishStateMachineFactory>(Lifetime.Singleton);

            // GameObject registrations
            builder.RegisterGameObjectsOfType<BaseGameTrigger>(ref autoInjectGameObjects);
            builder.RegisterGameObjectsOfType<BaseInteractable>(ref autoInjectGameObjects);
            builder.RegisterGameObjectsOfType<FishController>(ref autoInjectGameObjects);

#if DEBUG
            builder.RegisterGameObjectsOfType<Unity.Debug.DialogueCheats>(ref autoInjectGameObjects);
#endif
        }
    }
}

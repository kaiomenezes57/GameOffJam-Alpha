using Game.Domains.Trigger;
using VContainer.Unity;
using VContainer;
using Game.Core.Dialogue;
using Game.Domains.Dialogue;
using Game.Services.Audio;
using UnityEngine;
using Game.Views.Debug;
using Game.Core.MessageChat;
using Game.Domains.MessageChat;
using System.Collections.Generic;
using Game.Core.UINotification;
using Game.Domains.UINotification;
using Game.Core.Smartphone;
using Game.Core.PhoneNotepad;
using Game.Domains.PhoneNotepad;
using Game.Core.Telephone;
using Game.Core.Extensions;
using Game.Core.Interaction;

namespace Game.Services.DI
{
    public sealed class GameplaySceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Dialogue Services
            builder.Register<IDialogueAudioService, DialogueAudioService>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<IDialogueViewUI>();
            builder.Register<IDialogueManager, DialogueManager>(Lifetime.Singleton);

            // Message Chat Services
            builder.RegisterComponentInHierarchy<IPlayerInputChatMessageViewUI>();
            builder.RegisterComponentInHierarchy<IMessageChatViewUI>();
            builder.Register<IMessageChatManager, MessageChatManager>(Lifetime.Singleton);

            // UI Notification Services
            builder.RegisterComponentInHierarchy<IUINotificationView>();
            builder.Register<IUINotificationManager, UINotificationManager>(Lifetime.Singleton);

            // Phone Services
            builder.RegisterComponentInHierarchy<IPhoneManager>();
            builder.RegisterComponentInHierarchy<IPhoneScreenSelectorView>();

            // Phone notepad Services
            builder.RegisterComponentInHierarchy<IPhoneNotepadView>();
            builder.Register<IPhoneNotepadManager, PhoneNotepadManager>(Lifetime.Singleton);

            // Telephone Services
            builder.RegisterComponentInHierarchy<ITelephone>();

            // GameObject registrations
            builder.RegisterGameObjectsOfType<BaseGameTrigger>(ref autoInjectGameObjects);
            builder.RegisterGameObjectsOfType<BaseInteractable>(ref autoInjectGameObjects);
#if DEBUG
            builder.RegisterComponentInHierarchy<DebugInformation>();
#endif
        }
    }
}

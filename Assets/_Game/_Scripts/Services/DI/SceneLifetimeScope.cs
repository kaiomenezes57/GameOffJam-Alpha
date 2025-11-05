using Game.Domains.Trigger;
using VContainer.Unity;
using VContainer;
using Game.Core.Dialogue;
using Game.Domains.Dialogue;
using Game.Services.Audio;
using Game.Views.Dialogue;
using UnityEngine;
using Game.Views.Debug;
using Game.Views.MessageChat;
using Game.Core.MessageChat;
using Game.Domains.MessageChat;
using System.Collections.Generic;

namespace Game.Services.DI
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private DialogueViewUI _dialogueUI;
        [SerializeField] private MessageChatViewUI _messageChatUI;
        [SerializeField] private PlayerInputChatMessageViewUI _playerInputChatMessageUI;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDialogueAudioService, DialogueAudioService>(Lifetime.Singleton);
            builder.RegisterInstance<IDialogueViewUI>(_dialogueUI);
            builder.Register<IDialogueManager, DialogueManager>(Lifetime.Singleton);

            builder.RegisterInstance<IPlayerInputChatMessageViewUI>(_playerInputChatMessageUI);
            builder.RegisterInstance<IMessageChatViewUI>(_messageChatUI);
            builder.Register<IMessageChatManager, MessageChatManager>(Lifetime.Singleton);

            RegisterAllGameObjects<BaseGameTrigger>();
#if DEBUG
            builder.RegisterComponentInHierarchy<DebugInformation>();
#endif
        }

        private void RegisterAllGameObjects<T>() where T : MonoBehaviour
        {
            autoInjectGameObjects ??= new List<GameObject>();
            var monobehaviours = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (var mono in monobehaviours)
            {
                if (mono.gameObject == null)
                    continue;
                autoInjectGameObjects.Add(mono.gameObject);
            }
        }
    }
}

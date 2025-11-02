using UnityEngine;
using Game.Core.Trigger;
using Sirenix.Serialization;

namespace Game.Domains.Trigger
{
    public abstract class BaseGameTrigger : MonoBehaviour, IGameTrigger
    {
        [OdinSerialize, SerializeReference] private ITriggerAction[] _actions;

        public void TriggerActions()
        {
            foreach (var action in _actions)
                action.Trigger(destroyCancellationToken);
        }
    }
}
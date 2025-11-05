using Game.Core.Extensions;
using UnityEngine;

namespace Game.Domains.Trigger
{
    public sealed class OnTriggerExit_GameTrigger : BaseGameTrigger
    {
        [SerializeField] private string _tag = "Player";

        private void Awake()
        {
            if (!gameObject.TryGetOrAdd<BoxCollider>(out var boxCollider))
            {
#if DEBUG
                Debug.LogError("[OVERLAP TRIGGER] No collider found.");
#endif
                return;
            }

            boxCollider.isTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null || !other.CompareTag(_tag))
                return;
            TriggerActions();
        }
    }
}
using Game.Core.Extensions;
using UnityEngine;

namespace Game.Domains.Trigger
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class OnOverlap_GameTrigger : BaseGameTrigger
    {
        [SerializeField] private string _tag = "Player";

        private void Start()
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

        private void OnTriggerEnter(Collider other)
        {
            if (other == null || !other.CompareTag(_tag))
                return;
            TriggerActions();
        }
    }
}

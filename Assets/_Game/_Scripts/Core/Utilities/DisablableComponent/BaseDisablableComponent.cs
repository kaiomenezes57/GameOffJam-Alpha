using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Core.Utilities.DisablableComponent
{
    public abstract class BaseDisablableComponent : MonoBehaviour, IDisablableComponent
    {
        [field: SerializeField, ReadOnly] protected bool Enabled { get; private set; }

        protected virtual void Awake()
        {
            Switch(true);
        }

        public void Switch(bool enabled)
        {
            Enabled |= enabled;
        }
    }
}

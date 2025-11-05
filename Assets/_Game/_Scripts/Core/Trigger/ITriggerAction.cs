using System.Threading;
using UnityEngine;
using VContainer;

namespace Game.Core.Trigger
{
    public interface ITriggerAction
    {
        bool Ready { get; }
        void Inject(GameObject triggerGO, IObjectResolver objectResolver);
        void Trigger(CancellationToken token);
    }
}

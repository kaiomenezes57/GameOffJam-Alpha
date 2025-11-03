using System.Threading;
using VContainer;

namespace Game.Core.Trigger
{
    public interface ITriggerAction
    {
        bool Ready { get; }
        void Inject(IObjectResolver objectResolver);
        void Trigger(CancellationToken token);
    }
}

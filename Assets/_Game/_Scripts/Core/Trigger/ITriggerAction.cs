using System.Threading;

namespace Game.Core.Trigger
{
    public interface ITriggerAction
    {
        void Trigger(CancellationToken token);
    }
}

namespace Game.Domains.Trigger
{
    public sealed class OnEnable_GameTrigger : BaseGameTrigger
    {
        private void OnEnable()
        {
            TriggerActions();
        }
    }
}
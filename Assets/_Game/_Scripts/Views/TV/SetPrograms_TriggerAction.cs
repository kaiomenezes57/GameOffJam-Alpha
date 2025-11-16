using Game.Core.Trigger;
using UnityEngine;
using UnityEngine.Localization;

namespace Game.Views.TV
{
    public sealed class SetPrograms_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private CentralTVController _centralTVController;
        [SerializeField] private LocalizedString _previousProgram;
        [SerializeField] private LocalizedString _currentProgram;
        [SerializeField] private LocalizedString _nextProgram;

        protected override void OnTriggered()
        {
            _centralTVController?.SetPrograms(_previousProgram,
                _currentProgram,
                _nextProgram);
        }
    }
}

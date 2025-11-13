using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Domains.Trigger
{
    public sealed class OnValueAchieved_GameTrigger : BaseGameTrigger
    {
        [SerializeField, MinValue(1)] private int _targetValue = 1;
        [SerializeField] private bool _incrementOncePerSource = true;
        private readonly List<GameObject> _incrementSources = new();
        private int _currentValue;
        
        public void IncrementValue(GameObject incrementSource)
        {
            if (_incrementOncePerSource)
            {
                if (_incrementSources.Contains(incrementSource))
                    return;

                _incrementSources.Add(incrementSource);
            }

            _currentValue++;
            
            if (_currentValue >= _targetValue)
            {
                TriggerActions();
                _currentValue = 0;
            }
        }
    }
}
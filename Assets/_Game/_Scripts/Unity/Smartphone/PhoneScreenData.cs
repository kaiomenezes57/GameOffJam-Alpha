using Game.Core.Smartphone;
using UnityEngine;

namespace Game.Unity.Smartphone
{
    [System.Serializable]
    public struct PhoneScreenData
    {
        [field: SerializeField] public PhoneScreenType ScreenType { get; private set; }
        [field: SerializeField] public GameObject ScreenObject { get; private set; }
    }
}

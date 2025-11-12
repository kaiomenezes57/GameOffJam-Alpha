using UnityEngine;

namespace Game.Core.Light
{
    public interface ILightBehaviour
    {
        void TurnOn(float delay);
        void TurnOff(float delay);
        void Switch(float delay);
    }
}

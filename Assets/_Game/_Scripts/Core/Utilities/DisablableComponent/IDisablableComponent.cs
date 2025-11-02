using System;

namespace Game.Core.Utilities.DisablableComponent
{
    public interface IDisablableComponent
    {
        void Switch(bool enabled);
    }
}

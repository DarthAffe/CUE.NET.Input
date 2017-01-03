using System;
using CUE.NET.Input.EventArgs;

namespace CUE.NET.Input.Input
{
    internal  interface IDeviceInput
    {
        void RegisterEventHandler(EventHandler<OnInputEventArgs> eventHandler);
        void UnregisterEventHandler(EventHandler<OnInputEventArgs> eventHandler);
    }
}

using System;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Input.EventArgs;

namespace CUE.NET.Input.Input
{
    internal interface IDeviceInput
    {
        CorsairLedId[] ActiveInputs { get; }

        void RegisterEventHandler(EventHandler<OnInputEventArgs> eventHandler);
        void UnregisterEventHandler(EventHandler<OnInputEventArgs> eventHandler);
    }
}

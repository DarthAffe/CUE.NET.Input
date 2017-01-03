using System;
using System.Collections.Generic;
using CUE.NET.Devices;
using CUE.NET.Input.EventArgs;
using CUE.NET.Input.Input;

namespace CUE.NET.Input
{
    public static class CueDeviceExtensions
    {
        #region Properties & Fields

        private static Dictionary<ICueDevice, IDeviceInput> _deviceInputMapping = new Dictionary<ICueDevice, IDeviceInput>();

        #endregion

        #region Methods

        public static void RegisterOnInput(this ICueDevice cueDevice, EventHandler<OnInputEventArgs> eventHandler)
        {
            IDeviceInput deviceInput = GetOrCreateDeviceInput(cueDevice);
            deviceInput.RegisterEventHandler(eventHandler);
        }

        public static void UnregisterOnInput(this ICueDevice cueDevice, EventHandler<OnInputEventArgs> eventHandler)
        {
            IDeviceInput deviceInput = GetOrCreateDeviceInput(cueDevice);
            deviceInput.UnregisterEventHandler(eventHandler);
        }

        private static IDeviceInput GetOrCreateDeviceInput(ICueDevice cueDevice)
        {
            IDeviceInput deviceInput;
            if (!_deviceInputMapping.TryGetValue(cueDevice, out deviceInput))
                _deviceInputMapping.Add(cueDevice, (deviceInput = new DeviceInput(cueDevice)));
            return deviceInput;
        }

        #endregion
    }
}

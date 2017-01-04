using System;
using System.Collections.Generic;
using CUE.NET.Devices;
using CUE.NET.Input.EventArgs;
using CUE.NET.Input.Input;

namespace CUE.NET.Input
{
    /// <summary>
    /// Extends the <see cref="ICueDevice"/> to allow access to device-input.
    /// </summary>
    public static class CueDeviceExtensions
    {
        #region Properties & Fields

        private static Dictionary<ICueDevice, IDeviceInput> _deviceInputMapping = new Dictionary<ICueDevice, IDeviceInput>();

        #endregion

        #region Methods

        /// <summary>
        /// Registers an eventhandler on the device to be notified about input.
        /// </summary>
        /// <param name="cueDevice">The <see cref="ICueDevice"/> the eventhandler should be registered to.</param>
        /// <param name="eventHandler">The eventhandler to register.</param>
        public static void RegisterOnInput(this ICueDevice cueDevice, EventHandler<OnInputEventArgs> eventHandler)
        {
            IDeviceInput deviceInput = GetOrCreateDeviceInput(cueDevice);
            deviceInput.RegisterEventHandler(eventHandler);
        }

        /// <summary>
        /// Unregisters an eventhandler on the device to stop beeing notified about input.
        /// </summary>
        /// <param name="cueDevice">The <see cref="ICueDevice"/> the eventhandler should be unregistered from.</param>
        /// <param name="eventHandler">The eventhandler to unregister.</param>
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

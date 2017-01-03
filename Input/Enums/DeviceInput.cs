using System;
using System.Collections.Generic;
using System.Linq;
using CUE.NET.Devices;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Input.EventArgs;

namespace CUE.NET.Input.Input.Enums
{
    internal class DeviceInput : IDeviceInput
    {
        #region Properties & Fields

        private static InputBitMask[] _bitMasks;

        private object _inputLoopLock = new object();

        private ICueDevice _cueDevice;
        private IInputLoop _inputLoop;

        private List<EventHandler<OnInputEventArgs>> _eventHandlers = new List<EventHandler<OnInputEventArgs>>();

        private List<CorsairLedId> _lastActiveInput = new List<CorsairLedId>();

        #endregion

        #region Constructors

        internal DeviceInput(ICueDevice cueDevice)
        {
            this._cueDevice = cueDevice;

            if (_bitMasks == null)
                GenerateInputBitMasks();
        }

        #endregion

        #region Methods

        public void RegisterEventHandler(EventHandler<OnInputEventArgs> eventHandler)
        {
            if (eventHandler != null && !_eventHandlers.Contains(eventHandler))
                _eventHandlers.Add(eventHandler);

            CheckInputLoop();
        }

        public void UnregisterEventHandler(EventHandler<OnInputEventArgs> eventHandler)
        {
            if (eventHandler != null && _eventHandlers.Contains(eventHandler))
                _eventHandlers.Remove(eventHandler);

            CheckInputLoop();
        }

        private void CheckInputLoop()
        {
            lock (_inputLoopLock)
            {
                if (_eventHandlers.Any())
                {
                    if (!(_inputLoop?.IsRunning ?? false))
                    {
                        _inputLoop = new HIDInputLoop(_cueDevice);
                        _inputLoop.RawDataReceived += InputLoopOnRawDataReceived;
                        _inputLoop.Start();
                    }
                }
                else
                {
                    if (_inputLoop?.IsRunning ?? false)
                    {
                        _inputLoop.Stop();
                        _inputLoop.RawDataReceived -= InputLoopOnRawDataReceived;
                        _inputLoop = null;
                    }
                }
            }
        }

        private void InputLoopOnRawDataReceived(object sender, RawDataReceivedEventArgs rawDataReceivedEventArgs)
        {
            List<CorsairLedId> activeInput = new List<CorsairLedId>();
            foreach (InputBitMask bitMask in _bitMasks)
                if (bitMask.IsSet(rawDataReceivedEventArgs.Buffer, 1))
                    activeInput.Add(bitMask.LedId);

            foreach (CorsairLedId lastActiveLedId in _lastActiveInput)
                if (!activeInput.Contains(lastActiveLedId))
                    OnInput(new OnInputEventArgs(_cueDevice, lastActiveLedId, InputAction.Released));

            foreach (CorsairLedId activeLedId in activeInput)
                if (!_lastActiveInput.Contains(activeLedId))
                    OnInput(new OnInputEventArgs(_cueDevice, activeLedId, InputAction.Pressed));

            _lastActiveInput = activeInput;
        }

        private void OnInput(OnInputEventArgs args)
        {
            foreach (EventHandler<OnInputEventArgs> eventHandler in _eventHandlers)
                eventHandler.Invoke(this, args);
        }

        private static void GenerateInputBitMasks()
        {
            Array ledIds = Enum.GetValues(typeof(CorsairLedId));

            _bitMasks = new InputBitMask[ledIds.Length - 1];

            int i = 0;
            foreach (CorsairLedId ledId in ledIds)
                if (ledId != CorsairLedId.Invalid)
                    _bitMasks[i++] = new InputBitMask(ledId);
        }

        #endregion
    }
}

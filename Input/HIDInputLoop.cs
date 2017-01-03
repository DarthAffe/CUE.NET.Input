using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CUE.NET.Devices;
using CUE.NET.Input.EventArgs;
using HidSharp;

namespace CUE.NET.Input.Input
{
    internal class HIDInputLoop : IInputLoop
    {
        #region Properties & Fields

        private ICueDevice _cueDevice;

        private bool _isStopped = false;
        private Thread _updateLoop;

        private HidDevice _hidDevice;
        private HidStream _inputStream;
        private byte[] _buffer;

        public bool IsRunning { get; private set; }

        #endregion

        #region Constructors

        public HIDInputLoop(ICueDevice cueDevice)
        {
            this._cueDevice = cueDevice;
        }

        #endregion

        #region Events

        public event EventHandler<RawDataReceivedEventArgs> RawDataReceived;

        #endregion

        #region Methods

        public void Start()
        {
            if (IsRunning) return;
            if (_isStopped) throw new InvalidOperationException("A loop which was already running can't be restarted.");

            IsRunning = true;
            _updateLoop = new Thread(Update);
            _updateLoop.Start();
        }

        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            _isStopped = true;

            _inputStream?.Dispose();
            _inputStream = null;
            _hidDevice = null;
            _buffer = null;
        }

        private void Update()
        {
            while (IsRunning)
            {
                try
                {
                    if (_inputStream == null)
                        ConnectDevice();

                    int count = _inputStream.Read(_buffer, 0, _buffer.Length);
                    if (count > 0)
                        RawDataReceived?.Invoke(this, new RawDataReceivedEventArgs(_hidDevice, _buffer, count));
                }
                catch (TimeoutException)
                { /* gogo do something with your device :p */ }
                catch (Exception ex)
                {
                    Debug.WriteLine("HIDInputLoop-Exception: " + ex.Message);
                    _inputStream?.Dispose();
                    _inputStream = null;
                    Thread.Sleep(1000);
                }
            }
        }

        private void ConnectDevice()
        {
            HidDeviceLoader loader = new HidDeviceLoader();
            int? deviceId = CorsairUSBIds.GetIdFromDeviceInfo(_cueDevice.DeviceInfo);
            if (deviceId == null) return;

            IEnumerable<HidDevice> possibleDevices = loader.GetDevices(CorsairUSBIds.VENDOR_ID, deviceId).Where(x => x.MaxInputReportLength == 64).ToList();
            //TODO DarthAffe 03.01.2017: This needs some sort of verification ...
            _hidDevice = possibleDevices.FirstOrDefault(x => x.DevicePath.Contains("&col03"))
                      ?? possibleDevices.FirstOrDefault(x => x.DevicePath.Contains("&mi_01"))
                      ?? possibleDevices.FirstOrDefault();

            if (!(_hidDevice?.TryOpen(out _inputStream) ?? false))
                _inputStream = null;

            _buffer = new byte[_hidDevice?.MaxInputReportLength ?? 0];
        }

        #endregion
    }
}

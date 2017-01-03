using System;
using CUE.NET.Input.EventArgs;

namespace CUE.NET.Input.Input
{
    internal interface IInputLoop
    {
        bool IsRunning { get; }

        event EventHandler<RawDataReceivedEventArgs> RawDataReceived;

        void Start();
        void Stop();
    }
}

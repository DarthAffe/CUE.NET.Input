using HidSharp;

namespace CUE.NET.Input.EventArgs
{
    internal class RawDataReceivedEventArgs : System.EventArgs
    {
        #region Properties & Fields

        internal HidDevice Device { get; }
        internal byte[] Buffer { get; }
        internal int Count { get; }

        #endregion

        #region Constructors

        internal RawDataReceivedEventArgs(HidDevice device, byte[] buffer, int count)
        {
            this.Device = device;
            this.Buffer = buffer;
            this.Count = count;
        }

        #endregion
    }
}

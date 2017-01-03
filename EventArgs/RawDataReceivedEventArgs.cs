using HidSharp;

namespace CUE.NET.Input.EventArgs
{
    internal class RawDataReceivedEventArgs : System.EventArgs
    {
        #region Properties & Fields

        public HidDevice Device { get; }
        public byte[] Buffer { get; }
        public int Count { get; }

        #endregion

        #region Constructors

        public RawDataReceivedEventArgs(HidDevice device, byte[] buffer, int count)
        {
            this.Device = device;
            this.Buffer = buffer;
            this.Count = count;
        }

        #endregion
    }
}

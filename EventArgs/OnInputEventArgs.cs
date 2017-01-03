using CUE.NET.Devices;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Input.Input.Enums;

namespace CUE.NET.Input.EventArgs
{
    public class OnInputEventArgs : System.EventArgs
    {
        #region Properties & Fields

        public ICueDevice Device { get; }
        public CorsairLedId LedId { get; }
        public InputAction Action { get; }

        #endregion

        #region Constructors

        public OnInputEventArgs(ICueDevice device, CorsairLedId ledId, InputAction action)
        {
            this.Device = device;
            this.LedId = ledId;
            this.Action = action;
        }

        #endregion
    }
}

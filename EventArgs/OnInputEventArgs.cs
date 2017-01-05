// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using CUE.NET.Devices;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Input.Enums;

namespace CUE.NET.Input.EventArgs
{
    /// <summary>
    /// Represents the information supplied with an OnInput-event.
    /// </summary>
    public class OnInputEventArgs : System.EventArgs
    {
        #region Properties & Fields

        /// <summary>
        /// Gets the <see cref="ICueDevice"/> on which the event occured.
        /// </summary>
        public ICueDevice Device { get; }

        /// <summary>
        /// Gets the <see cref="CorsairLedId"/> of the key that performed the action.
        /// </summary>
        public CorsairLedId LedId { get; }

        /// <summary>
        /// Gets the <see cref="InputAction"/> performed by the key.
        /// </summary>
        public InputAction Action { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnInputEventArgs"/> class.
        /// </summary>
        /// <param name="device">The <see cref="ICueDevice"/> on which the event occured.</param>
        /// <param name="ledId">The <see cref="CorsairLedId"/> of the key that performed the action.</param>
        /// <param name="action">The <see cref="InputAction"/> performed by the key.</param>
        public OnInputEventArgs(ICueDevice device, CorsairLedId ledId, InputAction action)
        {
            this.Device = device;
            this.LedId = ledId;
            this.Action = action;
        }

        #endregion
    }
}

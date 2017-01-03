using CUE.NET.Devices.Generic.Enums;

namespace CUE.NET.Input.Input
{
    internal class InputBitMask
    {
        #region Properties & Fields

        internal int ContainingByte { get; }
        internal int Bit { get; }
        internal CorsairLedId LedId { get; }

        private byte _mask;

        #endregion

        #region Constructors

        internal InputBitMask(CorsairLedId ledId)
        {
            ContainingByte = ((int)ledId - 1) / 8;
            Bit = ((int)ledId - (ContainingByte * 8)) - 1;

            this.LedId = ledId;

            _mask = (byte)(1 << Bit);
        }

        #endregion

        #region Methods

        internal bool IsSet(byte[] data, int offset)
        {
            try
            {
                return (data[offset + ContainingByte] & _mask) == _mask;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

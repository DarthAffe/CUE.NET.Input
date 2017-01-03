using CUE.NET.Devices.Generic.Enums;

namespace CUE.NET.Input.Input
{
    public class InputBitMask
    {
        #region Properties & Fields

        public int ContainingByte { get; }
        public int Bit { get; }
        public CorsairLedId LedId { get; }

        private byte _mask;

        #endregion

        #region Constructors

        public InputBitMask(CorsairLedId ledId)
        {
            ContainingByte = ((int)ledId - 1) / 8;
            Bit = (int)ledId - (ContainingByte * 8);

            this.LedId = ledId;

            _mask = (byte)(1 << (Bit - 1));
        }

        #endregion

        #region Methods

        public bool IsSet(byte[] data, int offset)
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

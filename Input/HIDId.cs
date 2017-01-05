// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace CUE.NET.Input.Input
{
    /// <summary>
    /// Represents a set of needed parameters do identify a single HID-Device.
    /// </summary>
    public class HIDId
    {
        #region Properties & Fields

        /// <summary>
        /// Gets or sets the USB-vendor-id of the device.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the USB-product-id of the device.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the interface of the device. (Path parameter: "mi_XX")
        /// </summary>
        public int Interface { get; set; }

        /// <summary>
        /// Gets or sets the collection of the device. (Path parameter: "colXX")
        /// </summary>
        public int Collection { get; set; }

        /// <summary>
        /// Gets or sets the size of the input-buffer used to read from the device.
        /// </summary>
        public int InputBufferSize { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HIDId"/> class.
        /// </summary>
        /// <param name="vendorId">The USB-vendor-id of the device.</param>
        /// <param name="productId">The USB-product-id of the device.</param>
        /// <param name="interface">The interface of the device. (Path parameter: "mi_XX")</param>
        /// <param name="collection">The collection of the device. (Path parameter: "colXX")</param>
        /// <param name="inputBufferSize">The size of the input-buffer used to read from the device.</param>
        public HIDId(int vendorId, int productId, int @interface, int collection, int inputBufferSize)
        {
            this.VendorId = vendorId;
            this.ProductId = productId;
            this.Interface = @interface;
            this.Collection = collection;
            this.InputBufferSize = inputBufferSize;
        }

        #endregion
    }
}

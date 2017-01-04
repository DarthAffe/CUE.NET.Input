// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

using System.Collections.Generic;
using CUE.NET.Devices;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Input.Input;

namespace CUE.NET.Input
{
    public static class CorsairHIDIds
    {
        /// <summary>
        /// Gets or sets the USB-veondor-id of Corsair. (Changing this should never be needed.)
        /// </summary>
        public static int VendorId { get; set; } = 0x1B1C;

        /// <summary>
        /// Gets a dictionary containing a mapping between the <see cref="IDeviceInfo.Model" /> and the <see cref="HIDId" /> for supported keyboards.
        /// </summary>
        public static Dictionary<string, HIDId> KeyboardIds { get; } = new Dictionary<string, HIDId>
        {
            /*    Name                      VendorId  ProdId  Iface Col  Buffer */
            {"K70 LUX",           new HIDId(VendorId, 0x1b36, 0x01, 0x03, 64)},
            {"K70 RAPIDFIRE",     new HIDId(VendorId, 0x1b3a, 0x01, 0x03, 64)},
            {"CGK65 RGB",         new HIDId(VendorId, 0x1B17, 0x01, 0x03, 64)},
            {"K65 LUX RGB",       new HIDId(VendorId, 0x1B37, 0x01, 0x03, 64)},
            {"K65 RGB RAPIDFIRE", new HIDId(VendorId, 0x1B39, 0x01, 0x03, 64)},
            {"K70 RGB RAPIDFIRE", new HIDId(VendorId, 0x1b38, 0x01, 0x03, 64)},
            {"K70 LUX RGB",       new HIDId(VendorId, 0x1b33, 0x01, 0x03, 64)},
            {"K70 RGB",           new HIDId(VendorId, 0x1b13, 0x01, 0x03, 64)},
            {"K95 RGB",           new HIDId(VendorId, 0x1b11, 0x01, 0x03, 64)},
            {"STRAFE",            new HIDId(VendorId, 0x1b15, 0x01, 0x03, 64)},
            {"STRAFE RGB",        new HIDId(VendorId, 0x1b20, 0x01, 0x03, 64)},
        };

        /// <summary>
        /// Gets a dictionary containing a mapping between the <see cref="IDeviceInfo.Model" /> and the <see cref="HIDId" /> for supported mice. 
        /// </summary>
        public static Dictionary<string, HIDId> MouseIds { get; } = new Dictionary<string, HIDId>
        {
            /*    Name                       VendorId  ProdId  Iface Col  Buffer */
            { "KATAR",             new HIDId(VendorId, 0x1b22, 0x01, 0x03, 0)},
            { "M65 PRO RGB",       new HIDId(VendorId, 0x1b2e, 0x01, 0x03, 0)},
            { "M65 RGB",           new HIDId(VendorId, 0x1b12, 0x01, 0x03, 0)},
            { "SABRE",             new HIDId(VendorId, 0x1b32, 0x01, 0x03, 0)},
            { "SABRE RGB",         new HIDId(VendorId, 0x1b2f, 0x01, 0x03, 0)},
            { "SABRE RGB Optical", new HIDId(VendorId, 0x1b14, 0x01, 0x03, 0)},
            { "SABRE RGB Laser",   new HIDId(VendorId, 0x1b19, 0x01, 0x03, 0)},
            { "Scimitar",          new HIDId(VendorId, 0x1b1e, 0x01, 0x03, 0)},
        };

        /// <summary>
        /// Gets a dictionary containing a mapping between the <see cref="IDeviceInfo.Model" /> and the <see cref="HIDId" /> for supported headsets.
        /// </summary>
        public static Dictionary<string, HIDId> HeadsetIds { get; } = new Dictionary<string, HIDId>
        {
            /*    Name                       VendorId  ProdId  Iface Col  Buffer */
            { "VOID SURROUND",     new HIDId(VendorId, 0x0a30, 0x01, 0x03, 0)},
            { "VOID USB",          new HIDId(VendorId, 0x0a0f, 0x01, 0x03, 0)},
            { "VOID WIRELESS",     new HIDId(VendorId, 0x0a0c, 0x01, 0x03, 0)},
            { "Generic Headset",   new HIDId(VendorId, 0x0100, 0x01, 0x03, 0)},
        };

        /// <summary>
        /// Gets the <see cref="HIDId" /> for the device the provided <see cref="IDeviceInfo"/> is from.
        /// </summary>
        /// <param name="deviceInfo">The <see cref="IDeviceInfo"/> of the device.</param>
        /// <returns>The <see cref="HIDId"/> fitting the provided <see cref="IDeviceInfo"/> or NULL if there isn't any found.</returns>
        public static HIDId GetHidIdFromDeviceInfo(IDeviceInfo deviceInfo)
        {
            HIDId id;
            switch (deviceInfo.Type)
            {
                case CorsairDeviceType.Keyboard:
                    if (!KeyboardIds.TryGetValue(deviceInfo.Model, out id))
                        id = null;
                    break;
                case CorsairDeviceType.Mouse:
                    if (!MouseIds.TryGetValue(deviceInfo.Model, out id))
                        id = null;
                    break;
                case CorsairDeviceType.Headset:
                    if (!HeadsetIds.TryGetValue(deviceInfo.Model, out id))
                        id = null;
                    break;
                default:
                    id = null;
                    break;
            }

            return id;
        }
    }
}

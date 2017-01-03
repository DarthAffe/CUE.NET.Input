using System.Collections.Generic;
using CUE.NET.Devices;
using CUE.NET.Devices.Generic.Enums;

namespace CUE.NET.Input
{
    public static class CorsairUSBIds
    {
        public static int VENDOR_ID = 0x1B1C;

        public static readonly Dictionary<string, int> KEYBOARD_IDS = new Dictionary<string, int>
        {
            { "K70 LUX", 0x1b36},
            { "K70 RAPIDFIRE", 0x1b3a},
            { "CGK65 RGB", 0x1B17},
            { "K65 LUX RGB", 0x1B37},
            { "K65 RGB RAPIDFIRE", 0x1B39},
            { "K70 RGB RAPIDFIRE", 0x1b38},
            { "K70 LUX RGB", 0x1b33},
            { "K70 RGB", 0x1b13},
            { "K95 RGB", 0x1b11},
            { "STRAFE", 0x1b15},
            { "STRAFE RGB", 0x1b20}
        };

        public static readonly Dictionary<string, int> MOUSE_IDS = new Dictionary<string, int>
        {
            { "KATAR", 0x1b22},
            { "M65 PRO RGB", 0x1b2e},
            { "M65 RGB", 0x1b12},
            { "SABRE", 0x1b32},
            { "SABRE RGB", 0x1b2f},
            { "SABRE RGB Optical", 0x1b14},
            { "SABRE RGB Laser", 0x1b19},
            { "Scimitar", 0x1b1e}
        };

        public static readonly Dictionary<string, int> HEADSET_IDS = new Dictionary<string, int>
        {
            { "VOID SURROUND", 0x0a30},
            { "VOID USB", 0x0a0f},
            { "VOID WIRELESS", 0x0a0c},
            { "Generic Headset", 0x0100}
        };

        public static int? GetIdFromDeviceInfo(IDeviceInfo deviceInfo)
        {
            int id;
            switch (deviceInfo.Type)
            {
                case CorsairDeviceType.Keyboard:
                    if (!KEYBOARD_IDS.TryGetValue(deviceInfo.Model, out id))
                        return null;
                    break;
                case CorsairDeviceType.Mouse:
                    if (!MOUSE_IDS.TryGetValue(deviceInfo.Model, out id))
                        return null;
                    break;
                case CorsairDeviceType.Headset:
                    if (!HEADSET_IDS.TryGetValue(deviceInfo.Model, out id))
                        return null;
                    break;
                default:
                    return null;
            }

            return id;
        }
    }
}

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Exceptions;
using CUE.NET.Groups;
using CUE.NET.Input;
using InputAction = CUE.NET.Input.Input.Enums.InputAction;

namespace SimpleDevTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Press any key to exit ...");
            Console.WriteLine();
            Task.Factory.StartNew(
                () =>
                {
                    Console.ReadKey();
                    Environment.Exit(0);
                });

            try
            {
                // Initialize CUE-SDK
                CueSDK.Initialize();
                Console.WriteLine("Initialized with " + CueSDK.LoadedArchitecture + "-SDK");

                CueSDK.KeyboardSDK.Brush = (SolidColorBrush)Color.Black;
                CueSDK.UpdateMode = UpdateMode.Continuous;

                ListLedGroup highlightGroup = new ListLedGroup(CueSDK.KeyboardSDK) { Brush = (SolidColorBrush)Color.White };

                CueSDK.KeyboardSDK.RegisterOnInput((sender, eventArgs) =>
                {
                    if (eventArgs.Action == InputAction.Pressed)
                        highlightGroup.AddLed(eventArgs.LedId);
                    else
                        highlightGroup.RemoveLed(eventArgs.LedId);
                });
            }
            catch (CUEException ex)
            {
                Console.WriteLine("CUE Exception! ErrorCode: " + Enum.GetName(typeof(CorsairError), ex.Error));
            }
            catch (WrapperException ex)
            {
                Console.WriteLine("Wrapper Exception! Message:" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception! Message:" + ex.Message);
            }

            while (true)
                Thread.Sleep(1000); // Don't exit after exception
        }
    }
}

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Exceptions;
using CUE.NET.Gradients;
using CUE.NET.Groups;

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

                CueSDK.KeyboardSDK.Brush = new LinearGradientBrush(new RainbowGradient()) { Brightness = 0.125f };
                CueSDK.UpdateMode = UpdateMode.Continuous;

                IBrush highlightBrushColor = new LinearGradientBrush(new RainbowGradient());
                highlightBrushColor.AddEffect(new ReactiveTypingEffect(CueSDK.KeyboardSDK) { DecayTime = 2f });
                ListLedGroup highlightGroupColor = new ListLedGroup(CueSDK.KeyboardSDK, CueSDK.KeyboardSDK) { Brush = highlightBrushColor };

                //TODO DarthAffe 05.01.2017: Workaround since there is a bug in the solid color brush - this behaves the same but without the problems caused by this bug.
                IBrush highlightBrushFlash = new LinearGradientBrush(new LinearGradient(new GradientStop(0, Color.White), new GradientStop(1, Color.White)));
                highlightBrushFlash.AddEffect(new ReactiveTypingEffect(CueSDK.KeyboardSDK) { DecayTime = 0.33f });
                ListLedGroup highlightGroupFlash = new ListLedGroup(CueSDK.KeyboardSDK, CueSDK.KeyboardSDK) { Brush = highlightBrushFlash };
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

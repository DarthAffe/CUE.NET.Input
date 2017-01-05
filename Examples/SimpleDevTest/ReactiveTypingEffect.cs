using System;
using System.Collections.Generic;
using System.Linq;
using CUE.NET.Brushes;
using CUE.NET.Devices;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Effects;
using CUE.NET.Helper;
using CUE.NET.Input;
using CUE.NET.Input.EventArgs;

namespace SimpleDevTest
{
    public class ReactiveTypingEffect : AbstractBrushEffect
    {
        #region Properties & Fields

        private ICueDevice _observedDevice;

        private EventHandler<OnInputEventArgs> _eventHandler = (sender, args) => { };
        private Dictionary<CorsairLedId, float> _alphaValues = new Dictionary<CorsairLedId, float>();

        // ReSharper disable once MemberCanBePrivate.Global
        public float DecayTime { get; set; } = 1f;

        #endregion

        #region Constructors

        public ReactiveTypingEffect(ICueDevice observedDevice)
        {
            this._observedDevice = observedDevice;
        }

        #endregion

        #region Methods

        public override void Update(float deltaTime)
        {
            CorsairLedId[] activeLeds = _observedDevice.GetActiveInputs();

            foreach (KeyValuePair<BrushRenderTarget, CorsairColor> renderTarget in Brush.RenderedTargets)
            {
                CorsairLedId ledId = renderTarget.Key.LedId;

                if (activeLeds.Contains(ledId))
                    _alphaValues[ledId] = renderTarget.Value.GetFloatA();
                else
                {
                    float alpha;
                    if (_alphaValues.TryGetValue(ledId, out alpha))
                    {
                        if (DecayTime <= 0)
                            alpha = 0;
                        else
                            alpha -= ((1f / DecayTime) * deltaTime);

                        if (alpha <= 0)
                            _alphaValues.Remove(ledId);
                        else
                            _alphaValues[ledId] = alpha;
                    }
                    else
                        alpha = 0;

                    renderTarget.Value.A = ColorHelper.GetIntColorFromFloat(alpha);
                }
            }
        }

        // DarthAffe 05.01.2017: We just apply an empty eventhandler since we want the input to be observed even if we don't care about the event.
        public override void OnAttach(IBrush target)
        {
            base.OnAttach(target);

            _observedDevice.RegisterOnInput(_eventHandler);
        }

        public override void OnDetach(IBrush target)
        {
            base.OnDetach(target);

            _observedDevice.UnregisterOnInput(_eventHandler);
        }

        #endregion
    }
}

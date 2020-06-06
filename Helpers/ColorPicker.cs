using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7.Helpers
{
    public class ColorPicker
    {
        public static Color GenerateRandomColor()
        {
            // use HSV
            var hue = (float)(RandomNumberGenerator.Instance.GenerateInt(0, 360));
            return HSVToRGB(hue, 1.0f, 1.0f);
        }

        private static Color HSVToRGB(float h, float s, float v)
        {
            var c = s * v;
            var x = (float)(c * (1.0f - MathF.Abs(((h / 60.0f) % 2.0f) - 1.0f)));
            var m = v - c;
            float rs, gs, bs;

            if (h >= 0.0f && h < 60.0f)
            {
                rs = c;
                gs = x;
                bs = 0;
            }
            else if (h >= 60.0f && h < 120.0f)
            {
                rs = x;
                gs = c;
                bs = 0;
            }
            else if (h >= 120.0f && h < 180.0f)
            {
                rs = 0;
                gs = c;
                bs = x;
            }
            else if (h >= 180.0f && h < 240.0f)
            {
                rs = 0;
                gs = x;
                bs = c;
            }
            else if (h >= 240.0f && h < 300.0f)
            {
                rs = x;
                gs = 0;
                bs = c;
            }
            else
            {
                rs = c;
                gs = 0;
                bs = x;
            }

            var r = (byte)((rs + m) * 255);
            var g = (byte)((gs + m) * 255);
            var b = (byte)((bs + m) * 255);

            return new Color(r, g, b);
        }
    }
}

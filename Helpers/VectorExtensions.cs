using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7.Helpers
{
    public static class VectorExtensions
    {
        public static void Normalize(this Vector2f v)
        {
            var invLength = 1.0f / v.Magnitude();
            v.X *= invLength;
            v.Y *= invLength;
        }

        public static float Magnitude(this Vector2f v)
        {
            return MathF.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        public static float Dot(this Vector2f v1, ref Vector2f v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static float Dot(this Vector3f v1, ref Vector3f v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
    }
}

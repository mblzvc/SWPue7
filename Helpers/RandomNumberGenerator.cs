using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7.Helpers
{
    public class RandomNumberGenerator
    {
        private readonly Random _random = new Random();

        public static RandomNumberGenerator Instance { get; } = new RandomNumberGenerator();

        public int GenerateInt(int min, int max)
        {
            // make it inclusive
            return _random.Next(min, max + 1);
        }

        public float GenerateFloat(float min, float max)
        {
            var range = max - min;

            if (range <= 0)
            {
                return min;
            }

            var randVal = (float)_random.NextDouble();
            return min + randVal * range;
        }
    }
}

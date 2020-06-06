using SFML.Graphics;
using SFML.System;
using SWP_UE7.Components;
using SWP_UE7.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SWP_UE7
{
    class MoveSystem
    {
        private readonly (Vector2f, float)[] bounds;

        public MoveSystem(ref FloatRect worldBounds)
        {
            bounds = new[]
            {
                (new Vector2f(0.0f, 1.0f), worldBounds.Top),
                (new Vector2f(-1.0f, 0.0f), worldBounds.Left + worldBounds.Width),
                (new Vector2f(0.0f, -1.0f), worldBounds.Top + worldBounds.Height),
                (new Vector2f(1.0f, 0.0f), worldBounds.Left)
            };
        }

        public void Update(ref Time dt, ref Vector2f position, ref Vector2f velocity, ref float radius)
        {

            var seconds = dt.AsSeconds();

            // calculate the next position
            Vector2f nextPosition = position + velocity * seconds;

            var l = new Vector3f();
            var pl = new Vector3f();
            var p0 = new Vector3f();
            var v = new Vector3f();

            foreach (var bound in bounds)
            {
                l.X = bound.Item1.X;
                l.Y = bound.Item1.Y;
                l.Z = bound.Item2 - radius;

                pl.X = nextPosition.X;
                pl.Y = nextPosition.Y;
                pl.Z = 1.0f;

                var distance = pl.Dot(ref l);

                // we have an intersection between circle and boundary
                if (distance <= 0.0f)
                {
                    p0.X = position.X;
                    p0.Y = position.Y;
                    p0.Z = 1.0f;

                    v.X = velocity.X;
                    v.Y = velocity.Y;
                    v.Z = 0.0f;

                    // calculate the exact time of collision
                    var t = -l.Dot(ref p0) / l.Dot(ref v);

                    // move the circle forward until it collides
                    position += velocity * t;

                    // calculate remaining time
                    seconds -= t;

                    // invert the movement direction
                    var reverse = -velocity;

                    // calculate the reflection vector and take it as the new velocity
                    velocity = 2.0f * bound.Item1.Dot(ref reverse) * bound.Item1 - reverse;

                    // for the remaining time, move into the new direction
                    nextPosition = position + velocity * MathF.Max(seconds, 0.0f);

                    break;

                }
            }
            position = nextPosition;
        }
    }
}

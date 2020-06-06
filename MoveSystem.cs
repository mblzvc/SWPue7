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
        private readonly ColorComponent colorComponent;
        private readonly PositionComponent positionComponent;
        private readonly RadiusComponent radiusComponent;
        private readonly VelocityComponent velocityComponent;
        private readonly RenderStates _renderStates;

        private readonly (Vector2f, float)[] bounds;

        public MoveSystem(ColorComponent cc, PositionComponent pc, RadiusComponent rc, VelocityComponent vc, ref FloatRect worldBounds)
        {
            colorComponent = cc;
            positionComponent = pc;
            radiusComponent = rc;
            velocityComponent = vc;

            bounds = new[]
            {
                (new Vector2f(0.0f, 1.0f), worldBounds.Top),
                (new Vector2f(-1.0f, 0.0f), worldBounds.Left + worldBounds.Width),
                (new Vector2f(0.0f, -1.0f), worldBounds.Top + worldBounds.Height),
                (new Vector2f(1.0f, 0.0f), worldBounds.Left)
            };

            _renderStates = new RenderStates(TextureManager.Instance.CircleTexture);
        }

        public void Update(ref Time dt, Entity entity)
        {
            var seconds = dt.AsSeconds();

            Vector2f position = positionComponent.GetComponent(entity);
            Vector2f velocity = velocityComponent.GetComponent(entity);
            float radius = radiusComponent.GetComponent(entity);

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
            velocityComponent.SetComponent(velocity, entity);
            positionComponent.SetComponent(nextPosition,entity);
        }

        public void Draw(RenderWindow window,Entity entity)
        {
            var textureSize = (float)_renderStates.Texture.Size.X;
            var color = colorComponent.GetComponent(entity);
            var position = positionComponent.GetComponent(entity);
            var radius = radiusComponent.GetComponent(entity);

            // the texture coordinates
            var uvTl = new Vector2f(0.0f, 0.0f);
            var uvTr = new Vector2f(textureSize, 0.0f);
            var uvBr = new Vector2f(textureSize, textureSize);
            var uvBl = new Vector2f(0.0f, textureSize);

            // generate the corner coordinates
            var tl = new Vector2f(position.X - radius, position.Y - radius);
            var tr = new Vector2f(position.X + radius, position.Y - radius);
            var bl = new Vector2f(position.X - radius, position.Y + radius);
            var br = new Vector2f(position.X + radius, position.Y + radius);

            // write the vertices (position, color, texture coordinates)
            var vertices = new Vertex[]
            {
                new Vertex(tl, color, uvTl),
                new Vertex(tr, color, uvTr),
                new Vertex(br, color, uvBr),
                new Vertex(bl, color, uvBl)
            };

            // draw using the circle texture
            window.Draw(vertices, PrimitiveType.Quads, _renderStates);
        }
    }
}

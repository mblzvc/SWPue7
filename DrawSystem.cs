using SFML.Graphics;
using SFML.System;
using SWP_UE7.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7
{
    public class DrawSystem
    {
        private readonly RenderStates _renderStates;

        public DrawSystem()
        {
            _renderStates = new RenderStates(TextureManager.Instance.CircleTexture);
        }

        public void Update(RenderWindow window, ref Vector2f position, ref Color color, ref float radius)
        {
            var textureSize = (float)_renderStates.Texture.Size.X;

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

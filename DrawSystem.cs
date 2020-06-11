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
        private Vector2f uvTl;
        private Vector2f uvTr;
        private Vector2f uvBr;
        private Vector2f uvBl;
        public DrawSystem()
        {
            _renderStates = new RenderStates(TextureManager.Instance.CircleTexture);
            var textureSize = (float)_renderStates.Texture.Size.X;

            // the texture coordinates
            uvTl = new Vector2f(0.0f, 0.0f);
            uvTr = new Vector2f(textureSize, 0.0f);
            uvBr = new Vector2f(textureSize, textureSize);
            uvBl = new Vector2f(0.0f, textureSize);
        }

        public Vertex[] Update(RenderWindow window, ref Vector2f position, ref Color color, ref float radius)
        {
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
            return vertices;
            // draw using the circle texture
            //window.Draw(vertices, PrimitiveType.Quads, _renderStates);
        }
    }
}

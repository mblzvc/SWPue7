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

        private Vertex[] vertices;

        public DrawSystem(ref Vertex[] vertices)
        {
            _renderStates = new RenderStates(TextureManager.Instance.CircleTexture);
            var textureSize = (float)_renderStates.Texture.Size.X;

            this.vertices = vertices;
            

            // the texture coordinates
            uvTl = new Vector2f(0.0f, 0.0f);
            uvTr = new Vector2f(textureSize, 0.0f);
            uvBr = new Vector2f(textureSize, textureSize);
            uvBl = new Vector2f(0.0f, textureSize);
        }

        public void Update(RenderWindow window, ref Vector2f position, ref Color color, ref float radius, int id)
        {
            // generate the corner coordinates
            var tl = new Vector2f(position.X - radius, position.Y - radius);
            var tr = new Vector2f(position.X + radius, position.Y - radius);
            var bl = new Vector2f(position.X - radius, position.Y + radius);
            var br = new Vector2f(position.X + radius, position.Y + radius);

            // write the vertices (position, color, texture coordinates)

            vertices[id*4].Position=tl;
            vertices[id*4].Color=color;
            vertices[id*4].TexCoords=uvTl;

            vertices[id * 4+1].Position = tr;
            vertices[id * 4 + 1].Color = color;
            vertices[id * 4 + 1].TexCoords = uvTr;

            vertices[id * 4 + 2].Position = br;
            vertices[id * 4 + 2].Color = color;
            vertices[id * 4 + 2].TexCoords = uvBr;

            vertices[id * 4 + 3].Position = bl;
            vertices[id * 4 + 3].Color = color;
            vertices[id * 4 + 3].TexCoords = uvBl;

            //vertices[id*4+1] = new Vertex(tr, color, uvTr);
            //vertices[id*4+2] = new Vertex(br, color, uvBr);
            //vertices[id*4+3] = new Vertex(bl, color, uvBl);
            
            // draw using the circle texture
            //window.Draw(vertices, PrimitiveType.Quads, _renderStates);
        }
    }
}

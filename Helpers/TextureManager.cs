using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SWP_UE7.Helpers
{
    public class TextureManager
    {
        public static TextureManager Instance { get; } = new TextureManager();
        public Texture CircleTexture { get; }

        private TextureManager()
        {
            CircleTexture = new Texture("textures/circle.png");
        }
    }
}

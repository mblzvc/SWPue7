using SFML.Graphics;
using SWP_UE7.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7.Components
{
    public class ColorComponent : IComponent<Color>
    {
        private Color[] Color { get; set; }

        public ColorComponent(int entities)
        {
            Color = new Color[entities];
        }
        public void AddComponent(Entity entity)
        {
            Color[entity.ID] = ColorPicker.GenerateRandomColor();
        }

        public ref Color GetComponent(Entity entity)
        {
            return ref Color[entity.ID];
        }
    }
}

using SFML.Graphics;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7
{
    class PositionComponent :IComponent<Vector2f>
    {
        public Vector2f[] Position { get; set; }

        public PositionComponent(int entities)
        {
            Position = new Vector2f[entities];
        }

        public Vector2f GetComponent(Entity entity)
        {
            return Position[entity.ID];
        }

        public void SetComponent(Vector2f position,Entity entity)
        {
            Position[entity.ID] = position;
        }

        public void AddComponent(Entity entity)
        {
            var bounds = new FloatRect();
            Position[entity.ID] = new Vector2f((bounds.Left + bounds.Width) / 2.0f, (bounds.Top + bounds.Height) / 2.0f);
        }

        public void AddComponent(Entity entity, FloatRect bounds)
        {
            Position[entity.ID] = new Vector2f((bounds.Left + bounds.Width) / 2.0f, (bounds.Top + bounds.Height) / 2.0f);
        }
    }
}

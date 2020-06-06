using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using System.Text;

namespace SWP_UE7
{
    interface IComponent<T>
    {
        public T GetComponent(Entity entity);
        public void AddComponent(Entity entity);
    }
}

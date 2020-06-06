using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7
{
    public class RadiusComponent : IComponent<float>
    {
        private float[] Radius { get; set; }
        private readonly Helpers.RandomNumberGenerator rng = Helpers.RandomNumberGenerator.Instance;

        public RadiusComponent(int entities)
        {
            Radius = new float[entities];
        }
        public void AddComponent(Entity entity)
        {
            Radius[entity.ID] = rng.GenerateFloat(8.0f, 32.0f);
        }

        public ref float GetComponent(Entity entity)
        {
            return ref Radius[entity.ID];
        }
    }
}

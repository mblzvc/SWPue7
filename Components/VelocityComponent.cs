using SFML.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using SWP_UE7.Helpers;

namespace SWP_UE7
{
    public class VelocityComponent : IComponent<Vector2f>
    {
        public Vector2f[] Velocity { get; set; }
        private readonly Helpers.RandomNumberGenerator rng = Helpers.RandomNumberGenerator.Instance;

        public VelocityComponent(int entities)
        {
            Velocity = new Vector2f[entities];
        }

        public ref Vector2f GetComponent(Entity entity)
        {
            return ref Velocity[entity.ID];
        }

        public void SetComponent(Vector2f velocity,Entity entity)
        {
            Velocity[entity.ID] = velocity;
        }

        public void AddComponent(Entity entity)
        {
            var angle = rng.GenerateFloat(0.0f, MathF.PI * 2.0f);
            var v = rng.GenerateFloat(5.0f, 100.0f);
            Velocity[entity.ID] = new Vector2f(MathF.Cos(angle) * v, MathF.Sin(angle) * v);
        }
    }
}

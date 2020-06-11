using System;
using System.Collections.Generic;
using System.Text;

namespace SWP_UE7
{
    public class EntityManager
    {
        int next;
        private List<Entity> Entities { get; set; }

        public EntityManager()
        {
            Entities = new List<Entity>();
        }

        public void CreateEntities(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateEntity();
            }
        }

        private Entity CreateEntity()
        {
            var entity = new Entity
            {
                ID = next++
            };
            Entities.Add(entity);
            return entity;
        }

        public List<Entity> GetEntities()
        {
            return Entities;
        }

    }
}

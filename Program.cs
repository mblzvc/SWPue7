using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SWP_UE7.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWP_UE7
{
    class Program
    {
        static void Main()
        {

            Font _font = new Font("fonts/JetBrainsMono-Regular.ttf");
            var es = new EntityManager();

            const uint windowWidth = 1024;
            const uint windowHeight = 768;
            const int numEntities = 85000;
            es.CreateEntities(numEntities);
            var bounds = new FloatRect(.0f, .0f, windowWidth, windowHeight);

            var window = new RenderWindow(new VideoMode(windowWidth, windowHeight), $"ECS Moving {numEntities} Circles");
            window.Closed += (sender, e) => { ((Window)sender).Close(); };

            var entities = new List<Entity>(es.GetEntities());
            var cc = new ColorComponent(entities.Count);
            var pc = new PositionComponent(entities.Count);
            var rc = new RadiusComponent(entities.Count);
            var vc = new VelocityComponent(entities.Count);

            Text text = new Text("", _font, 24);
            text.FillColor = Color.Yellow;

            foreach (Entity entity in entities)
            {
                cc.AddComponent(entity);
                pc.AddComponent(entity, bounds);
                rc.AddComponent(entity);
                vc.AddComponent(entity);
            }

            var ms = new MoveSystem(cc, pc, rc, vc, ref bounds);

            var clock = new Clock();

            while (window.IsOpen)
            {
                var dt = clock.Restart();
                foreach (Entity entity in entities)
                {
                    ms.Update(ref dt, entity);
                }
                text.DisplayedString = "frametime: " + dt.AsMilliseconds() + " ms";
                window.Clear();

                foreach (Entity entity in entities)
                {
                    ms.Draw(window, entity);

                }
                window.Draw(text);
                window.Display();
            }
        }
    }
}

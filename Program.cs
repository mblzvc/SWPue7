using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SWP_UE7.Components;
using SWP_UE7.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var entities = es.GetEntities();
            var cc = new ColorComponent(entities.Count);
            var pc = new PositionComponent(entities.Count, ref bounds);
            var rc = new RadiusComponent(entities.Count);
            var vc = new VelocityComponent(entities.Count);

            Text text = new Text("", _font, 24);
            text.FillColor = Color.Yellow;

            foreach (Entity entity in entities)
            {
                cc.AddComponent(entity);
                pc.AddComponent(entity);
                rc.AddComponent(entity);
                vc.AddComponent(entity);
            }

            var ms = new MoveSystem(ref bounds);
            Vertex[] vertexes = new Vertex[numEntities * 4];
            for(int i = 0; i< numEntities * 4; i++)
            {
                vertexes[i] = new Vertex();
            }
            var ds = new DrawSystem(ref vertexes);

            var clock = new Clock();
            
            RenderStates _renderStates = new RenderStates(TextureManager.Instance.CircleTexture);
            var cycle = 0;
            int frames = 0;
            while (window.IsOpen)
            {
                var dt = clock.Restart();
                window.Clear();
                foreach (Entity entity in entities)
                {
                    ref Vector2f position = ref pc.GetComponent(entity);
                    ref var velocity = ref vc.GetComponent(entity);
                    ref var color = ref cc.GetComponent(entity);
                    ref var radius = ref rc.GetComponent(entity);
                    ms.Update(ref dt, ref position, ref velocity, ref radius);
                    //vertexes.AddRange(ds.Update(window, ref position, ref color, ref radius));
                    ds.Update(window, ref position, ref color, ref radius, entity.ID);
                }
                window.Draw(vertexes, PrimitiveType.Quads, _renderStates);
                frames += dt.AsMilliseconds();
                if (cycle == 10)
                {
                    cycle = 0;
                    text.DisplayedString = "frametime: " + frames/10 + " ms";
                    Console.WriteLine(text.DisplayedString);
                    frames = 0;
                }
                window.Draw(text);
                window.Display();
                Console.WriteLine(text.DisplayedString);
                cycle++;
            }
        }
    }
}

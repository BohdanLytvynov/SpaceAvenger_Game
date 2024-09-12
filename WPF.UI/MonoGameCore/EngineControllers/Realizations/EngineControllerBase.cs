using Microsoft.Xna.Framework;
using MonoGame.Extensions.Behaviors;
using MonoGame.Extensions.Physics.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.UI.MonoGameCore.EngineControllers.Interfaces;
using WPF.UI.MonoGameCore.Engines.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WPF.UI.MonoGameCore.EngineControllers.Realizations
{
    internal class EngineControllerBase : IEngineControllStrategy
    {
        public void GetDisplacement(Dictionary<string, IEngine> engines,
            Vector2 destination,
            Transform transform)
        {
            if (transform.Position != destination && destination != Vector2.Zero)
            {
                //Move to the destination

            }

            if (transform.Position == destination)
            {
                //Stop all engines when we reached the destination
                foreach (var k in engines.Values)
                {
                    k.Stop();
                }            
            }
        }
    }
}

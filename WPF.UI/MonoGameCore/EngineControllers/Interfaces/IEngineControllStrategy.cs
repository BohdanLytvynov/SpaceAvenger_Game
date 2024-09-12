using Microsoft.Xna.Framework;
using MonoGame.Extensions.Behaviors;
using System.Collections.Generic;
using WPF.UI.MonoGameCore.Engines.Interfaces;

namespace WPF.UI.MonoGameCore.EngineControllers.Interfaces
{
    internal interface IEngineControllStrategy
    {
        void GetDisplacement(Dictionary<string, IEngine> engines, 
            Vector2 destination,
            Transform transform);
    }
}

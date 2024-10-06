using Microsoft.Xna.Framework;
using WPF.UI.MonoGameCore.Engines.Realizations;

namespace WPF.UI.MonoGameCore.Engines.Interfaces
{
    internal interface IEngine
    {
        public float CurrentThrust { get; }

        public float MaxThrust { get; }
                       
        public EngineState Mode { get; }
        
        void Start(GameTime time);

        void Stop();        
    }
}

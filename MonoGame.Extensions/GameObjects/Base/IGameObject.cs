using Microsoft.Xna.Framework;
using System;

namespace MonoGame.Extensions.GameObjects.Base
{
    public unsafe interface IGameObject : IDisposable
    {
        public string Name { get;}

        public bool Loaded { get; }

        public bool Disposed { get; }
        
        void Load();

        void UnLoad();

        void Update(IUpdateArgs args, GameTime time, ref bool play);

        void Draw(GameTime time, ref bool play);        
    }
}

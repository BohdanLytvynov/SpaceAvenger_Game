using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObject.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.GameComponents.Base
{
    public unsafe interface IGameObject : IDisposable
    {
        public string Name { get;}

        public bool Loaded { get; }

        public bool Disposed { get; }
        
        void Load();

        void UnLoad();

        void Update(IUpdateArgs args,ref bool play);

        void Draw(ref bool play);        
    }
}

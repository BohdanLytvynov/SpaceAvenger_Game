using Microsoft.Xna.Framework;
using MonoGame.Extensions.Physics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.UI.MonoGameCore.Modules;

namespace WPF.UI.MonoGameCore.Engines.Interfaces
{
    internal interface IEngine
    {
        public float CurrentThrust { get; }

        public float MaxThrust { get; }
                       
        public bool Working { get; }

        public float Increment { get; }

        void Start();

        void Stop();

        void Increase(GameTime gameTime);
    }
}

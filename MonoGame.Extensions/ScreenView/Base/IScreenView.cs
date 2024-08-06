using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.ScreenView.Base
{
    public unsafe interface IScreenView : IDisposable
    {
        public bool Loaded { get; }

        public bool Disposed { get; }

        void Load();

        void UnLoad();

        void Update(void* ptr);

        void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);
    }
}

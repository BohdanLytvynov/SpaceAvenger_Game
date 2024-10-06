using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Behaviors.Transformables;

namespace MonoGame.Extensions.Animations.Interfaces.Animations
{
    public interface IAnimation
    {
        #region Properties

        public bool Reverse { get; }

        public Color BlendColor { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int FrameCount { get; }

        public int FrameHeight { get; }

        public int FrameWidth { get; }

        public double AnimationSpeed { get; set; }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; }

        #endregion

        #region Functions

        void Start();

        void Stop();

        void Reset(bool reverse);

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransformable transform, 
            float layerDepth = 0f);

        #endregion        
    }
}

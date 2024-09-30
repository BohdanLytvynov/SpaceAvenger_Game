using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.Debugging.Interfaces;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Sprites.Interfaces;

namespace MonoGame.Extensions.Sprites.Realization
{
    public abstract class Sprite : GameObject, ISprite, IDebug
    {
        #region Fields

        private ITransformable m_transform;
       
        #endregion

        #region Properties

        public ITransformable Transform
        {
            get => m_transform;
            set => m_transform = value;
        }

        public bool Debugging { get; }

        #endregion

        #region Ctor

        protected Sprite(            
            string name, 
            ContentManager contentManager,
            SpriteBatch spriteBatch,
            ITransformable? transform,
            IAssetStorage? assetStorage,
            ILoadAssetStrategy? loadAssetStrategy,
            bool debug) 
            : base(name, 
                  contentManager, 
                  spriteBatch, 
                  assetStorage,
                  loadAssetStrategy)
        {            
            Debugging = debug;

            if(transform is null)
                m_transform = new Transform();
            else
                m_transform = transform;            
        }

        #endregion

        #region Functions
       
        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);

            if (Debugging)
            {
                //Draw Central origin Point of the Sprite
                SpriteBatch.DrawPoint(Transform.Position, Color.Blue, 5, 1f);

                //Draw Central origin Point of the Sprite
                SpriteBatch.DrawPoint(Transform.UpperLeftCorner, Color.Orange, 5, 1f);

                //Draw Local X
                SpriteBatch.DrawLine(Transform.Position, Transform.Position +
                    Transform.LocalBasis[0] * Transform.ActualSize/2, Color.Red, 2, 1f);

                //Draw Local Y
                SpriteBatch.DrawLine(Transform.Position, Transform.Position +
                    Transform.LocalBasis[1] * Transform.ActualSize/2, Color.Green, 2, 1f);
            }            
        }

        #endregion

    }
}

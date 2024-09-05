using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Animations.Interfaces.AnimationManagers;
using MonoGame.Extensions.Animations.Realizations.AnimationManagers;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Sprites.Interfaces;

namespace MonoGame.Extensions.Sprites.Realization
{
    public abstract class Sprite : GameObject, ISprite
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
       
        #endregion

        #region Ctor

        protected Sprite(
            string name, 
            ContentManager contentManager,
            SpriteBatch spriteBatch,
            ITransformable? transform,
            IAssetStorage? assetStorage,
            ILoadAssetStrategy loadAssetStrategy) 
            : base(name, 
                  contentManager, 
                  spriteBatch, 
                  assetStorage,
                  loadAssetStrategy)
        {
            if(transform is null)
                m_transform = new Transform();
            else
                m_transform = transform;            
        }        

        #endregion

    }
}

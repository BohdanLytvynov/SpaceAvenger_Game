using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;

namespace MonoGame.Extensions.Screens.Base
{
    public abstract class ScreenBase : GameObject
    {
        #region Fields
        private Rectangle m_ScreenDimensions;        
        #endregion

        #region Properties
        public Rectangle ScreenDimensions { get => m_ScreenDimensions; }
        #endregion

        #region Ctor
        protected ScreenBase(string name,
            ContentManager contentmanager,
            SpriteBatch spriteBatch,
            Rectangle screenDimensions,
            ILoadAssetStrategy loadAssetStrategy,
            IAssetStorage? assetStorage = default) :
            base(name, contentmanager, spriteBatch,  assetStorage, loadAssetStrategy)
        {
            m_ScreenDimensions = screenDimensions;
        }
        #endregion
    }
}

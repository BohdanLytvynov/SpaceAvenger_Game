using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;

namespace MonoGame.Extensions.Screens.Base
{
    public abstract class ScreenBase : GameObject.Base.GameObject
    {
        #region Fields
        private Rectangle m_ScreenDimensions;        
        #endregion

        #region Properties
        public Rectangle ScreenResolution { get => m_ScreenDimensions; }
        #endregion

        #region Ctor
        protected ScreenBase(string name,
            ContentManager contentmanager,
            SpriteBatch spriteBatch,
            Rectangle screenDimensions,
            IAssetStorage? assetStorage) :
            base(name, contentmanager, spriteBatch,  assetStorage)
        {
            m_ScreenDimensions = screenDimensions;
        }
        #endregion
    }
}

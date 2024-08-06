using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.AssetStorages.Realization;
using System;

namespace MonoGame.Extensions.ScreenView.Base
{
    public abstract class ScreenViewBase : IScreenView
    {
        #region Fields
        private bool disposedValue;

        private bool m_loaded;

        private IAssetStorage m_storage;

        #endregion

        #region Properties

        public bool Loaded => m_loaded;

        public bool Disposed => disposedValue;

        #endregion

        #region Ctor

        protected ScreenViewBase(IAssetStorage assetStorage = null)
        {
            disposedValue = false;

            m_loaded = false;

            if (assetStorage is null)
                m_storage = new AssetStorage();
        }

        #endregion

        public abstract void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);

        public virtual void Load()
        { 
            m_loaded = true;
        }

        public virtual void UnLoad()
        {
            m_loaded = false;
        }

        public abstract unsafe void Update(void* ptr);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    UnLoad();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ScreenViewBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }        
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.AssetStorages.Realization;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;
using System.Linq;

namespace MonoGame.Extensions.GameObjects.Base
{
    public abstract class GameObject : IGameObject
    {
        #region Fields
        private bool disposedValue;

        private bool m_loaded;

        private IAssetStorage m_storage;

        private ILoadAssetStrategy m_loadStrategy;

        private readonly string m_name;

        private readonly ContentManager m_contentManager;       

        private readonly SpriteBatch m_spriteBatch;        
                
        #endregion

        #region Properties

        public bool Loaded => m_loaded;

        public bool Disposed => disposedValue;

        public string Name => m_name;

        protected ContentManager ContentManager => m_contentManager;

        protected IAssetStorage Storage => m_storage;
        
        protected SpriteBatch SpriteBatch => m_spriteBatch;
       
        #endregion

        #region Ctor

        public GameObject(            
            string name, 
            ContentManager contentmanager,
            SpriteBatch spriteBatch,             
            IAssetStorage? assetStorage,
            ILoadAssetStrategy? loadAssetStrategy)
        {            
            m_name = name;

            disposedValue = false;

            m_loaded = false;

            m_contentManager = contentmanager;

            if (assetStorage is null)
                m_storage = new AssetStorage(m_contentManager);

            m_loadStrategy = loadAssetStrategy;
                       
            m_spriteBatch = spriteBatch;                          
        }

        
        #endregion

        public virtual void Draw(GameTime time, ref bool play)
        {
            if (!play) return;
        }

        public virtual void Load()
        {
            if (Loaded)
                return;

            m_loadStrategy?.LoadAsset(m_storage, m_contentManager, this);

            m_loaded = true;
        }

        public virtual void UnLoad()
        {
            if (m_loaded)
            {                                
                m_storage.Clear();

                m_loaded = false;
            }            
        }

        public virtual void Update(IUpdateArgs args, GameTime time, ref bool play)
        { 
            if(!play) return;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)                                                           
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

using Microsoft.Xna.Framework.Content;
using MonoGame.Extensions.AssetStorages.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoGame.Extensions.AssetStorages.Realization
{
    public class AssetStorage : IAssetStorage
    {
        private readonly ContentManager m_contentManager;

        private readonly SortedDictionary<string, string> m_pathStorage;

        private readonly SortedDictionary<string, IDisposable> m_storage;

        #region Ctor

        public AssetStorage(ContentManager contentManager)
        {
            m_contentManager = contentManager;

            m_storage = new SortedDictionary<string, IDisposable>();  
            
            m_pathStorage = new SortedDictionary<string, string>();
        }

        #endregion

        #region Methods

        public IDisposable this[string key] => m_storage[key];

        public void LoadAsset<T>(string key, string path)
            where T : IDisposable
        {
            if (!m_storage.ContainsKey(key))
            {
                var asset = m_contentManager.Load<T>(path);

                m_storage.Add(key, asset);

                m_pathStorage.Add(key, path);
            }
        }

        public void LoadAssets<T>(params (string key, string path)[] assets)
            where T : IDisposable
        {
            foreach (var item in assets)
            {
                LoadAsset<T>(item.key, item.path);
            }
        }

        public void Clear()
        {
            //Unload all Assets
            foreach (var v in m_pathStorage.Values)
            {
                m_contentManager.UnloadAsset(v);
            }

            m_storage.Clear();

            m_pathStorage.Clear();
        }

        public object? GetAsset(Func<KeyValuePair<string, IDisposable>, bool> predicate)
        {
            return m_storage.FirstOrDefault(predicate);
        }

        public IEnumerable<IDisposable> GetAssets(Func<KeyValuePair<string, IDisposable>, bool> predicate)
        {
            return m_storage.Where(predicate).Select(s => s.Value);
        }

        public string GetPath(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return m_pathStorage.FirstOrDefault(predicate).Value;
        }

        public IEnumerable<string> GetPaths(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return m_pathStorage.Where(predicate).Select(s => s.Value);
        }

        public void UnloadAsset(string key)
        {
            m_contentManager.UnloadAsset(m_pathStorage[key]);
            m_storage.Remove(key);
            m_pathStorage.Remove(key);
        }

        public void UnloadAssets(params string[] keys)
        {
            foreach (var item in keys)
            {
                UnloadAsset(item);
            }
        }
                
        #endregion


    }
}

using MonoGame.Extensions.AssetStorages.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame.Extensions.AssetStorages.Realization
{
    public class AssetStorage : IAssetStorage
    {
        private readonly SortedDictionary<string, IDisposable> m_storage;

        #region Ctor

        public AssetStorage()
        {
            m_storage = new SortedDictionary<string, IDisposable>();
        }

        public void AddAsset(string key, IDisposable asset)
        {
            if(!m_storage.ContainsKey(key))
                m_storage.Add(key, asset);
        }

        public void AddAssets(params KeyValuePair<string, IDisposable>[] assets)
        {
            foreach (var item in assets)
            {
                AddAsset(item.Key, item.Value);
            }
        }

        public void Clear()
        {            
            m_storage.Clear();
        }

        public object? GetAsset(Func<KeyValuePair<string, IDisposable>, bool> predicate)
        {
            return m_storage.FirstOrDefault(predicate);
        }

        public IEnumerable<IDisposable> GetAssets(Func<KeyValuePair<string, IDisposable>, bool> predicate)
        {
            return m_storage.Where(predicate).Select(s => s.Value);
        }

        public void RemoveAsset(string key)
        {
            m_storage.Remove(key);
        }

        public void RemoveAssets(params string[] keys)
        {
            foreach (var item in keys)
            {
                RemoveAsset(item);
            }
        }

        #endregion


    }
}

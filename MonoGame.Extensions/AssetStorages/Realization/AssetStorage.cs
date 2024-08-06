using MonoGame.Extensions.AssetStorages.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoGame.Extensions.AssetStorages.Realization
{
    public class AssetStorage : IAssetStorage
    {
        private readonly SortedDictionary<string, string> m_pathStorage;

        private readonly SortedDictionary<string, IDisposable> m_storage;

        #region Ctor

        public AssetStorage()
        {
            m_storage = new SortedDictionary<string, IDisposable>();  
            
            m_pathStorage = new SortedDictionary<string, string>();
        }

        public IDisposable this[string key] => m_storage[key];

        public void AddAsset(string key, string path, IDisposable asset)
        {
            if (!m_storage.ContainsKey(key))
            {
                m_storage.Add(key, asset);

                m_pathStorage.Add(key, path);
            }                                
        }

        public void AddAssets(params (string key, string path, IDisposable obj)[] assets)
        {
            foreach (var item in assets)
            {
                AddAsset(item.key, item.path, item.obj);
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

        public string GetPath(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return m_pathStorage.FirstOrDefault(predicate).Value;
        }

        public IEnumerable<string> GetPaths(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return m_pathStorage.Where(predicate).Select(s => s.Value);
        }

        public void RemoveAsset(string key)
        {
            m_storage.Remove(key);
            m_pathStorage.Remove(key);
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

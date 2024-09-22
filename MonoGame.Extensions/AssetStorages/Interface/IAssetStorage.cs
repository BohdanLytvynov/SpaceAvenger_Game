using System;
using System.Collections.Generic;

namespace MonoGame.Extensions.AssetStorages.Interface
{
    public interface IAssetStorage
    {
        public IDisposable this [string key] { get; }

        void LoadAsset<T>(string key, string path)
        where T : IDisposable;

        void LoadAssets<T>(params (string key, string path) [] assets)
            where T : IDisposable;

        void UnloadAsset(string key);

        object? GetAsset(Func<KeyValuePair<string, IDisposable>, bool> predicate);
        
        void UnloadAssets(params string[] keys);
        
        IEnumerable<IDisposable> GetAssets(Func<KeyValuePair<string, IDisposable>, bool> predicate);

        IEnumerable<string> GetPaths(Func<KeyValuePair<string, string>, bool> predicate);

        string GetPath(Func<KeyValuePair<string, string>, bool> predicate);

        void Clear();
    }
}

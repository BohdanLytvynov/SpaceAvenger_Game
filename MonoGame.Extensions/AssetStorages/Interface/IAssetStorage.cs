using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.AssetStorages.Interface
{
    public interface IAssetStorage
    {
        public IDisposable this [string key] { get; }

        void AddAsset(string key, string path, IDisposable asset);

        void AddAssets(params (string key, string path, IDisposable obj) [] assets);

        void RemoveAsset(string key);

        object? GetAsset(Func<KeyValuePair<string, IDisposable>, bool> predicate);
        
        void RemoveAssets(params string[] keys);

        IEnumerable<IDisposable> GetAssets(Func<KeyValuePair<string, IDisposable>, bool> predicate);

        IEnumerable<string> GetPaths(Func<KeyValuePair<string, string>, bool> predicate);

        string GetPath(Func<KeyValuePair<string, string>, bool> predicate);

        void Clear();
    }
}

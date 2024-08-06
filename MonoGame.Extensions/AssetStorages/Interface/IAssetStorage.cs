using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.AssetStorages.Interface
{
    public interface IAssetStorage
    {
        void AddAsset(string key, IDisposable asset);

        void AddAssets(params KeyValuePair<string, IDisposable> [] assets);

        void RemoveAsset(string key);

        object? GetAsset(Func<KeyValuePair<string, IDisposable>, bool> predicate);
        
        void RemoveAssets(params string[] keys);

        IEnumerable<IDisposable> GetAssets(Func<KeyValuePair<string, IDisposable>, bool> predicate);

        void Clear();
    }
}

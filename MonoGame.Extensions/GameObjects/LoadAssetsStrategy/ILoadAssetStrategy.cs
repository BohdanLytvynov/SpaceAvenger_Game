using Microsoft.Xna.Framework.Content;
using MonoGame.Extensions.AssetStorages.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.GameObjects.LoadAssetsStrategy
{
    public interface ILoadAssetStrategy
    {
        void LoadAsset(IAssetStorage assetStorage, ContentManager contentManager);
    }
}

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.MonoGameCore.LoadAssetsStrategies.Faction10.Engines.PlasmaEngines
{
    internal class IntegratedPlasmaEngineLoadStrategy : ILoadAssetStrategy
    {
        public IntegratedPlasmaEngineLoadStrategy()
        {
            
        }

        public void LoadAsset(IAssetStorage assetStorage, 
            ContentManager contentManager, GameObject current)
        {            
            assetStorage.LoadAsset<Texture2D>("jet1","Animations/Jets/Jet-1/Jet-1_128");
        }
    }
}

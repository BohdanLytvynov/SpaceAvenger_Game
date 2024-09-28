using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;

namespace WPF.UI.MonoGameCore.LoadAssetsStrategies.Faction10.Engines.PlasmaEngines
{
    internal class Acceler_PlasmaEngineLoadStrategy : ILoadAssetStrategy
    {
        public void LoadAsset(IAssetStorage assetStorage, ContentManager contentManager, 
            GameObject current)
        {
            assetStorage.LoadAsset<Texture2D>("Faction10-acceler", "Assets/Factions/Faction10/Engines/ManuverEngine_F10");
        }
    }
}

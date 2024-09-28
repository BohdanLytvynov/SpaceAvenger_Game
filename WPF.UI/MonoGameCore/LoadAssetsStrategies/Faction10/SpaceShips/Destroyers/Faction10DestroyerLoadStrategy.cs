using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Sprites.Realization;

namespace WPF.UI.MonoGameCore.LoadAssetsStrategies.Faction10.SpaceShips.Destroyers
{
    internal class Faction10DestroyerLoadStrategy : ILoadAssetStrategy
    {        
        public void LoadAsset(IAssetStorage assetStorage, 
            ContentManager contentManager, 
            GameObject current)
        {                        
            assetStorage.LoadAsset<Texture2D>("destroyer", "Assets/Factions/Faction10/SpaceShips/destroyer256");

            var t = (Texture2D)assetStorage["destroyer"];

            (current as Sprite)!.Transform.TextureSize = new SizeF() { Width = t.Width, Height = t.Height }; ;
        }
    }
}

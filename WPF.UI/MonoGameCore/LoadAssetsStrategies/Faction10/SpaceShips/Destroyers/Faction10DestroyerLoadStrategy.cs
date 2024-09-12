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
            var t = contentManager.Load<Texture2D>("Assets/SpaceShips/Faction10/destroyer256");
            
            assetStorage.AddAsset("destroyer", "Assets/SpaceShips/Faction10/destroyer256", t);
            
            (current as Sprite)!.Transform.TextureSize = new SizeF() { Width = t.Width, Height = t.Height }; ;
        }
    }
}

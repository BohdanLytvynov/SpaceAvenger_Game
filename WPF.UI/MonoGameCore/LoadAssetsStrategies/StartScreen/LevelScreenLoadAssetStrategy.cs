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

namespace WPF.UI.MonoGameCore.LoadAssetsStrategies.StartScreen
{
    internal class LevelScreenLoadAssetStrategy : ILoadAssetStrategy
    {
        public void LoadAsset(IAssetStorage assetStorage, ContentManager contentManager
            , GameObject current)
        {
            assetStorage.LoadAssets<Texture2D>
                (
                    ("back-1", "Backgrounds/Levels/Back1"),
                    ("back-2", "Backgrounds/Levels/Back2"),
                    ("back-3", "Backgrounds/Levels/Back3")
                );

            //Add Animations
            assetStorage.LoadAsset<Texture2D>
                ("puls-star1", "Animations/Environment/PulsatingStar");
        }
    }
}

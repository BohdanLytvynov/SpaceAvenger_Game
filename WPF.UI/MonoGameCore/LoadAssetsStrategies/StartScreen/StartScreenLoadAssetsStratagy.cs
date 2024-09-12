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
    internal class StartScreenLoadAssetsStratagy : ILoadAssetStrategy
    {
        public void LoadAsset(IAssetStorage assetStorage,
            ContentManager contentManager,
            GameObject current)
        {
            assetStorage.AddAssets(
                ("ui-back-choose-profile", "Backgrounds/UI/ChooseProfile",
                contentManager.Load<Texture2D>("Backgrounds/UI/ChooseProfile")),
                ("ui-back-levels", "Backgrounds/UI/ChooseProfile",
                contentManager.Load<Texture2D>("Backgrounds/UI/Levels")),
                ("ui-back-main", "Backgrounds/UI/ChooseProfile",
                contentManager.Load<Texture2D>("Backgrounds/UI/MainBack"))
                );
        }
    }
}

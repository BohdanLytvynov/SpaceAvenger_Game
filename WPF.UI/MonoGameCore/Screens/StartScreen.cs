using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObject.Base;
using MonoGame.Extensions.ScreenView.Base;

namespace WPF.UI.MonoGameCore.Screens
{
    internal class StartScreenUpdateArgs : IUpdateArgs<StartScreenType>
    {
        public StartScreenType Args { get; set; }

        public StartScreenUpdateArgs(StartScreenType startScreenType)
        {
            Args = startScreenType;
        }

        public StartScreenUpdateArgs()
        {
            Args = StartScreenType.Choose_profile;
        }
    }

    internal enum StartScreenType : byte 
    {
        Choose_profile,
        Main,
        Levels,
    }

    internal class StartScreen : GameObject
    {
        private StartScreenType _type;

        public StartScreen(
            string name, 
            ContentManager contentManager,
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch,
            IAssetStorage? assetStorage = null) 
            : base(name, contentManager, graphicsDevice, spriteBatch, assetStorage)
        {            
        }

        public override void Load()
        {
            if (Loaded)
                return;
            
            var storage = this.Storage;

            storage.AddAssets(
                ("ui-back-choose-profile", "Backgrounds/UI/ChooseProfile",
                ContentManager.Load<Texture2D>("Backgrounds/UI/ChooseProfile")),
                ("ui-back-levels", "Backgrounds/UI/ChooseProfile", 
                ContentManager.Load<Texture2D>("Backgrounds/UI/Levels")),
                ("ui-back-main", "Backgrounds/UI/ChooseProfile", 
                ContentManager.Load<Texture2D>("Backgrounds/UI/MainBack"))
                );

            base.Load();
        }

        public override void Draw(ref bool play)
        {
            base.Draw(ref play);

            switch (_type)
            {
                case StartScreenType.Choose_profile:
                    SpriteBatch.Draw(Storage["ui-back-choose-profile"] as Texture2D, 
                        Vector2.Zero, null, Color.White);
                    break;
                case StartScreenType.Main:
                    SpriteBatch.Draw(Storage["ui-back-main"] as Texture2D,
                        Vector2.Zero, null, Color.White);
                    break;
                case StartScreenType.Levels:
                    SpriteBatch.Draw(Storage["ui-back-levels"] as Texture2D,
                        Vector2.Zero, null, Color.White);
                    break;               
            }

            play = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="play"></param>
        public override void Update(IUpdateArgs args ,ref bool play)
        {
            base.Update(args ,ref play);

            _type = (args as StartScreenUpdateArgs)!.Args;
        }

        public override void UnLoad()
        {
            foreach( var ass in Storage.GetPaths(a => true))
            {
                ContentManager.UnloadAsset(ass);
            }

            base.UnLoad();
        }        
    }
}

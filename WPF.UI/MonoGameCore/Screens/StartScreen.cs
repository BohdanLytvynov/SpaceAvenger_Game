using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Screens.Base;

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

    internal class StartScreen : ScreenBase
    {
        private StartScreenType _type;

        public StartScreen( bool debug,
            string name, 
            ContentManager contentManager,            
            SpriteBatch spriteBatch,
            Rectangle ScreenResolution,
            ILoadAssetStrategy loadAssetStrategy,
            IAssetStorage? assetStorage = default
            ) 
            : base(debug, name, 
                  contentManager, 
                  spriteBatch, 
                  ScreenResolution,
                  loadAssetStrategy,
                  assetStorage)
        {
            _type = StartScreenType.Choose_profile;
        }
       
        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);

            switch (_type)
            {
                case StartScreenType.Choose_profile:
                    SpriteBatch.Draw(Storage["ui-back-choose-profile"] as Texture2D, 
                        Vector2.Zero, ScreenDimensions, Color.White);
                    break;
                case StartScreenType.Main:
                    SpriteBatch.Draw(Storage["ui-back-main"] as Texture2D,
                        Vector2.Zero, ScreenDimensions, Color.White);
                    break;
                case StartScreenType.Levels:
                    SpriteBatch.Draw(Storage["ui-back-levels"] as Texture2D,
                        Vector2.Zero, ScreenDimensions, Color.White);
                    break;               
            }

            play = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="play"></param>
        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time ,ref play);

            _type = (args as StartScreenUpdateArgs)!.Args;
        }
              
    }
}

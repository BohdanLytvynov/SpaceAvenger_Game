using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.GameObject.Base;
using MonoGame.Extensions.ScreenView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.UI.MonoGameCore.Screens
{
    internal class LevelScreen : GameObject
    {
        Random random;

        int random_back = 0;

        public LevelScreen(string name, ContentManager contentmanager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, IAssetStorage? assetStorage = null) 
            : base(name, contentmanager, graphicsDevice, spriteBatch, assetStorage)
        {
            random = new Random();

            random_back = random.Next(1, 4);
        }

        public override void Load()
        {
            Storage.AddAssets
                (
                    ("back-1", "Backgrounds/Levels/Back1", 
                    ContentManager.Load<Texture2D>("Backgrounds/Levels/Back1")),
                    ("back-2", "Backgrounds/Levels/Back2",
                    ContentManager.Load<Texture2D>("Backgrounds/Levels/Back2")),
                    ("back-3", "Backgrounds/Levels/Back3",
                    ContentManager.Load<Texture2D>("Backgrounds/Levels/Back3"))
                );

            base.Load();
        }

        public override void UnLoad()
        {            
            base.UnLoad();
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time, ref play);


        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);

            SpriteBatch.Draw(Storage[$"back-{random_back}"] as Texture2D,
                Vector2.Zero, null, Color.White);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}

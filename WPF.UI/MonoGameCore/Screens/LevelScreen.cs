using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Animations.Interfaces.AnimationFrames;
using MonoGame.Extensions.Animations.Realizations.AnimationFrames;
using MonoGame.Extensions.Animations.Realizations.Animations;
using MonoGame.Extensions.Animations.Utilities;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors;
using MonoGame.Extensions.Behaviors.MouseInteractable;
using MonoGame.Extensions.GameObject.Base;
using MonoGame.Extensions.Screens.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WPF.UI.MonoGameControls;
using WPF.UI.MonoGameCore.Ships;

namespace WPF.UI.MonoGameCore.Screens
{
    internal class LevelScreen : ScreenBase, IMouseInteractable, IKeyBoardInteractable
    {
        SpaceShip m_Player;         

        Random random;

        int random_back = 0;

        Animation? m_PulsatingStar;

        MouseStateArgs? m_MouseStateArgs;

        KeyEventArgs? m_KeyEventArgs;

        public LevelScreen(string name, 
            ContentManager contentmanager, 
            SpriteBatch spriteBatch, 
            Rectangle screenResolutions,
            IAssetStorage? assetStorage = default) 
            : base(name, 
                  contentmanager, 
                  spriteBatch, 
                  screenResolutions,
                  assetStorage)
        {
            random = new Random();

            random_back = random.Next(1, 4);

            m_Player = new SpaceShip("Player",
                ContentManager,
                SpriteBatch,                
                new Transform(new(50f, 50f), 0, new (0.5f, 0.5f)));
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

            //Add Animations
            Storage.AddAssets(
                ("puls-star1", "Animations/Environment/PulsatingStar",
                ContentManager.Load<Texture2D>("Animations/Environment/PulsatingStar"))
                );

            //Configure Animations

            var puls_star_animatinFrames = AnimationUtilities.BuildAnimationFrames(6, 1);

            m_PulsatingStar = new Animation((Texture2D)Storage["puls-star1"], 1, 6, true, 2,
                puls_star_animatinFrames, Color.White);

            //Load Space - Ships
            
            m_Player.Load();

            base.Load();
        }

        public override void UnLoad()
        {
            //Stop Animations
            m_PulsatingStar!.Stop();
            
            m_Player!.UnLoad();

            base.UnLoad();
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time, ref play);

            //Animations
            m_PulsatingStar!.Start(time);
            m_PulsatingStar!.Update(time);
            m_Player!.Update(args, time, ref play);

            //Check if Either object is selected by Mouse

            if (m_MouseStateArgs is not null &&
                m_MouseStateArgs!.LeftButton == 
                Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                var mouseCords = m_MouseStateArgs.Position;


            }
        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);

            SpriteBatch.Draw(Storage[$"back-{random_back}"] as Texture2D,
                Vector2.Zero, null, Color.White);

            //Animations
            m_PulsatingStar!.Draw(time, SpriteBatch, Vector2.Zero);

            m_Player!.Draw(time, ref play);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void MouseAction(object mouseState)
        {
            m_MouseStateArgs = mouseState as MouseStateArgs;
        }

        public void KeyBoardAction(object keyBoardState)
        {
            m_KeyEventArgs = keyBoardState as KeyEventArgs;
        }
    }
}

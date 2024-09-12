using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.Animations.Realizations.Animations;
using MonoGame.Extensions.Animations.Utilities;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors;
using MonoGame.Extensions.Behaviors.MouseInteractable;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Physics.Interfaces;
using MonoGame.Extensions.Physics.Realizations;
using MonoGame.Extensions.Screens.Base;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Navigation;
using WPF.UI.MonoGameControls;
using WPF.UI.MonoGameCore.Engines.PlasmaEngines;
using WPF.UI.MonoGameCore.Engines.Realizations;
using WPF.UI.MonoGameCore.LoadAssetsStrategies.Faction10.Engines.PlasmaEngines;
using WPF.UI.MonoGameCore.LoadAssetsStrategies.Faction10.SpaceShips.Destroyers;
using WPF.UI.MonoGameCore.Ships;

namespace WPF.UI.MonoGameCore.Screens
{
    internal class LevelScreen : ScreenBase, IMouseInteractable, IKeyBoardInteractable
    {
        private bool m_drawLine;

        private Vector2 m_SelectedPosition;

        private SpaceShip m_Player;

        private List<GameObject> m_gameObjects;//Scene interactable GameObjects

        private Random random;

        private int random_back = 0;

        private Animation? m_PulsatingStar;

        private MouseStateArgs? m_MouseStateArgs;

        private KeyEventArgs? m_KeyEventArgs;

        Func<GameTime, float> m_thrustCalcFunction;

        public LevelScreen(bool debug, string name, 
            ContentManager contentmanager, 
            SpriteBatch spriteBatch, 
            Rectangle screenResolutions,
            ILoadAssetStrategy loadAssetStrategy,
            IAssetStorage? assetStorage = default) 
            : base(debug, name, 
                  contentmanager, 
                  spriteBatch, 
                  screenResolutions,
                  loadAssetStrategy,
                  assetStorage)
        {
            m_drawLine = false;

            m_gameObjects = new List<GameObject>();

            random = new Random();

            random_back = random.Next(1, 4);

            m_thrustCalcFunction = new Func<GameTime, float>(
                (time) => 1f * (float)time.ElapsedGameTime.TotalSeconds);

            m_Player = new SpaceShip("Player",
                ContentManager,
                SpriteBatch,
                Debugging,
                mass: 40f, //megaTones
                modules: new List<IRigidBodyObject>()
                {
                    new IntegratedPlasmaEngine(Debugging, "MainEngine1", 
                    contentmanager, spriteBatch, 14f, 3f,
                    new Transform(),
                    new PlasmaEngineLoadStrategy(),
                    m_thrustCalcFunction),

                    new IntegratedPlasmaEngine(Debugging, "MainEngine2",
                    contentmanager, spriteBatch, 14f, 3f,
                    new Transform(),
                    new PlasmaEngineLoadStrategy(),
                    m_thrustCalcFunction),
                    /*new PlasmaEngine(2, 7f, 1, 1f), 
                    new PlasmaEngine(3, 7f, 1, 1f)*/ 
                },
                new CalculateMOIForTriangleShape_Z(40f, 51.2f, 51.2f),
                new Faction10DestroyerLoadStrategy(),
                new Transform(new(100f, 100f), 0f, new(0.4f, 0.4f))
                );
        }

        public override void Load()
        {
            //Load Sprites and animations
            base.Load();

            //Configure Animations
            var puls_star_animatinFrames = AnimationUtilities.BuildAnimationFrames(6, 1);

            m_PulsatingStar = new Animation((Texture2D)Storage["puls-star1"], 1, 6, true, 2,
                puls_star_animatinFrames, Color.White);

            //Load Space - Ships
            
            m_Player.Load();            
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

            foreach (var obj in m_gameObjects)
            {
                obj.Update(args, time, ref play);
            }

            //Check if Either object is selected by Mouse

            if (m_MouseStateArgs is not null &&
                m_MouseStateArgs!.LeftButton ==
                Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                var mouseCords = m_MouseStateArgs.Position;

                if (mouseCords.X >= m_Player.Transform.UpperLeftCorner.X &&
                    mouseCords.X <= m_Player.Transform.UpperLeftCorner.X +
                    m_Player.Transform.ActualSize.Width &&
                    mouseCords.Y >= m_Player.Transform.UpperLeftCorner.Y &&
                    mouseCords.Y <= m_Player.Transform.UpperLeftCorner.Y +
                    m_Player.Transform.ActualSize.Height)
                {
                    m_Player.Select();

                    m_drawLine = true;
                }
                else
                { 
                    m_SelectedPosition = mouseCords;
                   
                    m_Player.Move(m_SelectedPosition);
                }
            }
            else if (m_MouseStateArgs is not null &&
                m_MouseStateArgs!.RightButton ==
                Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (m_Player.Selected)
                {
                    m_Player.Deselect();

                    m_drawLine = false;
                }
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

            if (m_SelectedPosition != Vector2.Zero && m_drawLine)
                SpriteBatch.DrawLine(m_Player.Transform.Position, 
                    m_SelectedPosition, Color.Green);
           
            foreach (var obj in m_gameObjects)
            {
                obj.Draw(time, ref play);
            }
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

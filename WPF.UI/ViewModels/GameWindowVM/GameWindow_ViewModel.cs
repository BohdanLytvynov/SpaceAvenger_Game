using WPF.UI.Attributes.PageManager;
using WPF.UI.ViewModels.Base;
using System;
using WPF.UI.Services.Interfaces.MessageBus;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using WPF.UI.Services.Interfaces.Message;
using WPF.UI.Services.Realizations.Message;
using WPF.UI.Services.Interfaces.PageManager;
using WPF.UI.Enums.FrameTypes;
using WPF.UI.Views.Pages;
using System.Collections.Generic;
using System.Windows;
using Point = Microsoft.Xna.Framework.Point;


namespace WPF.UI.ViewModels.GameWindowVM
{
    internal class GameWindow_ViewModel : SubscriptableMonoGameViewModel
    {
        #region Fields
        private IMessageBus? m_msgBus;

        private IPageManagerService<FrameType>? m_pageManager;
        #endregion

        #region Game 

        #region Fields
        //Indicates the current Game State
        bool m_play;

        Random random;

        private SpriteBatch _spriteBatch = default!;

        private Texture2D? m_Currentbackground;

        SortedDictionary<string, object> m_AssetStore;

        Point m_screen_Dimensions;

        #endregion

        #region Ctors
        //Will be called first

        public GameWindow_ViewModel()
        {
            random = new Random();

            m_play = true;

            m_AssetStore = new SortedDictionary<string, object>();            
        }

        public GameWindow_ViewModel(IPageManagerService<FrameType> pm, IMessageBus bus) : this()
        {
            m_pageManager = pm;

            m_msgBus = bus;
            
            Subscriptions.Add(m_msgBus.RegisterHandler<IGameMessage>(OnMessageRecieved));
        }

        #endregion
        /// <summary>
        /// Will be called after Constructor Finishes Executing
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        #region Game Cycle
        /// <summary>
        /// Will be called After Initialization of the Game
        /// </summary>
        public override void LoadContent()
        {
            m_screen_Dimensions = new Point(
                (int)App.Current.MainWindow.ActualWidth,
                (int)App.Current.MainWindow.ActualHeight
                );

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            m_AssetStore.Add("Background-ChooseProfile",
                Content.Load<Texture2D>("Backgrounds/UI/ChooseProfile/0"));

            m_Currentbackground = m_AssetStore["Background-ChooseProfile"] as Texture2D;

            base.LoadContent();
        }

        /// <summary>
        /// The part of the game cycle. Calls Permanently 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (!m_play)
                return;

#if DEBUG
            Debug.WriteLine($"Update Runs: {m_play}");
#endif

            base.Update(gameTime);

        }
        /// <summary>
        /// The part of the game cycle. Calls Permanently after Update
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (!m_play)
                return;
#if DEBUG
            Debug.WriteLine($"Draw Runs: {m_play}");
#endif
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(m_Currentbackground, Vector2.Zero, null, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);

        }
        /// <summary>
        /// Will be Called when game recieve close command(When Game Window will Close)
        /// After this call Finalizer will be called
        /// </summary>
        public override void UnloadContent()
        {
            m_Currentbackground?.Dispose();

            _spriteBatch?.Dispose();

            base.UnloadContent();
        }
        #endregion

        #region Message Bus Handlers

        private void OnMessageRecieved(IGameMessage msg)
        {
            if (msg is FinishGame || msg is PauseGame && m_play)
            {
                m_play = false;
            }
            else if (msg is ResumeGame && !m_play)
            {
                m_play = true;
            }
            else if (msg is SetGameLevel)
            {
                // Change Level Environment
            }
        }

        #endregion
        /// <summary>
        /// Will be called in case of disposing the MainWindow with game
        /// </summary>
        public override void Dispose()
        {
            Unsubscribe();

            base.Dispose();
        }

        #endregion
    }

}

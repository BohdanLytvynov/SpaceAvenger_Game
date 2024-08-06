using WPF.UI.ViewModels.Base;
using WPF.UI.Services.Interfaces.MessageBus;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using WPF.UI.Services.Interfaces.Message;
using WPF.UI.Services.Realizations.Message;
using WPF.UI.Services.Interfaces.PageManager;
using WPF.UI.Enums.FrameTypes;
using Point = Microsoft.Xna.Framework.Point;
using WPF.UI.MonoGameCore.Screens;
using SharpDX.Win32;
using System;


namespace WPF.UI.ViewModels.GameWindowVM
{
    internal unsafe class GameWindow_ViewModel : SubscriptableMonoGameViewModel
    {
        #region Fields
        private IMessageBus? m_msgBus;

        private IPageManagerService<FrameType>? m_pageManager;
        #endregion

        #region Game 

        #region Fields
        //Indicates the current Game State
        bool m_play;
        
        private SpriteBatch _spriteBatch = default!;

        private StartScreen? m_startScreen;

        private StartScreenUpdateArgs? m_startScreenUpdateArgs;
        
        Point m_screen_Dimensions;
                        
        #endregion

        #region Ctors
        //Will be called first

        public GameWindow_ViewModel()
        {            
            m_play = true;

            m_startScreenUpdateArgs = new();
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Init Screens

            m_startScreen = new StartScreen("Start", Content, GraphicsDevice, _spriteBatch);

            m_startScreen!.Load();

            m_screen_Dimensions = new Point(
                (int)App.Current.MainWindow.ActualWidth,
                (int)App.Current.MainWindow.ActualHeight
                );
                        
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

            m_startScreen!.Update(m_startScreenUpdateArgs!, ref m_play);

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
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            m_startScreen!.Draw(ref m_play);

            _spriteBatch.End();

            base.Draw(gameTime);

        }
        /// <summary>
        /// Will be Called when game recieve close command(When Game Window will Close)
        /// After this call Finalizer will be called
        /// </summary>
        public override void UnloadContent()
        {
            if(m_startScreen!.Loaded)
                m_startScreen!.UnLoad();
            
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
            else if (msg is SetStartScreen start)
            {                
                m_startScreenUpdateArgs!.Args = start.Args;
            }
        }

        #endregion
        /// <summary>
        /// Will be called in case of disposing the MainWindow with game
        /// </summary>
        public override void Dispose()
        {
            Unsubscribe();

            if (!GraphicsDevice.IsDisposed)
                GraphicsDevice.Dispose();

            if (!_spriteBatch.IsDisposed)
                _spriteBatch?.Dispose();

            if(!m_startScreen!.Disposed)
                m_startScreen?.Dispose();

            base.Dispose();
        }

        #endregion
    }

}

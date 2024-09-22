using WPF.UI.ViewModels.Base;
using WPF.UI.Services.Interfaces.MessageBus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WPF.UI.Services.Interfaces.Message;
using WPF.UI.Services.Realizations.Message;
using WPF.UI.Services.Interfaces.PageManager;
using WPF.UI.Enums.FrameTypes;
using WPF.UI.MonoGameCore.Screens;
using System.Collections.Generic;
using MonoGame.Extensions.Screens.Base;
using WPF.UI.MonoGameControls;
using System.Windows.Input;
using MonoGame.Extensions.Behaviors.MouseInteractable;
using WPF.UI.MonoGameCore.LoadAssetsStrategies.StartScreen;

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
        
        private SpriteBatch _spriteBatch = default!;
       
        private ScreenBase? m_currentScreen;

        private List<ScreenBase?> m_screens;

        private StartScreenUpdateArgs? m_startScreenUpdateArgs;
        
        Rectangle m_screen_Dimensions;
                                
        #endregion

        #region Ctors
        //Will be called first

        public GameWindow_ViewModel()
        {                                    
            m_startScreenUpdateArgs = new();

            m_screens = new List<ScreenBase?>();

            OnContentUnloaded += () => { this.Dispose(); };

            m_play = true;
        }

        public GameWindow_ViewModel(IPageManagerService<FrameType> pm, IMessageBus bus) : this()
        {
            m_pageManager = pm;

            m_msgBus = bus;
            
            Subscriptions.Add(m_msgBus.RegisterHandler<IGameMessage>(OnMessageRecieved));

            Subscriptions.Add(m_msgBus.RegisterHandler<SetStartScreen>(OnMessageRecieved));
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

            m_screen_Dimensions = new Rectangle(0,0,
                (int)App.Current.MainWindow.ActualWidth,
                (int)App.Current.MainWindow.ActualHeight);

            m_currentScreen = new StartScreen(IsDebugging, "StartScreen", 
                Content,
                _spriteBatch,
                m_screen_Dimensions,
                new StartScreenLoadAssetsStratagy());
           
            m_screens.Add(m_currentScreen);

            m_currentScreen!.Load();
                                    
            base.LoadContent();

            //m_play = true;
        }

        /// <summary>
        /// The part of the game cycle. Calls Permanently 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (!m_play)
                return;

            m_currentScreen!.Update(m_startScreenUpdateArgs!, gameTime, ref m_play);

#if DEBUG
            //Debug.WriteLine($"Update Runs: {m_play}");
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
            //Debug.WriteLine($"Draw Runs: {m_play}");
#endif
            GraphicsDevice.Clear(Color.DarkBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            m_currentScreen!.Draw(gameTime, ref m_play);

            _spriteBatch.End();

            base.Draw(gameTime);

        }
        /// <summary>
        /// Will be Called when game recieve close command(When Game Window will Close)
        /// After this call Finalizer will be called
        /// </summary>
        public override void UnloadContent()
        {
            foreach (var screen in m_screens)                            
                screen!.UnLoad();
                                   
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
            else if (msg is SetLevel level)
            {
                m_play = false;

                var name = level.Args;

                var levelScreen = new LevelScreen(IsDebugging, 
                    name, 
                    Content, 
                    _spriteBatch,                     
                    m_screen_Dimensions,
                    new LevelScreenLoadAssetStrategy());

                levelScreen.Load();

                m_screens.Add(levelScreen);

                m_currentScreen = levelScreen;
               
                m_play = true;
            }
        }

        private void OnMessageRecieved(SetStartScreen message)
        {
            m_startScreenUpdateArgs!.Args = message.Args;
        }

        #endregion
        /// <summary>
        /// Will be called in case of disposing the MainWindow with game
        /// </summary>
        public override void Dispose()
        {
            Unsubscribe();

            if (!GraphicsDevice.IsDisposed)
                GraphicsDevice?.Dispose();

            if (!_spriteBatch.IsDisposed)
                _spriteBatch?.Dispose();

            foreach (var screen in m_screens)
            { 
                if(!screen!.Disposed)
                    screen!.Dispose();
            }

            base.Dispose();
        }

        #endregion

        #region Mouse KeyBoard Input System

        public override void OnMouseDown(MouseStateArgs mouseState)
        {
            // Say to the current screen about Mouse Pressed Event!
            (m_currentScreen as IMouseInteractable)?.MouseAction(mouseState);
        }

        public override void KeyDownHandler(object sender, KeyEventArgs e)
        {
            (m_currentScreen as IKeyBoardInteractable)?.KeyBoardAction(e);
        }

        #endregion
    }

}

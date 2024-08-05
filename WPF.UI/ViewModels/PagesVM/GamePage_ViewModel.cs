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

namespace WPF.UI.ViewModels.PagesVM
{
    [ReflexionDetectionIgnore]
    internal class GamePage_ViewModel : SubscriptableMonoGameViewModel
    {
        #region Fields
        private IMessageBus? m_msgBus;

        private IPageManagerService<FrameType>? m_pageManager;
        #endregion

        #region Game 

        bool m_play;

        bool m_start;

        Random random;

        private SpriteBatch _spriteBatch = default!;

        private Texture2D? m_back;

        public GamePage_ViewModel()
        {
            random = new Random();

            m_play = true;

            m_start = true;
        }

        public GamePage_ViewModel(IPageManagerService<FrameType> pm, IMessageBus bus) : this()
        {
            m_pageManager = pm;

            m_msgBus = bus;

            Subscriptions.Add(m_msgBus.RegisterHandler<IGameMessage>(OnMessageRecieved));
        }

        #region Game Cycle
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var backs = Content.RootDirectory + Path.DirectorySeparatorChar + "Backgrounds";

            var filesCount = Directory.GetFiles(backs).Length;

            m_back = Content.Load<Texture2D>($"Backgrounds/Back{random.Next(1, filesCount + 1)}");

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            m_back?.Dispose();

            _spriteBatch?.Dispose();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (m_play)
            {
                base.Update(gameTime);
            }            
        }

        public override void Draw(GameTime gameTime)
        {
            if (m_start)
            {
                GraphicsDevice.Clear(Color.Green);

                // Start Logo
                                
                m_start = !m_start;

                m_play = !m_play;

                m_pageManager!.SwitchPage(nameof(ChooseProfile_Page), FrameType.MainFrame);
            }

            if (m_play)
            {
                Debug.WriteLine($"Activated: {m_play}");

                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Begin();

                _spriteBatch.Draw(m_back, Vector2.Zero, Color.White);

                _spriteBatch.End();

                base.Draw(gameTime);
            }                        
        }
        #endregion

        #region Message Bus Handlers

        private void OnMessageRecieved(IGameMessage msg)
        {            
            if (msg is StartGame)
            {
                m_play = true;
            }
            else if (msg is FinishGame || msg is PauseGame)
            {
                m_play = false;
            }
            else if (msg is ResumeGame)
            {
                m_play = true;
            }
            else if (msg is SetGameLevel)
            {
                // Change Level Environment
            }
        }

        #endregion

        //public override void Dispose()
        //{
        //    Unsubscribe();

        //    Subscriptions.Clear();

        //    base.Dispose();
        //}

        #endregion
    }

}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using WPF.UI.Enums.ModuleTypes;
using WPF.UI.MonoGameCore.Engines.Interfaces;
using WPF.UI.MonoGameCore.Modules;

namespace WPF.UI.MonoGameCore.Engines.Realizations
{
    internal class PlasmaEngine : Module, IEngine
    {
        #region Fields
        private float m_currentThrust;

        private bool m_Working;

        private float m_increment;
        #endregion

        #region Properties

        public float CurrentThrust { get => m_currentThrust; }

        public float MaxThrust { get; }                

        public bool Working => m_Working;

        public float Increment => m_increment;

        #endregion

        #region Ctor

        public PlasmaEngine(
            string name,
            ContentManager contentManager,
            SpriteBatch spriteBatch,
            float maxThrust, 
            float increment,
            float mass,
            ITransformable transform,
            IAssetStorage? assetStorage = default) :
            base(name, contentManager, spriteBatch, mass, transform, assetStorage)
        {
            m_moduleType = ModuleType.Engine;

            MaxThrust = maxThrust;
           
            Mass = mass;

            m_currentThrust = 0f;

            m_Working = false;

            m_increment = increment;
        }

        public void Start()
        {            
            m_Working = true;
        }

        public void Stop()
        {
            m_currentThrust = 0f;
            m_Working = false;
        }

        public void Increase(GameTime gameTime)
        {
            if (m_Working && m_currentThrust < MaxThrust)
            {
                m_currentThrust += m_increment * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Load()
        {


            base.Load();
        }

        #endregion


    }
}

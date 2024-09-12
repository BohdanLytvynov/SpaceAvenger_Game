using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;
using WPF.UI.Enums.ModuleTypes;
using WPF.UI.MonoGameCore.Engines.Interfaces;
using WPF.UI.MonoGameCore.Modules;

namespace WPF.UI.MonoGameCore.Engines.Realizations
{
    internal class EngineBase : Module, IEngine
    {
        #region Fields
        private float m_currentThrust;

        private bool m_Working;

        private Func<GameTime, float> m_IncreaseCalc;
        #endregion

        #region Properties

        public float CurrentThrust { get => m_currentThrust; }

        public float MaxThrust { get; }                

        public bool Working => m_Working;        

        #endregion

        #region Ctor

        public EngineBase( 
            bool debug,
            string name,
            ContentManager contentManager,
            SpriteBatch spriteBatch,
            float maxThrust,             
            float mass,
            ITransformable transform,
            ILoadAssetStrategy loadAssetStrategy,
            Func<GameTime, float> IncreaseCalcFunction,
            IAssetStorage? assetStorage = default) :
            base(debug, name, contentManager, spriteBatch, mass, transform, assetStorage, 
                loadAssetStrategy)
        {
            m_moduleType = ModuleType.Engine;

            MaxThrust = maxThrust;
           
            Mass = mass;

            m_currentThrust = 0f;

            m_Working = false;   
            
            m_IncreaseCalc = IncreaseCalcFunction;
        }

        public virtual void Start()
        {            
            m_Working = true;
        }

        public virtual void Stop()
        {
            m_currentThrust = 0f;
            m_Working = false;
        }
       
        public override void Load()
        {
            base.Load();
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time, ref play);

            if (m_Working && m_currentThrust < MaxThrust)
            {
                m_currentThrust += m_IncreaseCalc(time);
            }
        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);
        }

        #endregion
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Animations.Interfaces.AnimationManagers;
using MonoGame.Extensions.Animations.Realizations.AnimationManagers;
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
    internal enum EngineState
    { 
        idle = 1,
        move,
        stop
    }

    internal abstract class EngineBase : Module, IEngine
    {
        #region Fields
        protected IAnimationManager m_animationManager;

        private float m_currentThrust;

        private EngineState m_Mode;

        private Func<GameTime, float>? m_IncreaseCalc;
        #endregion

        #region Properties

        public float CurrentThrust { get => m_currentThrust; }

        public float MaxThrust { get; }                

        public EngineState Mode => m_Mode;        

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
            Func<GameTime, float>? IncreaseCalcFunction,
            ILoadAssetStrategy? loadAssetStrategy = default,            
            IAssetStorage? assetStorage = default) :
            base(debug, name, contentManager, spriteBatch, mass, transform, assetStorage, 
                loadAssetStrategy)
        {
            m_moduleType = ModuleType.Engine;

            MaxThrust = maxThrust;
           
            Mass = mass;

            m_currentThrust = 0f;

            m_Mode = EngineState.idle;   
            
            m_IncreaseCalc = IncreaseCalcFunction;

            m_animationManager = new AnimationManager();           
        }

        #endregion

        public virtual void Start(GameTime time)
        {           
            m_Mode = EngineState.move;
        }

        public virtual void Stop()
        {
            m_currentThrust = 0f;
            m_Mode = EngineState.stop;
        }
                       
        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time, ref play);

            m_animationManager.Start();
          
            switch (Mode)
            {
                case EngineState.idle:

                    if (!m_animationManager.Current_Animation_Name.Equals(EngineState.idle.ToString()))
                    {
                        m_animationManager.SetAnimationForPlay(EngineState.idle.ToString(), true);
                    }

                    break;
                case EngineState.move:

                    if (!m_animationManager.Current_Animation_Name.Equals(EngineState.move.ToString()))
                    {
                        m_animationManager.SetAnimationForPlay(EngineState.move.ToString(), true);
                    }

                    if (m_currentThrust < MaxThrust)
                    {
                        if (m_IncreaseCalc is not null)
                        {
                            m_currentThrust += m_IncreaseCalc(time);
                        }
                        else
                        {
                            m_currentThrust = MaxThrust;
                        }                        
                    }

                    break;
                case EngineState.stop:

                    if (!m_animationManager.Current_Animation_Name.Equals(EngineState.stop.ToString()))
                    {
                        m_animationManager.SetAnimationForPlay(EngineState.stop.ToString(), true);
                    }

                    break;               
            }

            m_animationManager.Update(time);
        }

        public override void Draw(GameTime time, ref bool play)
        {
            m_animationManager.Draw(time, SpriteBatch, Transform, 0.9f);

            base.Draw(time, ref play);
        }
    }
}

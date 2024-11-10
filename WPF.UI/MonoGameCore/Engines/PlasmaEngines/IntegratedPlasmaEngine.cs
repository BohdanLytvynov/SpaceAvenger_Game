using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using MonoGame.Extensions.Animations.Realizations.Animations;
using MonoGame.Extensions.Animations.Utilities;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.Base;
using System;
using WPF.UI.MonoGameCore.Engines.Realizations;

namespace WPF.UI.MonoGameCore.Engines.PlasmaEngines
{
    internal class IntegratedPlasmaEngine : EngineBase
    {        
        public IntegratedPlasmaEngine(
            bool debug,
            string name, 
            ContentManager contentManager, 
            SpriteBatch spriteBatch, 
            float maxThrust, 
            float mass, 
            ITransformable transform,              
            Func<GameTime, float>? IncreaseCalcFunction = default, 
            IAssetStorage? assetStorage = default) 
            : base(debug, 
                  name, 
                  contentManager, 
                  spriteBatch, 
                  maxThrust, 
                  mass, 
                  transform,
                  IncreaseCalcFunction                                    
                  )
        {
            
        }

        public override void Load()
        {
            base.Load();

            Storage.LoadAssets<Texture2D>(
                ("idle", "Animations/Jets/Jet-1/Jet1-Idle"),
                ("move", "Animations/Jets/Jet-1/Jet1-Move"));

            IAnimation jet1_idle = new Animation(
                (Texture2D)Storage["idle"],
                6, 4, true, 1f, AnimationUtilities.BuildAnimationFrames(24, 2f),
                Color.White
                );

            IAnimation jet1_move = new Animation(
                (Texture2D)Storage["move"],
                6, 4, false, 0.001f, AnimationUtilities.BuildAnimationFrames(24, 1f),
                Color.White
                );

            IAnimation jet1_stop = new Animation(
                (Texture2D)Storage["move"],
                6, 4, false, 1f, AnimationUtilities.BuildAnimationFrames(24, 2f),
                Color.White, true
                );

            m_animationManager.AddAnimation(EngineState.idle.ToString(), jet1_idle);
            m_animationManager.AddAnimation(EngineState.move.ToString(), jet1_move);
            m_animationManager.AddAnimation(EngineState.stop.ToString(), jet1_stop);

            m_animationManager.SetAnimationForPlay(EngineState.move.ToString());
        }        
    }
}

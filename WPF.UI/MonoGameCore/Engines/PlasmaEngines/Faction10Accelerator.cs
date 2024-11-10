using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using MonoGame.Extensions.Animations.Realizations.Animations;
using MonoGame.Extensions.Animations.Utilities;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.Behaviors.Transformables.RelativeTransform;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;
using WPF.UI.MonoGameCore.Engines.Realizations;

namespace WPF.UI.MonoGameCore.Engines.PlasmaEngines
{
    internal class Faction10Accelerator : EngineBase
    {        
        public Faction10Accelerator(bool debug,
            string name,
            ContentManager contentManager,
            SpriteBatch spriteBatch,
            float maxThrust, float mass,
            ITransformable transform,
            ILoadAssetStrategy? loadAssetStrategy = default,
            IAssetStorage? assetStorage = default,
            Func<GameTime, float>? IncreaseCalcFunction = default) :
            base(debug,
                name,
                contentManager,
                spriteBatch,
                maxThrust,
                mass,
                transform,
                IncreaseCalcFunction,
                loadAssetStrategy,
                assetStorage)
        {
        }

        public override void Load()
        {             
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
            
            base.Load();
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time, ref play);

            var relTransform = (this.Transform as IRelativeTransform)!;

            relTransform.CalculateAbsoluteTransform();
        }     
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
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
            ILoadAssetStrategy loadAssetStrategy, 
            Func<GameTime, float> IncreaseCalcFunction, 
            IAssetStorage? assetStorage = null) 
            : base(debug, 
                  name, 
                  contentManager, 
                  spriteBatch, 
                  maxThrust, 
                  mass, 
                  transform, 
                  loadAssetStrategy,
                  IncreaseCalcFunction, 
                  assetStorage)
        {
            
        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);

            //Position in the only Positive coordinate system 
            var position_onlypos = Transform.Position;

            //Convert to the Non-Only Positive Coord System

            var position_2 = new Vector2(
                position_onlypos.X - Transform.ActualSize.Width/2,
                position_onlypos.Y - Transform.ActualSize.Height/2
                );

            //SpriteBatch.Draw();
        }
    }
}

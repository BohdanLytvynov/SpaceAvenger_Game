using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

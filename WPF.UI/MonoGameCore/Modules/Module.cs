using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Physics.Interfaces;
using MonoGame.Extensions.Sprites.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.UI.Enums.ModuleTypes;

namespace WPF.UI.MonoGameCore.Modules
{
    internal abstract class Module : Sprite, IModule
    {
        protected ModuleType m_moduleType;

        public Module(
            string name, 
            ContentManager 
            contentManager, 
            SpriteBatch spriteBatch, 
            float mass,
            ITransformable? transform, 
            IAssetStorage? assetStorage,
            ILoadAssetStrategy loadAssetStrategy) : 
            base(name, contentManager, spriteBatch, transform, assetStorage, loadAssetStrategy)
        {
            Mass = mass;            
        }

        public float Mass { get; set; }

        public ModuleType Type => m_moduleType;
    }
}

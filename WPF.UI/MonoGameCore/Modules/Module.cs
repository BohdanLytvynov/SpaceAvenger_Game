using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Sprites.Realization;
using WPF.UI.Enums.ModuleTypes;

namespace WPF.UI.MonoGameCore.Modules
{
    internal abstract class Module : Sprite, IModule
    {
        protected ModuleType m_moduleType;       

        public Module(bool debug, string name, 
            ContentManager 
            contentManager, 
            SpriteBatch spriteBatch, 
            float mass,
            ITransformable? transform, 
            IAssetStorage? assetStorage,
            ILoadAssetStrategy? loadAssetStrategy) : 
            base(name, 
                contentManager,
                spriteBatch,
                transform,
                assetStorage, 
                loadAssetStrategy,
                debug)
        {
            Mass = mass;            
        }

        public float Mass { get; set; }

        public ModuleType Type => m_moduleType;        
    }
}

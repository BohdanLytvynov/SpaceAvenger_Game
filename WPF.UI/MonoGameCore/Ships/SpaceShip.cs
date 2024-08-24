using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extensions.Animations.Interfaces.AnimationManagers;
using MonoGame.Extensions.Animations.Realizations.AnimationManagers;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors;
using MonoGame.Extensions.Behaviors.Selectables;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObject.Base;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using WPF.UI.MonoGameCore.Modules;

namespace WPF.UI.MonoGameCore.Ships
{
    internal class SpaceShip : GameObject, ISelectable
    {
        private ITransformable m_transform;

        private IAnimationManager m_animationManager;

        private IEnumerable<IModule> m_modules;

        private bool m_selected;

        public bool Selected => m_selected;
        
        public SpaceShip(string name, 
            ContentManager contentManager, 
            SpriteBatch spriteBatch,             
            ITransformable transform,
            IAssetStorage? assetStorage = default) 
            : base(name, 
                  contentManager, 
                  spriteBatch, 
                  assetStorage
                  )
        {
            m_animationManager = new AnimationManager();

            m_modules = new List<IModule>();

            m_selected = false;

            m_transform = transform;
        }
        
        public override void Load()
        {
            base.Load();

            var t = ContentManager.Load<Texture2D>("Assets/SpaceShips/Faction10/destroyer256");

            Storage.AddAsset("destroyer", "Assets/SpaceShips/Faction10/destroyer256", t);            
        }

        public override void UnLoad()
        {
            base.UnLoad();                 
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {            
            base.Update(args, time, ref play);
        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);
            var t = (Texture2D)Storage["destroyer"];
            SpriteBatch.Draw(t, m_transform.Position, null, Color.White, 0f, 
                Vector2.Zero, m_transform.Scale, SpriteEffects.None, 0f );

            //Draw The Selection
            if (m_selected)
            {
                SpriteBatch.DrawLine(m_transform.Position, 
                    new Vector2(m_transform.Position.X + t.Width * m_transform.Scale.X, 
                    m_transform.Position.Y), Color.Green, 2);
            }            
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void Select()
        {
            if (!Selected)
                m_selected = true;
        }

        public void Deselect()
        {
            if (Selected)
                m_selected = false;
        }
    }
}

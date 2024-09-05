using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Selectables;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.Extensions;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.Physics.Interfaces;
using MonoGame.Extensions.Sprites.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF.UI.MonoGameCore.Engines.Interfaces;
using WPF.UI.MonoGameCore.Modules;

namespace WPF.UI.MonoGameCore.Ships
{
    internal class SpaceShip : RigidBodySprite, ISelectable
    {
        private float m_GlobalScale;

        private Vector2 m_destination;

        private bool m_moving;

        private List<IModule> m_Modules;

        private List<IRigidBodyObject> m_modules;

        private bool m_selected;
        
        public bool Selected => m_selected;

        public override Vector2 CenterOfMass =>
            RigidBodyPhysics.GetCenterOfMass(m_modules);
        
        public SpaceShip(string name, 
            ContentManager contentManager, 
            SpriteBatch spriteBatch, 
            float mass,
            List<IEngine> engines, 
            IMOICalculator calculator,
            ITransformable? transform = default,
            IRigidBodyPhysics? rigidBodyPhysics = default,
            IAssetStorage? assetStorage = default,
            float GlobScale = 1f) 
            : base(name, 
                  contentManager, 
                  spriteBatch,
                  mass,
                  transform,
                  rigidBodyPhysics,
                  calculator,
                  assetStorage
                  )
        {    
            m_GlobalScale = GlobScale;

            m_Modules = engines;

            m_modules = new List<IRigidBodyObject>();

            m_modules.Add(this);

            foreach (var engine in m_Modules)
            {
                Mass += engine.Mass;
            }

            m_selected = false;  
            
            m_moving = false;            
        }
        
        public override void Load()
        {
            base.Load();

            var t = ContentManager.Load<Texture2D>("Assets/SpaceShips/Faction10/destroyer256");

            Transform.TextureSize = new SizeF() { Width = t.Width, Height = t.Height };

            Storage.AddAsset("destroyer", "Assets/SpaceShips/Faction10/destroyer256", t);            
        }

        public override void UnLoad()
        {
            base.UnLoad();                 
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {            
            base.Update(args, time, ref play);

            var main_engine = m_Modules.First(x => x.Id == 1);
            var acc2 = m_Modules.First(x => x.Id == 2);
            var acc3 = m_Modules.First(x => x.Id == 3);

            if (Transform.Position != m_destination && m_destination != Vector2.Zero)
            {                                
                //Activate Main engine:
                main_engine.Start();

                main_engine.Increase(time);

                //1 Get Direction Vector
                var dir = m_destination - Transform.Position; 
                var norm_dir = dir.NormalizedCopy();

                //Calculate angle between norm_dir and i local basis Vector

                var angle = MathF.Atan2(norm_dir.Y, norm_dir.X);

                var angleDegree = angle * (180 / MathF.PI);

                var main_engine_thrust = Transform.LocalBasis[0] * main_engine.CurrentThrust;
               
                var acc2_thrust = Transform.LocalBasis[1] * -1 * acc2.CurrentThrust;

                var acc3_thrust = Transform.LocalBasis[1] * acc3.CurrentThrust;

                var forces_sum = main_engine_thrust + acc2_thrust + acc3_thrust;

                //Regulate Additional Accelerators

                float current_Sum_force_Angle = MathF.Atan2(forces_sum.Y, forces_sum.X);

                //Calculate the moment of Intertia of the ship according to it's Center of Mass



                if (current_Sum_force_Angle > angle)
                {
                    //Use Accelerator2
                    acc2.Start();
                    acc3.Stop();
                    acc2.Increase(time);
                }
                else if (current_Sum_force_Angle < angle)
                {
                    //Use Accelarator3
                    acc2.Stop();
                    acc3.Start();
                    acc3.Increase(time);
                }
                else
                {
                    acc2.Stop();
                    acc3.Stop();
                }

                Vector2 displacement = (RigidBodyPhysics.GetDisplacement(
                    forces_sum,
                    Mass,
                    time) * m_GlobalScale);

                Transform.Position += displacement;
            }

            if (Transform.Position == m_destination)//Stop all engines
            { 
                main_engine.Stop();
                acc2.Stop();
                acc3.Stop();            
            }
            
        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);
            
            var t = (Texture2D)Storage["destroyer"];

            SpriteBatch.Draw(t, Transform.Position, null, Color.White, 
                Transform.Rotation*(MathF.PI/180),
                origin: CenterOfMass, 
                Transform.Scale, SpriteEffects.None, 0f );
            
            //Draw The Selection
            if (m_selected)
            {
                SpriteBatch.DrawRectangle(
                    new RectangleF(Transform.UpperLeftCorner, 
                    new SizeF()
                    {
                        Width = Transform.ActualSize.Width,
                        Height = Transform.ActualSize.Height
                    }),
                    Color.Green,
                    2f
                    );
                
                SpriteBatch.DrawPoint(Transform.Position, Color.Green, 5f);
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

        public void Move(Vector2 destination)
        { 
            m_destination = destination;
        }        
    }
}

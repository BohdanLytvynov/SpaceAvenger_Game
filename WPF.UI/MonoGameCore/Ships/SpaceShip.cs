using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Selectables;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.Behaviors.Transformables.RelativeTransform;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Physics.Interfaces;
using MonoGame.Extensions.Sprites.Interfaces;
using MonoGame.Extensions.Sprites.Realization;
using System;
using System.Collections.Generic;
using WPF.UI.Enums.ModuleTypes;
using WPF.UI.MonoGameCore.Engines.Interfaces;
using WPF.UI.MonoGameCore.Modules;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WPF.UI.MonoGameCore.Ships
{
    internal class SpaceShip : RigidBodySprite, ISelectable
    {
        private float m_GlobalScale;

        private Vector2 m_destination;

        private bool m_moving;
        
        private List<IRigidBodyObject> m_modules;

        private bool m_selected;
        
        public bool Selected => m_selected;

        public override Vector2 CenterOfMass =>
            Transform.Origin;

        #region Engines References
        
        private Dictionary<string, IEngine> m_engines;

        #endregion

        public SpaceShip(string name,
            ContentManager contentManager,
            SpriteBatch spriteBatch,
            bool debug,
            float mass,
            List<IRigidBodyObject> modules,
            IMOICalculator calculator,
            ILoadAssetStrategy? loadAssetStrategy = default,            
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
                  assetStorage,
                  loadAssetStrategy,
                  debug)
        {    
            m_GlobalScale = GlobScale;

            m_modules = modules;           

            m_modules.Add(this);

            m_engines = new Dictionary<string, IEngine>();

            //Add Engines to the List
            foreach (var item in m_modules)
            {
                if((item is IModule m) && m.Type == ModuleType.Engine)
                {
                    var e = (IEngine)item;

                    m_engines.Add((item as IGameObject)!.Name, e);
                }
            }

            foreach (var m in m_modules)
            {
                Mass += m.Mass;
               
                if(m.Transform is IRelativeTransform rt)
                    rt.ParentTransform = this.Transform;
            }

            m_selected = false;  
            
            m_moving = false;            
        }
        
        public override void Load()
        {
            base.Load();

            foreach (var m in m_modules)
            {
                var go = (IGameObject)m;

                if (!Object.ReferenceEquals(go, this))
                    go.Load();
            }
        }

        public override void UnLoad()
        {
            base.UnLoad();                 
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {            
            base.Update(args, time, ref play);

            //Orientation of the modules properly  

            foreach (var m in m_modules)
            {
                var sprite = (ISprite)m;

                if (!Object.ReferenceEquals(sprite, this))
                {
                    (sprite.Transform as IRelativeTransform)!.UpdateParentData(Transform);

                    (sprite as IGameObject)!.Update(args, time, ref play);
                }
            }

            //Use another Way to find engines module
            
            //if (Transform.Position != m_destination && m_destination != Vector2.Zero)
            //{                                
            //    //Activate Main engine:
            //    //main_engine.Start();
                
            //    //1 Get Direction Vector
            //    var dir = m_destination - Transform.Position; 
            //    var norm_dir = dir.NormalizedCopy();

            //    //Calculate angle between norm_dir and i local basis Vector

            //    var angle = MathF.Atan2(norm_dir.Y, norm_dir.X);

            //    var angleDegree = angle * (180 / MathF.PI);

            //    //var main_engine_thrust = Transform.LocalBasis[0] * main_engine.CurrentThrust;

            //    //var acc2_thrust = Transform.LocalBasis[1] * -1 * acc2.CurrentThrust;

            //    //var acc3_thrust = Transform.LocalBasis[1] * acc3.CurrentThrust;

            //    Vector2 forces_sum = Vector2.Zero; //main_engine_thrust + acc2_thrust + acc3_thrust;

            //    //Regulate Additional Accelerators

            //    float current_Sum_force_Angle = MathF.Atan2(forces_sum.Y, forces_sum.X);

            //    //Calculate the moment of Intertia of the ship according to it's Center of Mass

            //    if (current_Sum_force_Angle > angle)
            //    {
            //        //Use Accelerator2
            //        //acc2.Start();
            //        //acc3.Stop();                   
            //    }
            //    else if (current_Sum_force_Angle < angle)
            //    {
            //        //Use Accelarator3
            //        //acc2.Stop();
            //        //acc3.Start();                    
            //    }
            //    else
            //    {
            //        //acc2.Stop();
            //        //acc3.Stop();
            //    }

            //    Vector2 displacement = (RigidBodyPhysics.GetDisplacement(
            //        forces_sum,
            //        Mass,
            //        time) * m_GlobalScale);

            //    Transform.Position += displacement;
            //}
                        
        }

        public override void Draw(GameTime time, ref bool play)
        {
            base.Draw(time, ref play);
            
            var t = (Texture2D)Storage["destroyer"];

            SpriteBatch.Draw(t, Transform.Position, null, Color.White, 
                Transform.Rotation,
                origin: CenterOfMass, 
                Transform.Scale, SpriteEffects.None, 0f );

            //Draw Modules

            foreach (var m in m_modules)
            {
                var go = (IGameObject)m;

                if (!Object.ReferenceEquals(go, this))
                    go.Draw(time, ref play);
            }

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

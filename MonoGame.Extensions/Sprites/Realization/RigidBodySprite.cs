using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using MonoGame.Extensions.Physics.Interfaces;
using MonoGame.Extensions.Physics.Realizations;
using MonoGame.Extensions.Sprites.Interfaces;
using System;

namespace MonoGame.Extensions.Sprites.Realization
{
    public abstract class RigidBodySprite : Sprite, IRigidBodySprite, IRigidBodyObject
    {
        #region Fields
        private IRigidBodyPhysics m_rigidBodyPhysics;
        #endregion

        #region Properties
        public float Mass { get; set; }

        public abstract Vector2 CenterOfMass { get; }

        public IRigidBodyPhysics RigidBodyPhysics => m_rigidBodyPhysics;

        public float MOI_CM_DISTANCE => m_MOI_CM_distance;

        public float MOI_CM => m_MOI_CM;

        private float m_MOI_CM_distance;

        private float m_MOI_CM;
        #endregion

        #region Ctor

        public RigidBodySprite(string name, 
            ContentManager contentManager, 
            SpriteBatch spriteBatch, 
            float mass, 
            ITransformable? transform,
            IRigidBodyPhysics? rigidBodyPhysics,
            IMOICalculator MOICalculator,
            IAssetStorage? assetStorage,
            ILoadAssetStrategy loadAssetStrategy) : 
            base(name, 
                contentManager, 
                spriteBatch, 
                transform, 
                assetStorage,
                loadAssetStrategy)
        {
            if(mass <= 0)
                throw new ArgumentOutOfRangeException("Mass should be greater then 0!");

            Mass = mass;

            if (rigidBodyPhysics is null)
                m_rigidBodyPhysics = new RigidBodyPhysics();
            else
                m_rigidBodyPhysics = rigidBodyPhysics;

            m_MOI_CM_distance = Transform.Position.X - Transform.UpperLeftCorner.X;

            m_MOI_CM = m_rigidBodyPhysics.GetMOI(MOICalculator) + Mass * MathF.Pow(m_MOI_CM_distance,2);
        }
        
        #endregion

    }
}

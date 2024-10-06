using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extensions.Behaviors.Transformables;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame.Extensions.Behaviors
{
    public class Transform : ITransformable
    {
        #region Fields

        private Vector2 m_position;

        private float m_rotation;//must be in radians

        private Vector2 m_scale;

        private SizeF m_TextureSize;

        private Vector2 m_geomCenterOffset;       
       
        #endregion

        #region Properties
        //Used by all the Draw Functions
        public Vector2 Position
        {
            get => m_position;           

            set => m_position = value;
        }
        //Used by all the Draw Functions
        public float Rotation
        {
            get => m_rotation;
            set => m_rotation = value;
        }
        //Used by all the Draw Functions
        public Vector2 Scale
        {
            get => m_scale;
            set => m_scale = value;
        }

        public SizeF TextureSize 
        { 
            get => m_TextureSize; 
            set => m_TextureSize = value; 
        }

        public SizeF ActualSize 
        {
            get => new SizeF() 
            {
                Width = TextureSize.Width * Scale.X,
                Height = TextureSize.Height * Scale.Y
            };
        }

        public Vector2 GeometryCenterOffset 
        { 
            get => m_geomCenterOffset;
            set
            {
                if (value.X < 0 || value.Y < 0 || value.X > 1 || value.Y > 1)
                    throw new ArgumentOutOfRangeException
                        ("The GeometryCenterOffset parametr must be in range (0 - 1)");
                
                m_geomCenterOffset = value;
            }   
        }

        public Vector2 Origin => new Vector2(
            x: TextureSize.Width * GeometryCenterOffset.X,
            y: TextureSize.Height * GeometryCenterOffset.Y);

        public Vector2 UpperLeftCorner => Position - ActualSize * GeometryCenterOffset;

        public List<Vector2> LocalBasis { get; }

        public List<Vector2> GlobalBasis { get; }
       
        #endregion

        #region ctor
        /// <summary>
        /// Main Ctor
        /// </summary>
        /// <param name="position">Absolute position</param>
        /// <param name="rotation_Rad">Absolute Rotation in Radians</param>
        /// <param name="scale">Scale</param>
        /// <param name="geomCenterOffset">Position of the Origin of the Sprite(range from 0 to 1)</param>
        /// <param name="globalBasis">Global Basis of the Screen</param>
        public Transform(Vector2 position, float rotation_Rad, Vector2 scale, 
            Vector2 geomCenterOffset, List<Vector2> globalBasis)
        {
            m_scale = scale;
            m_position = position;
            
            GeometryCenterOffset = geomCenterOffset;

            if(globalBasis is not null)
                GlobalBasis = globalBasis;
            else
                GlobalBasis = new List<Vector2>() { new Vector2(1f,0f), new Vector2(0f,1f) };

            LocalBasis = new List<Vector2>();

            foreach (var basis in GlobalBasis)
            {
                LocalBasis.Add(new Vector2(basis.X, basis.Y));
            }

            Rotate(rotation_Rad);
        }
        /// <summary>
        /// Default Ctor
        /// </summary>
        public Transform() 
            : this(Vector2.Zero, 0f, new Vector2(1,1), new Vector2(0.5f,0.5f), null
                  )
        {
            
        }

        public Transform(Vector2 position, float rotation_Rad, Vector2 scale,
            Vector2 geomCenterOffset)
            :this(position, rotation_Rad, scale, geomCenterOffset, null)
        {
            
        }

        /// <summary>
        /// Ctor for Position, Rotation, Scale
        /// </summary>
        /// <param name="position">Absolute position</param>
        /// <param name="rotation_Rad">Absolute Rotation in Radians</param>
        /// <param name="scale">Scale</param>
        public Transform(Vector2 position, float rotation_Rad, Vector2 scale)
            : this(position, rotation_Rad, scale, new Vector2(0.5f,0.5f), null
                  )
        {
            
        }

        #endregion

        #region Functions
               
        public void Rotate(float radians)
        {            
            m_rotation = radians;
           
            for (int i = 0; i < LocalBasis.Count; i++)
            {
                LocalBasis[i] = LocalBasis[i].Rotate(radians);
            }            
        }
       
        #endregion

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Animations.Interfaces.AnimationFrames;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using MonoGame.Extensions.Behaviors.Transformables;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame.Extensions.Animations.Realizations.Animations
{
    public class Animation : IAnimation
    {       
        #region Fields

        private double m_start_global_time;
        private double m_current_local_time;
        private double m_acum;
        
        private int m_Rows;
        private int m_Columns;

        private int m_current_row;
        private int m_current_column;

        private int m_curr_frame_index;

        private bool m_start;

        private bool m_glob_start_time_set;

        private bool m_reverse;

        private Color m_BlendColor;

        private List<IAnimationFrame> m_frames;

        private double m_animSpeed;

        private bool m_IsLooping;

        private Texture2D m_Texture;
                
        #endregion

        #region Properties

        public bool Reverse { get => m_reverse; }

        public Color BlendColor { get=> m_BlendColor; set => m_BlendColor = value; }
        
        //Amount of rows in a sprite Sheet
        public int Rows { get => m_Rows;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("The amount of Rows can't be negative or Zero!");
                else
                    m_Rows = value;
            } 
        }
        //Amount of columns in the Sprite Sheet
        public int Columns { get => m_Columns;
            set
            { 
                if (value <= 0)
                    throw new ArgumentException("The amount of Columns can't be negative or Zero!");
                else
                    m_Columns = value;
            }
        }
        //Count of frames
        public int FrameCount 
        {
            get => Columns * Rows;
        }
        //Height of the frame
        public int FrameHeight 
        { 
            get => Texture.Height / Rows; 
        }

        //Animation Speed (R) 
        public double AnimationSpeed { get => m_animSpeed; set => m_animSpeed = value; }

        //Width of the frame
        public int FrameWidth { get => Texture.Width / Columns; } 

        //Wether repeating is required
        public bool IsLooping { get => m_IsLooping; set => m_IsLooping = value; }
        //Texture that represents sprite sheet
        public Texture2D Texture { get=> m_Texture; }

        #endregion

        #region Ctor

        public Animation(
            Texture2D texture,
            int rows_on_texture,
            int columns_on_texture,
            bool isLooping,
            double animationSpeed,
            List<IAnimationFrame> frames,
            Color blendColor,
            bool reverse = false)
        {
            m_Texture = texture;

            Rows = rows_on_texture;

            Columns = columns_on_texture;

            IsLooping = isLooping;

            AnimationSpeed = animationSpeed;

            m_frames = frames;

            m_reverse = reverse;

            BlendColor = blendColor;

            Reset(reverse);

            Stop();
        }

        #endregion

        #region Functions

        public void Start()
        {
            if (m_start)
                return;

            m_start = true;                       
        }

        public void Stop()
        {
            if (!m_start)
                return;

            m_start = false;
        }

        public void Reset(bool reverse)
        {
            m_glob_start_time_set = false;

            m_start_global_time = 0;

            m_current_local_time = 0f;

            m_curr_frame_index = 0;

            m_acum = 0f;

            if (!reverse)//Case of the direct animation
            {
                m_current_row = 0;

                m_current_column = 0;
            }
            else//Case of the Reverse Animation
            {
                m_current_row = Rows - 1;

                m_current_column = Columns - 1;
            }
        }

        public void Update(GameTime gameTime)
        {           
            if (!m_start)
                return;

            if (!m_glob_start_time_set)
            {
                m_start_global_time = gameTime.TotalGameTime.TotalMilliseconds;
                m_glob_start_time_set = true;
            }

            //1 Calculate local time
            m_current_local_time = (gameTime.TotalGameTime.TotalMilliseconds - m_start_global_time)
                * AnimationSpeed;

            //Debug.WriteLine($"CL: {m_current_local_time}");
            
            if (!Reverse)
                Direct(gameTime);
            else
                Inverse(gameTime);

        }

        private void Direct(GameTime gameTime)
        {
            if (IsLooping && m_curr_frame_index >= m_frames.Count - 1)
            {
                Stop();

                Reset(Reverse);

                Start();                                
            }
            else if (m_curr_frame_index >= m_frames.Count - 1)
            {
                Stop();                
            }
            
            if (m_current_local_time >= m_frames[m_curr_frame_index].Lifespan + m_acum)
            {
                m_acum += m_frames[m_curr_frame_index].Lifespan;
                ++m_current_column;
                ++m_curr_frame_index;
            }

            if (m_current_column >= Columns)//The last column -> need to switch to the next row
            {
                ++m_current_row;
                m_current_column = 0;
            }

            //Debug.WriteLine($"r: {m_current_row} c: {m_current_column}" +
            //    $"  Current LT: {m_current_local_time} Acum: {m_acum} CurrFrame: {m_curr_frame_index}");
        }
        //Need modification
        private void Inverse(GameTime gameTime)
        {
            if (IsLooping && m_current_row == 0 && m_current_column == 0)
            {
                Stop();

                Reset(Reverse);

                Start();
            }

            if (m_current_local_time >= m_frames[m_current_column].Lifespan + m_acum)
            {
                m_acum += m_frames[m_current_column].Lifespan;
                m_current_column--;
            }

            if (m_current_column <= 0)
            {
                m_current_row--;
                m_current_column = Columns - 1;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransformable transform,
            float layerDepth = 0f)
        {           
            spriteBatch.Draw(
                m_Texture,
                transform.Position,
                new Rectangle(
                    m_current_column * FrameWidth,
                    m_current_row * FrameHeight,
                    FrameWidth, FrameHeight),
                BlendColor, 
                transform.Rotation, 
                transform.Origin, 
                transform.Scale,
                SpriteEffects.None, layerDepth);
        }

        #endregion
    }
}

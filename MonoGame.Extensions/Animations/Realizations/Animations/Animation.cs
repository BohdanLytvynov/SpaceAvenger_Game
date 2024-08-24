using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Animations.Interfaces.AnimationFrames;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using MonoGame.Extensions.Animations.Realizations.AnimationFrames;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Animations.Realizations.Animations
{
    public class Animation : IAnimation
    {
        #region Fields
        
        private int m_start_global_time;
        private float m_current_local_time;
        private float m_acum;
        
        private int m_Rows;
        private int m_Columns;

        private int m_current_row;
        private int m_current_column;

        private bool m_start;

        private bool m_reverse;

        private Color m_BlendColor;

        private List<IAnimationFrame> m_frames;

        private float m_animSpeed;

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
        public float AnimationSpeed { get => m_animSpeed; set => m_animSpeed = value; }

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
            int rows,
            int columns,
            bool isLooping,
            float animationSpeed,
            List<IAnimationFrame> frames,
            Color blendColor,
            bool reverse = false)
        {
            m_Texture = texture;

            Rows = rows;

            Columns = columns;

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

        public void Start(GameTime gameTime)
        {
            if (m_start)
                return;

            m_start = true;

            m_start_global_time = gameTime.TotalGameTime.Seconds;            
        }

        public void Stop()
        {
            if (!m_start)
                return;

            m_start = false;
        }

        public void Reset(bool reverse)
        {
            m_current_local_time = 0f;

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

            //1 Calculate local time
            m_current_local_time = (((float)gameTime.TotalGameTime.Seconds) - m_start_global_time) * AnimationSpeed;

            if (!Reverse)
                Direct(gameTime);
            else
                Inverse(gameTime);

        }

        private void Direct(GameTime gameTime)
        {
            if (IsLooping && m_current_row >= Rows - 1 && m_current_column >= Columns - 1)
            {
                Stop();

                Reset(Reverse);

                Start(gameTime);
            }

            if (m_current_local_time >= m_frames[m_current_column].Lifespan + m_acum)
            {
                m_acum = m_frames[m_current_column].Lifespan;
                m_current_column++;
            }

            if (m_current_column >= Columns - 1)//The last column -> need to switch to the next row
                m_current_row++;
        }
        //Need modification
        private void Inverse(GameTime gameTime)
        {
            if (IsLooping && m_current_row == 0 && m_current_column == 0)
            {
                Stop();

                Reset(Reverse);

                Start(gameTime);
            }

            if (m_current_local_time >= m_frames[m_current_column].Lifespan + m_acum)
            {
                m_acum = m_frames[m_current_column].Lifespan;
                m_current_column--;
            }

            if (m_current_column <= 0)
            {
                m_current_row--;
                m_current_column = Columns - 1;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            if (!m_start)
                return;

            spriteBatch.Draw(
                m_Texture,
                position,
                new Rectangle(
                    m_current_column * FrameWidth,
                    m_current_row,
                    FrameWidth, FrameHeight),
                BlendColor);
        }

        #endregion
    }
}

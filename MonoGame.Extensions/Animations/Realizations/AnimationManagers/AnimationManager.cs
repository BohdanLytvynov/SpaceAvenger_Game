using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MonoGame.Extensions.Animations.Interfaces.AnimationManagers;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using MonoGame.Extensions.Behaviors.Transformables;

namespace MonoGame.Extensions.Animations.Realizations.AnimationManagers
{
    public class AnimationManager : IAnimationManager
    {
        private Dictionary<string, IAnimation> m_animation;

        private IAnimation m_current;

        private string m_current_animation_name;

        public string Current_Animation_Name { get => m_current_animation_name; }

        public IAnimation Current => m_current;

        public AnimationManager(Dictionary<string, IAnimation> animations = null)
        {
            if(animations is null)
                m_animation = new Dictionary<string, IAnimation>();
            else
                m_animation = animations;
        }

        public void AddAnimation(string name, IAnimation animation)
        { 
            m_animation.Add(name, animation);
        }

        public void RemoveAnimation(string name)
        { 
            m_animation.Remove(name);
        }

        public IAnimation? GetAnimation(string name)
        { 
            return m_animation[name];
        }

        public void SetAnimationForPlay(string animationName)
        {            
            m_current = m_animation[animationName];
            m_current_animation_name = animationName;
        }

        public void Start()
        {
            m_current.Start();
        }

        public void Stop()
        { 
            m_current.Stop();
        }

        public void Reset()
        { 
            m_current.Reset(m_current.Reverse);
        }

        public void Update(GameTime gameTime)
        { 
            m_current.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransformable transform
            ,float layerDepth)
        { 
            m_current.Draw(gameTime, spriteBatch, transform, layerDepth);   
        }
     
        public void SetAnimationForPlay(string animationName, bool resetPrevAnim)
        {
            if (resetPrevAnim)
            {
                m_current.Stop();
                m_current.Reset(m_current.Reverse);
                m_current = this[animationName];
                m_current.Start();
            }
            else
            {
                m_current.Stop();
                m_current = this[animationName];
                m_current.Start();
            }

            m_current_animation_name = animationName;
        }
       
        public IAnimation? this[string key]
        { 
            get => m_animation[key];
            set => m_animation[key] = value;
        }
    }
}

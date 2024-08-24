using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MonoGame.Extensions.Animations.Interfaces.AnimationManagers;
using MonoGame.Extensions.Animations.Interfaces.Animations;

namespace MonoGame.Extensions.Animations.Realizations.AnimationManagers
{
    public class AnimationManager : IAnimationManager
    {
        private Dictionary<string, IAnimation> m_animation;

        private IAnimation m_current;
        
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
        }

        public void Start(GameTime gameTime)
        {
            m_current.Start(gameTime);
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        { 
            m_current.Draw(gameTime, spriteBatch, position);   
        }

        public IAnimation? this[string key]
        { 
            get => m_animation[key];
            set => m_animation[key] = value;
        }
    }
}

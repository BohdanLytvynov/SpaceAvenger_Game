using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Animations.Interfaces.AnimationManagers
{
    public interface IAnimationManager
    {
        public void AddAnimation(string name, IAnimation animation);

        public void RemoveAnimation(string name);

        public IAnimation? GetAnimation(string name);

        public void SetAnimationForPlay(string animationName);

        public void Start(GameTime gameTime);

        public void Stop();

        public void Reset();

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position);        

        public IAnimation? this[string key]
        {
            get;
            set;
        }
    }
}

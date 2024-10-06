using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extensions.Animations.Interfaces.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extensions.Behaviors.Transformables;

namespace MonoGame.Extensions.Animations.Interfaces.AnimationManagers
{
    public interface IAnimationManager
    {
        public string Current_Animation_Name { get; }

        public IAnimation Current { get; }

        void AddAnimation(string name, IAnimation animation);

        void RemoveAnimation(string name);

        IAnimation? GetAnimation(string name);
        
        void SetAnimationForPlay(string animationName);
        
        void SetAnimationForPlay(string animationName, bool resetPrevAnim);

        void Start();

        void Stop();

        void Reset();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch, ITransformable transform,
            float layerDepth);        

        public IAnimation? this[string key]
        {
            get;
            set;
        }
    }
}

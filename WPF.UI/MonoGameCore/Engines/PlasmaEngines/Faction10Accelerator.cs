using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extensions.AssetStorages.Interface;
using MonoGame.Extensions.Behaviors.Transformables;
using MonoGame.Extensions.Behaviors.Transformables.RelativeTransform;
using MonoGame.Extensions.GameObjects.Base;
using MonoGame.Extensions.GameObjects.LoadAssetsStrategy;
using System;
using WPF.UI.MonoGameCore.Engines.Realizations;

namespace WPF.UI.MonoGameCore.Engines.PlasmaEngines
{
    internal class Faction10Accelerator : EngineBase
    {
        public Faction10Accelerator(bool debug, 
            string name, 
            ContentManager contentManager, 
            SpriteBatch spriteBatch, 
            float maxThrust, float mass, 
            ITransformable transform, 
            ILoadAssetStrategy? loadAssetStrategy = default,              
            IAssetStorage? assetStorage = default,
            Func<GameTime, float>? IncreaseCalcFunction = default) : 
            base(debug, 
                name, 
                contentManager, 
                spriteBatch, 
                maxThrust, 
                mass, 
                transform,
                IncreaseCalcFunction,
                loadAssetStrategy,                  
                assetStorage)
        {
        }

        public override void Load()
        {
            var t = Storage.LoadAssetAndGet<Texture2D>("Faction10-Accelerator", "Assets/Factions/Faction10/Engines/ManuverEngine_F10");
            
            if (t != null)
            {
                this.Transform.TextureSize = new SizeF() { Width = t.Width, Height = t.Height };
            }

            base.Load();
        }

        public override void Update(IUpdateArgs args, GameTime time, ref bool play)
        {
            base.Update(args, time, ref play);

            var relTransform = (this.Transform as IRelativeTransform)!;

            var RelPosition = relTransform.RelativePosition;

            Vector2 relPos_Scaled = new Vector2()
            {
                X = RelPosition.X * Transform.Scale.X,
                Y = RelPosition.Y * Transform.Scale.Y
            };
            


            this.Transform.Position = relPos_Scaled + relTransform.ParentPosition;
        }

        public override void Draw(GameTime time, ref bool play)
        {
            var t = (Texture2D)Storage["Faction10-Accelerator"];

            SpriteBatch.Draw(t, Transform.Position, null, Color.White,
                Transform.Rotation * (MathF.PI / 180),
                origin: Transform.Origin,
                Transform.Scale, SpriteEffects.None, 0.9f);

            base.Draw(time, ref play);
        }
    }
}

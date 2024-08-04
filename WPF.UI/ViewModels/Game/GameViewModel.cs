using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.UI.Attributes.PageManager;
using WPF.UI.MonoGameControls;

namespace WPF.UI.ViewModels.Game
{
    [ReflexionDetectionIgnore]
    internal class GameViewModel : MonoGameViewModel
    {        
        #region Game 

        private SpriteBatch _spriteBatch = default!;
        
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);  
            
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            base.Draw(gameTime);
        }

        #endregion
    }
}

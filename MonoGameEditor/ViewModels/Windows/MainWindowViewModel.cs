using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameEditor.MonoGameControls;

namespace MonoGameEditor.ViewModels.Windows;

public class MainWindowViewModel : MonoGameViewModel
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Ctor

    public MainWindowViewModel()
    {

    }

    #endregion

    #region MonoGame Engine

    private SpriteBatch _spriteBatch = default!;

    public override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }
    
    public override void Update(GameTime gameTime)
    {

    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _spriteBatch.End();
    }

    public override void UnloadContent()
    {


        base.UnloadContent();
    }

    #endregion


}

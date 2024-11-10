using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameEditor.MonoGameControls;

namespace MonoGameEditor.ViewModels.Windows;

public class MainWindowViewModel : MonoGameViewModel
{
    #region Fields
    private string m_Title;
    #endregion

    #region Properties
    public string Title { get => m_Title; set => Set(ref m_Title, value); }
    #endregion

    #region Ctor

    public MainWindowViewModel()
    {
        m_Title = "MonoGame Editor";
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

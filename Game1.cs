using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Engine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public List<Sprite> sprites = new List<Sprite>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        AddSprites();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        LoadSprites();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        UpdateSprites();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        DrawSprites();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void AddSprites()
    {
        sprites.Add(new Player(new Vector2(1, 1)));
    }

    private void LoadSprites()
    {
        foreach (Sprite sprite in sprites)
        {
            sprite.Initialize();
            sprite.Load(Content);
        }
    }

    private void UpdateSprites()
    {
        foreach (Sprite sprite in sprites)
            sprite.Update();
    }

    private void DrawSprites() 
    {
        foreach (Sprite sprite in sprites)
        {
            sprite.Draw(_spriteBatch);
        }
    }
}

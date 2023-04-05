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
    public Map map = new Map();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Resolution.Init(ref _graphics);
        Resolution.SetVirtualResolution(1280, 720);
        Resolution.SetResolution(1280, 720, false);
    }

    protected override void Initialize()
    {
        Camera.Initialize();
        AddSprites();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        map.Load(Content);
        LoadLevel();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        UpdateCamera();
        map.Update(sprites);
        UpdateSprites();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, 
        DepthStencilState.None, RasterizerState.CullNone, null, Camera.GetTransformMatrix());
        map.DrawWalls(_spriteBatch);
        DrawSprites();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void LoadLevel()
    {
        AddSprites();
        map.LoadMap(Content);
        // map.GenerateBorders();

        map.walls.Add(new Wall(Content.Load<Texture2D>("Placeholders/defaultTile"), new Rectangle(0, 0, 64, 64)));
        
        LoadSprites();
        
    }

    private void AddSprites()
    {
        sprites.Add(new Player(new Vector2(300, 300)));
        
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
            sprite.Update(sprites, map);
    }

    private void DrawSprites() 
    {
        foreach (Sprite sprite in sprites)
        {
            sprite.Draw(_spriteBatch);
        }
    }

    private void UpdateCamera()
    {
      if (sprites.Count == 0)
        return;

      Camera.Update(sprites[0].position);
    }
}

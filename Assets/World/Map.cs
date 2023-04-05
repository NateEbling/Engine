using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Engine
{
    public class Map
    {
        public List<Decor> decor = new List<Decor>();
        public List<Wall> walls = new List<Wall>();

        public int tileSize = 64; // Tile size in pixels
        public const int mapWidth = 10;
        public const int mapHeight = 5; // Number of tiles
        public int[,] mapMatrix = new int[mapWidth, mapHeight];

        private Color drawColor = Color.White;
        private Texture2D defaultBorder, defaultTile, test;

        public void LoadMap(ContentManager content)
        {
            foreach (Decor dec in decor)
                dec.Load(content, dec.imagePath);
        }

        public void Load(ContentManager content)
        {
            defaultTile = content.Load<Texture2D>("Placeholders/defaultTile");
            defaultBorder = content.Load<Texture2D>("Placeholders/defaultBorder");
            test = content.Load<Texture2D>("test");
        }

        public Rectangle CheckCollisions(Rectangle input)
        {
            foreach (Wall wall in walls)
            {
                if (wall != null && wall.wall.Intersects(input) == true)
                    return wall.wall;
            }

            return Rectangle.Empty;
        } 

        public void Update(List<Sprite> sprites)
        {
            foreach (Sprite sprite in sprites)
                sprite.Update(sprites, this);
        }

        public void DrawWalls(SpriteBatch spriteBatch)
        {
            foreach (Wall wall in walls)
            {
                if (wall._texture != null && wall.active == true)
                    spriteBatch.Draw(wall._texture, new Vector2(wall.wall.X, wall.wall.Y), wall.wall, drawColor, 0f, 
                    Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                else spriteBatch.Draw(defaultTile, new Vector2(wall.wall.X, wall.wall.Y), wall.wall, drawColor, 0f,
                Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
            }
        }

        public void AddFloor(int num, int x, int y)
        {
            for (int i = 0; i < num; i++)
            {
                this.walls.Add(new Wall(defaultBorder, new Rectangle(x + (i * 64), y, 64, 64)));
            }
        }

        public Point GetTileIndex(Vector2 inputPosition)
        {
            if (inputPosition == new Vector2(-1, -1))
                return new Point(-1, -1);

            return new Point((int)inputPosition.X / tileSize, (int)inputPosition.Y / tileSize);
        }
    }
}
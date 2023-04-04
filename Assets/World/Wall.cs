using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Engine
{
    public class Wall
    {
        public Rectangle wall;
        public bool active = true;
        public Texture2D texture;

        public Wall() {}

        public Wall(Texture2D inputTexture, Rectangle inputRectangle)
        {
            texture = inputTexture;
            wall = inputRectangle;
        }

        public Wall(Rectangle inputRectangle)
        {
            wall = inputRectangle; 
        }
    }
}
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine
{
    public class Sprite
    {
        protected Texture2D texture;
        public bool isActive = true, collidable = true;
        public Vector2 position, startPosition = new Vector2(-1, -1), direction = new Vector2(1, 0);
        public float scale = 1f, rotation = 0f, layerDepth = 0.5f;
        public Color drawColor = Color.White;

        protected int boundingBoxWidth, boundingBoxHeight;
        protected Vector2 boundingBoxOffset, center;
        private Texture2D boundingBoxTexture;
        const bool drawBoundingBoxes = true;

        public Sprite() {}

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(position.X + boundingBoxOffset.X), (int)(position.Y + boundingBoxOffset.Y),
                                     boundingBoxWidth, boundingBoxHeight);
            }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public virtual bool CheckCollisions(Rectangle input)
        {
            return BoundingBox.Intersects(input);
        }

        public virtual void Initialize()
        {
            if (position == new Vector2(-1, -1))
                startPosition = position;
        }

        public virtual void Load(ContentManager content)
        {
            boundingBoxTexture = content.Load<Texture2D>("boundingBox");

            CalculateCenter();

            if (texture != null)
            {
                boundingBoxWidth = texture.Width;
                boundingBoxHeight = texture.Height;
            }
        }

        public virtual void Update(List<Sprite> sprites, Map map)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            if (boundingBoxTexture != null && drawBoundingBoxes == true && isActive == true)
            {
                spriteBatch.Draw(boundingBoxTexture, new Vector2(BoundingBox.X, BoundingBox.Y), BoundingBox,
                                 new Color(120, 120, 120, 120), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
            }
            if (isActive == true)
            {
                spriteBatch.Draw(texture, position, null, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, 
                                 layerDepth);
            }
        }

        private void CalculateCenter()
        {
            if (texture == null)
                return;
    
            center.X = texture.Width / 2;
            center.Y = texture.Height / 2;
            
        }
    }
}
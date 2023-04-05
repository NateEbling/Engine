using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Engine
{
    public class Sprite
    {
        protected Texture2D _texture;
        public Vector2 _position;
        public Color drawColor = Color.White;
        public float scale = 1f, rotation = 0f;
        public float layerDepth = 0.5f;
        public bool isActive = true;
        protected Vector2 center;
        public bool collidable = true;
        protected int boundingBoxWidth, boundingBoxHeight;
        protected Vector2 boundingBoxOffset;
        protected int boundingBoxWidthTrim, boundingBoxHeightTrim;
        protected int boundingBoxWidthTrimOffset, boundingBoxHeightTrimOffset;
        protected int boundingBoxBottomTrim;
        Texture2D boundingBoxImage;
        const bool drawBoundingBoxes = true;
        protected Vector2 direction = new Vector2(1, 0); // right
        public Vector2 startPosition = new Vector2(-1, -1);

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(_position.X + boundingBoxOffset.X + boundingBoxWidthTrim), 
                (int)(_position.Y + boundingBoxOffset.Y + boundingBoxHeightTrim), 
                boundingBoxWidth - boundingBoxWidthTrim * 2, 
                boundingBoxHeight - boundingBoxHeightTrim * 2 - boundingBoxBottomTrim);
            }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height); }
        }

        public Sprite()
        {

        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Initialize()
        {
            if (startPosition == new Vector2(-1, -1))
                startPosition = _position;
        }

        public virtual void SetToDefaultPosition()
        {
            _position = startPosition;
        }

        public virtual void Load(ContentManager content)
        {
            boundingBoxImage = content.Load<Texture2D>("boundingBox");

            CalculateCenter();

            if (_texture != null)
            {
                boundingBoxWidth = _texture.Width;
                boundingBoxHeight = _texture.Height;
            }
        }

        public virtual void Update(List<Sprite> sprites, Map map)
        {

        }

        public virtual bool CheckCollisions(Rectangle input)
        {
            return BoundingBox.Intersects(input);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (boundingBoxImage != null && drawBoundingBoxes == true && isActive == true)
                spriteBatch.Draw(boundingBoxImage, new Vector2(BoundingBox.X, BoundingBox.Y), BoundingBox, new Color(120, 120, 120, 120),
                  0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            if (_texture != null && isActive == true)
                spriteBatch.Draw(_texture, _position, null, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }

        private void CalculateCenter()
        {
            if (_texture == null)
                return;

            center.X = _texture.Width / 2;
            center.Y = _texture.Height / 2;
        }
    }
}
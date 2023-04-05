using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class Decor : Sprite
    {
        public string imagePath;
        public Rectangle sourceRect;
        public string Name { get {return imagePath; }}

        public Decor()
        {
            collidable = true;
        }

        public Decor(Vector2 inputPosition, string inputImagePath, float inputDepth, bool collide)
        {
            _position = inputPosition;
            imagePath = inputImagePath;
            layerDepth = inputDepth;
            collidable = collide;
        }

        public Decor(Vector2 inputPosition, string inputImagePath, float inputDepth)
        {
            _position = inputPosition;
            imagePath = inputImagePath;
            layerDepth = inputDepth;
            isActive = true;
            collidable = true;
        }

        public virtual void Load(ContentManager content, string asset)
        {
            _texture = TextureLoader.Load(asset, content);
            _texture.Name = asset;

            boundingBoxWidth = _texture.Width;
            boundingBoxHeight = _texture.Height;
            
            if (sourceRect == Rectangle.Empty)
            {
                sourceRect = new Rectangle(0, 0, _texture.Width, _texture.Height);
            }
        }

        public void SetImage(Texture2D input, string newPath)
        {
            _texture = input;
            imagePath = newPath;
            boundingBoxWidth = sourceRect.Width = _texture.Width;
            boundingBoxHeight = sourceRect.Height = _texture.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null && isActive == true)
            {
                spriteBatch.Draw(_texture, _position, sourceRect, drawColor, rotation, Vector2.Zero, scale, 
                SpriteEffects.None, layerDepth);
            }
            base.Draw(spriteBatch);
        }
    }
}
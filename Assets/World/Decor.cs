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
            position = inputPosition;
            imagePath = inputImagePath;
            layerDepth = inputDepth;
            collidable = collide;
        }

        public Decor(Vector2 inputPosition, string inputImagePath, float inputDepth)
        {
            position = inputPosition;
            imagePath = inputImagePath;
            layerDepth = inputDepth;
            isActive = true;
            collidable = true;
        }

        public virtual void Load(ContentManager content, string asset)
        {
            texture = TextureLoader.Load(asset, content);
            texture.Name = asset;

            boundingBoxWidth = texture.Width;
            boundingBoxHeight = texture.Height;
            
            if (sourceRect == Rectangle.Empty)
            {
                sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
            }
        }

        public void SetImage(Texture2D input, string newPath)
        {
            texture = input;
            imagePath = newPath;
            boundingBoxWidth = sourceRect.Width = texture.Width;
            boundingBoxHeight = sourceRect.Height = texture.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null && isActive == true)
            {
                spriteBatch.Draw(texture, position, sourceRect, drawColor, rotation, Vector2.Zero, scale, 
                SpriteEffects.None, layerDepth);
            }
            base.Draw(spriteBatch);
        }
    }
}
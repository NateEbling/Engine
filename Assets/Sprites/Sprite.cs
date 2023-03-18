using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Sprite
    {
        protected Texture2D texture;

        public bool isActive = true;
        public Vector2 position, startPosition = new Vector2(-1, -1), direction = new Vector2(1, 0); // Default dir right
        public float scale = 1f, rotation = 0f, layerDepth = 0.5f;
        public Color drawColor = Color.White;

        public Sprite() {}

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Initialize()
        {
            if (position == new Vector2(-1, -1))
                startPosition = position;
        }

        public virtual void Load(ContentManager content)
        {
            
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            if (isActive == true)
            {
                spriteBatch.Draw(texture, position, null, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, 
                                 layerDepth);
            }
        }


    }
}
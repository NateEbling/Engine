using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Engine
{
    public class Player : Character
    {
        public Player()
        {

        }

        public Player(Vector2 inputPosition)
        {
            _position = inputPosition;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprites/Square");

            base.Load(content);
        }

        public override void Update(List<Sprite> sprites, Map map)
        {
            CheckInput(sprites, map);
            base.Update(sprites, map);
        }

        private void CheckInput(List<Sprite> sprites, Map map)
        {
            if (Character.applyGravity == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    MoveRight();

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    MoveLeft();

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    MoveDown();

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    MoveUp();
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    MoveRight();

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    MoveLeft();

                if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                    Jump(map);
            }
        }
    }
}
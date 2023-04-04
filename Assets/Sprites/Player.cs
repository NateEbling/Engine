using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public class Player : Sprite
    {
        public Vector2 velocity;
        protected float decel = 1.7f, accel = 0.8f, maxSpeed = 5f, gravity = 1f, jumpVelo = 16f, maxFallVelo = 32f;
        protected bool jumping;
        public static bool applyGravity = false;

        public Player() {}

        public Player(Vector2 inputPosition)
        {
            position = inputPosition;
        }

        public override void Initialize()
        {
            velocity = Vector2.Zero;
            jumping = false;

            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites/Square");

            base.Load(content);
        }

        public override void Update()
        {
            CheckInput();
            base.Update();
        }

        private void CheckInput()
        {
            if (applyGravity == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    MoveRight();
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    MoveLeft();
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    MoveUp();
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    MoveDown();
            }

            else if (applyGravity == true)
            {

            }

            UpdateMovement();
        }

        private void UpdateMovement()
        {   
            position.X += velocity.X;
            position.Y += velocity.Y;

            velocity.X = ReturnToZero(velocity.X, decel);
            velocity.Y = ReturnToZero(velocity.Y, decel);

            // if (applyGravity == true)
            //     ApplyGravity();
            // else 
            //     velocity.Y = ReturnToZero(velocity.X, decel);
        
        }

        private void ApplyGravity()
        {

        }

        protected void MoveRight()
        {
            velocity.X += accel + decel;

            if (velocity.X > maxSpeed)
                velocity.X = maxSpeed;

            direction.X = 1;
        }

        protected void MoveLeft()
        {
            velocity.X -= accel + decel;

            if (velocity.X < -maxSpeed)
                velocity.X = -maxSpeed;

            direction.X = -1;
        }

        protected void MoveDown()
        {
            velocity.Y += accel + decel;

            if (velocity.Y > maxSpeed)
                velocity.Y = maxSpeed;

            direction.Y = 1;
        }

        protected void MoveUp()
        {
            velocity.Y -= accel + decel;

            if (velocity.Y < -maxSpeed)
                velocity.Y = -maxSpeed;

            direction.Y = -1;
        }

        protected float ReturnToZero(float val, float amount)
        {
            if (val > 0f && (val -= amount) < 0f) return 0f;
            if (val < 0f && (val += amount) > 0f) return 0f;
            return val;
        }

        
    }
}
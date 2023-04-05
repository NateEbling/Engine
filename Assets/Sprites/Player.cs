using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Engine
{
    public class Player : Sprite
    {
        public Vector2 velocity = Vector2.Zero;
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

        public override void Update(List<Sprite> sprites, Map map)
        {
            UpdateMovement(sprites, map);
            base.Update(sprites, map);
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
        }

        private void UpdateMovement(List<Sprite> sprites, Map map)
        {   
            CheckInput();

            position.X += velocity.X;
            position.Y += velocity.Y;

            velocity.X = ReturnToZero(velocity.X, decel);
            velocity.Y = ReturnToZero(velocity.Y, decel);

            if (applyGravity == true)
                ApplyGravity(map);
            else 
                velocity.Y = ReturnToZero(velocity.X, decel);
        
        }

        // private void UpdateMovement(List<Sprite> sprites, Map map)
        // {
        //     CheckInput();

        //     if (velocity.X != 0 && CheckCollisions(map, sprites, true) == true)
        //         velocity.X = 0;

        //     position.X += velocity.X;

        //     if(velocity.Y != 0 && CheckCollisions(map, sprites, false) == true)
        //         velocity.Y = 0;

        //     position.Y += velocity.Y;

        //     if (applyGravity == true)
        //         ApplyGravity(map);

        //     velocity.X = ReturnToZero(velocity.X, decel);
        //     if (!applyGravity)
        //         velocity.Y = ReturnToZero(velocity.Y, decel);
        // }

        private void ApplyGravity(Map map)
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

        protected virtual bool CheckCollisions(Map map, List<Sprite> sprites, bool xAxis)
        {
            Rectangle futureBoundingBox = BoundingBox;

            int maxX = (int)maxSpeed;
            int maxY = (int)maxSpeed;

            if (applyGravity == true)
                maxY = (int)jumpVelo;

            if (xAxis == true && velocity.X != 0)
            {
                if (velocity.X > 0)
                futureBoundingBox.X += maxX;
                else
                futureBoundingBox.X -= maxX;
            }
            else if (!applyGravity && !xAxis && velocity.Y != 0)
            {
                if (velocity.Y > 0)
                futureBoundingBox.Y += maxY;
                else
                futureBoundingBox.Y -= maxY;
            }

            else if (applyGravity && !xAxis && velocity.Y != gravity)
            {
                if (velocity.Y > 0)
                futureBoundingBox.Y += maxY;
                else
                futureBoundingBox.Y -= maxY;
            }

            // Checking wall collision 
            Rectangle wallCollision = map.CheckCollisions(futureBoundingBox);

            if (wallCollision != Rectangle.Empty)
            {
                if (applyGravity == true && velocity.Y >= gravity && (futureBoundingBox.Bottom > wallCollision.Top - maxSpeed)
                && (futureBoundingBox.Bottom <= wallCollision.Top + velocity.Y))
                {
                LandResponse(wallCollision);
                return true;
                }
                else
                return true;
            }

            // Check for sprite collisions
            foreach (var sprite in sprites)
            {
                if (sprite != this && sprite.isActive == true && sprite.collidable == true && sprite.CheckCollisions(futureBoundingBox) == true)
                return true;
            }

            return false;
        }

        public void LandResponse(Rectangle wallCollision)
        {
            position.Y = wallCollision.Top - (boundingBoxHeight + boundingBoxOffset.Y);
            velocity.Y = 0;
            jumping = false;
        }

        protected Rectangle OnGround(Map map)
        {
            Rectangle futureHitBox = new Rectangle((int)(position.X + boundingBoxOffset.X), 
            (int)(position.Y + boundingBoxOffset.Y + (velocity.Y + gravity)), boundingBoxWidth, boundingBoxHeight);
            return map.CheckCollisions(futureHitBox);
        } 
    }
}
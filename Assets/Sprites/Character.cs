using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Character : Sprite
    {
        public Vector2 velocity;
        protected float decel = 1.7f;
        protected float accel = 0.8f;
        protected float maxSpeed = 6f;
        const float gravity = 1f;
        const float jumpVelocity = 20f;
        const float maxFallVelocity = 32f;
        protected bool jumping;
        public static bool applyGravity = true;

        public override void Initialize()
        {
            velocity = Vector2.Zero;
            jumping = false;
            base.Initialize();
        }

        public override void Update(List<Sprite> sprites, Map map)
        {
            UpdateMovement(sprites, map);
            base.Update(sprites, map);
        }

        private void UpdateMovement(List<Sprite> sprites, Map map)
        {
            if (velocity.X != 0 && CheckCollisions(map, sprites, true) == true)
                velocity.X = 0;

            _position.X += velocity.X;

            if (velocity.Y != 0 && CheckCollisions(map, sprites, false) == true)
                velocity.Y = 0;

            _position.Y += velocity.Y;

            if (applyGravity == true)
                ApplyGravity(map);

            velocity.X = TendToZero(velocity.X, decel);
            if (!applyGravity)
                velocity.Y = TendToZero(velocity.Y, decel);
        }

        private void ApplyGravity(Map map)
        {
            if (jumping == true || OnGround(map) == Rectangle.Empty)
                velocity.Y += gravity;

            if (velocity.Y > maxFallVelocity)
                velocity.Y = maxFallVelocity;
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

        protected bool Jump(Map map)
        {
            if (jumping == true)
                return false;

            if (velocity.Y == 0 && OnGround(map) != Rectangle.Empty)
            {
                velocity.Y -= jumpVelocity;
                jumping = true;
                return true;
            }

            return false;
        }

        protected virtual bool CheckCollisions(Map map, List<Sprite> sprites, bool xAxis)
        {
            Rectangle futureBoundingBox = BoundingBox;

            int maxX = (int)maxSpeed;
            int maxY = (int)maxSpeed;

            if (applyGravity == true)
                maxY = (int)jumpVelocity;

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

            // Check for sprite collisions (not working)
            // foreach (var sprite in sprites)
            // {
            //     if (sprite != this && sprite.isActive == true && sprite.collidable == true && 
            //     sprite.CheckCollisions(futureBoundingBox) == true)
            //         return true;
            // }

            return false;
        }

        public void LandResponse(Rectangle wallCollision)
        {
            _position.Y = wallCollision.Top - (boundingBoxHeight + boundingBoxOffset.Y);
            velocity.Y = 0;
            jumping = false;
        }

        protected Rectangle OnGround(Map map)
        {
            Rectangle futureHitBox = new Rectangle((int)(_position.X + boundingBoxOffset.X),
              (int)(_position.Y + boundingBoxOffset.Y + (velocity.Y + gravity)), boundingBoxWidth, boundingBoxHeight);
            return map.CheckCollisions(futureHitBox);
        }

        protected float TendToZero(float val, float amount)
        {
            if (val > 0f && (val -= amount) < 0f) return 0f;
            if (val < 0f && (val += amount) > 0f) return 0f;
            return val;
        }
    }
}
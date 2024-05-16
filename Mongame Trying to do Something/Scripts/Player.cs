using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mongame_Trying_to_do_Something.Scripts
{
    public class Player : CollisionBox
    {
        public Vector2 Position { get; set; }
        public Vector2 StartPosition { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool IsOnGround { get; private set; }
        private Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } }
        private float gravity = 0.35f;
        private float jumpSpeed = -8;
        private float jumpCooldown = 0.5f; // Jump cooldown in seconds
        private float timeSinceLastJump = 0; // Time elapsed since last jump

        public Player(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Position = new Vector2(rectangle.X, rectangle.Y);
            StartPosition = Position;
            Texture = texture;
            IsOnGround = false;
        }

        public void Update(GameTime gameTime, Vector2 direction, bool isJumping, List<Platform> platforms)
        {
            // Update the jump timer
            if (timeSinceLastJump < jumpCooldown)
            {
                timeSinceLastJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            float newX = Position.X + direction.X * 2;
            float newY = Position.Y;

            // Attempt vertical jump
            if (IsOnGround && isJumping && timeSinceLastJump >= jumpCooldown)
            {
                velocity.Y = jumpSpeed;
                IsOnGround = false;
                timeSinceLastJump = 0; // Reset jump timer
            }

            velocity.Y += gravity;
            newY += velocity.Y;

            // Check for horizontal collisions
            Rectangle futureHorizontalRect = new Rectangle((int)newX, (int)Position.Y, Rectangle.Width, Rectangle.Height);
            if (CheckCollisions(futureHorizontalRect, platforms))
            {
                newX = Position.X;
            }

            // Check for vertical collisions
            Rectangle futureVerticalRect = new Rectangle((int)Position.X, (int)newY, Rectangle.Width, Rectangle.Height);
            if (CheckCollisions(futureVerticalRect, platforms))
            {
                newY = Position.Y;
                velocity.Y = 0;
                IsOnGround = true;
            }

            Position = new Vector2(newX, newY);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);
        }

        private bool CheckCollisions(Rectangle futureRect, List<Platform> platforms)
        {
            foreach (var platform in platforms)
            {
                if (futureRect.Intersects(platform.Rectangle))
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }

        public void ResetPosition()
        {
            Position = StartPosition; // Reset position to the start position
            velocity = Vector2.Zero; // Reset velocity
        }
    }
}

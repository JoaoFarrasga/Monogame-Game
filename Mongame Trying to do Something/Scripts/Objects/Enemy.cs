using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    public class Enemy : CollisionBox
    {
        public Texture2D Texture { get; private set; }
        private Vector2 velocity;
        private float gravity = 0.35f;
        private float speed = 1.5f;

        public Enemy(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
            velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime, Player player, List<Platform> platforms)
        {
            Vector2 direction = Vector2.Zero;

            if (player.Position.X > Position.X)
            {
                direction.X = 1;
            }
            else if (player.Position.X < Position.X)
            {
                direction.X = -1;
            }

            float newX = Position.X + direction.X * speed;
            float newY = Position.Y;

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
            }

            Position = new Vector2(newX, newY);
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
            spriteBatch.Draw(Texture, Rectangle, Color.White); // Use the Rectangle defined in the base class
        }

        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

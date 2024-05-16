using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mongame_Trying_to_do_Something.Scripts
{
    public class Enemy : CollisionBox
    {
        public Texture2D Texture { get; private set; }

        public Enemy(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
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

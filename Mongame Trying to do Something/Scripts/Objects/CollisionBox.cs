using Microsoft.Xna.Framework;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    public class CollisionBox
    {
        public Rectangle Rectangle { get; set; }
        public Vector2 Position
        {
            get => new Vector2(Rectangle.X, Rectangle.Y);
            set
            {
                Rectangle = new Rectangle((int)value.X, (int)value.Y, Rectangle.Width, Rectangle.Height);
            }
        }

        public CollisionBox(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }
    }
}
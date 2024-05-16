using Microsoft.Xna.Framework;

namespace Mongame_Trying_to_do_Something.Scripts
{
    public class CollisionBox
    {
        public Rectangle Rectangle { get; set; }

        public CollisionBox(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }
    }
}

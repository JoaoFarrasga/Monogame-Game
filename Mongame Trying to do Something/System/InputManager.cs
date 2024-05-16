using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mongame_Trying_to_do_Something.System
{
    public class InputManager
    {
        public Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }

            return direction;
        }

        public bool IsJumping()
        {
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up);
        }
    }
}

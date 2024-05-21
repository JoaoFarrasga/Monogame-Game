using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mongame_Trying_to_do_Something.System
{
    //Classe que gere a entrada do utilizador
    public class InputManager
    {
        //Método que obtém a direção do movimento com base na entrada do teclado
        public Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            //Verifica se as teclas A ou Left estão pressionadas para mover para a esquerda
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            //Verifica se as teclas D ou Right estão pressionadas para mover para a direita
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }

            return direction; //Retorna a direção do movimento
        }

        //Método que verifica se o jogador está a tentar saltar
        public bool IsJumping()
        {
            KeyboardState state = Keyboard.GetState();
            //Verifica se as teclas W, Space ou Up estão pressionadas para saltar
            return state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up);
        }
    }
}

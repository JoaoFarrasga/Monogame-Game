using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    //Classe que representa um inimigo no jogo, herda da CollisionBox
    public class Enemy : CollisionBox
    {
        //Propriedade que armazena a textura do inimigo
        public Texture2D Texture { get; private set; }

        //Velocidade e gravidade do inimigo
        private Vector2 velocity;
        private float gravity = 0.35f;
        private float speed = 1.5f;

        //Construtor da classe Enemy que inicializa o inimigo com um retângulo e uma textura
        public Enemy(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
            velocity = Vector2.Zero;
        }

        //Método que atualiza o estado do inimigo
        public void Update(GameTime gameTime, Player player, List<Platform> platforms)
        {
            Vector2 direction = Vector2.Zero;

            //Define a direção do movimento do inimigo baseado na posição do jogador
            if (player.Position.X > Position.X)
            {
                direction.X = 1;
            }
            else if (player.Position.X < Position.X)
            {
                direction.X = -1;
            }

            //Calcula a nova posição do inimigo
            float newX = Position.X + direction.X * speed;
            float newY = Position.Y;

            //Aplica a gravidade à velocidade vertical
            velocity.Y += gravity;
            newY += velocity.Y;

            //Verifica colisões horizontais
            Rectangle futureHorizontalRect = new Rectangle((int)newX, (int)Position.Y, Rectangle.Width, Rectangle.Height);
            if (CheckCollisions(futureHorizontalRect, platforms))
            {
                newX = Position.X;
            }

            //Verifica colisões verticais
            Rectangle futureVerticalRect = new Rectangle((int)Position.X, (int)newY, Rectangle.Width, Rectangle.Height);
            if (CheckCollisions(futureVerticalRect, platforms))
            {
                newY = Position.Y;
                velocity.Y = 0;
            }

            //Atualiza a posição do inimigo
            Position = new Vector2(newX, newY);
        }

        //Método que verifica colisões com as plataformas
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

        //Método para renderizar o inimigo na tela
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White); //Usa o retângulo definido na classe base
        }

        //Método para definir uma nova textura para o inimigo
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

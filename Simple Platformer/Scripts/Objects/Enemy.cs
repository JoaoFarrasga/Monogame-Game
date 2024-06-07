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
        private float gravity = 0.7f;
        private float speed = 3.0f;

        private List<Rectangle> frames;
        private int frameWidth;
        private int frameHeight;
        private int currentFrame;
        private float frameTime;
        private float timeElapsed;

        //Construtor da classe Enemy que inicializa o inimigo com um retângulo e uma textura
        public Enemy(Rectangle rectangle, Texture2D texture)
    : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
            velocity = Vector2.Zero;

            // Initialize animation fields
            frameWidth = texture.Width / 8; // assuming 4 frames in a row
            frameHeight = texture.Height;
            frames = new List<Rectangle>();
            for (int i = 0; i < 8; i++)
            {
                frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
            }
            currentFrame = 0;
            frameTime = 0.1f; // 10 frames per second
            timeElapsed = 0;
        }

        //Método que atualiza o estado do inimigo
        public void Update(GameTime gameTime, Player player, List<Platform> platforms)
        {
            Vector2 direction = Vector2.Zero;

            // Define the direction of enemy movement based on player's position
            if (player.Position.X > Position.X)
            {
                direction.X = 1;
            }
            else if (player.Position.X < Position.X)
            {
                direction.X = -1;
            }

            // Handle animation
            if (direction.X != 0)
            {
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeElapsed > frameTime)
                {
                    currentFrame = (currentFrame + 1) % frames.Count;
                    timeElapsed -= frameTime;
                }
            }
            else
            {
                currentFrame = 0; // Reset to the first frame when idle
            }

            // Calculate new position
            float newX = Position.X + direction.X * speed;
            float newY = Position.Y;

            // Apply gravity to vertical velocity
            velocity.Y += gravity;
            newY += velocity.Y;

            // Check horizontal collisions
            Rectangle futureHorizontalRect = new Rectangle((int)newX, (int)Position.Y, Rectangle.Width, Rectangle.Height);
            if (CheckCollisions(futureHorizontalRect, platforms))
            {
                newX = Position.X;
            }

            // Check vertical collisions
            Rectangle futureVerticalRect = new Rectangle((int)Position.X, (int)newY, Rectangle.Width, Rectangle.Height);
            if (CheckCollisions(futureVerticalRect, platforms))
            {
                newY = Position.Y;
                velocity.Y = 0;
            }

            // Update enemy position
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
            spriteBatch.Draw(Texture, Rectangle, frames[currentFrame], Color.White);
        }

        //Método para definir uma nova textura para o inimigo
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

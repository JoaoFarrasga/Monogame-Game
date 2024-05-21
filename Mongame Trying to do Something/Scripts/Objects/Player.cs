using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mongame_Trying_to_do_Something.Scripts.Objects;
using System.Collections.Generic;

namespace Mongame_Trying_to_do_Something.Scripts
{
    //Classe que representa o jogador no jogo, herda da CollisionBox
    public class Player : CollisionBox
    {
        //Propriedades do jogador
        public Vector2 Position { get; set; }
        public Vector2 StartPosition { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool IsOnGround { get; private set; }
        private Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } }
        private float gravity = 0.35f;
        private float jumpSpeed = -8;
        private float jumpCooldown = 0.5f; //Cooldown do salto em segundos
        private float timeSinceLastJump = 0; //Tempo desde o último salto

        private SoundEffectInstance jumpSound;

        //Construtor da classe Player que inicializa o jogador com um retângulo, uma textura e um som de salto
        public Player(Rectangle rectangle, Texture2D texture, SoundEffectInstance jumpSound)
        private List<Rectangle> frames;
        private int frameWidth;
        private int frameHeight;
        private int currentFrame;
        private float frameTime;
        private float timeElapsed;

        public Player(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Position = new Vector2(rectangle.X, rectangle.Y);
            StartPosition = Position;
            Texture = texture;
            IsOnGround = false;
            this.jumpSound = jumpSound;
        }

        //Método que atualiza o estado do jogador
        public void Update(GameTime gameTime, Vector2 direction, bool isJumping, List<Platform> platforms, List<Coin> coins, PlatformManager platformManager)
        {
            //Atualiza o Cooldown do salto
            if (timeSinceLastJump < jumpCooldown)
            {
                timeSinceLastJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            float newX = Position.X + direction.X * 2;
            float newY = Position.Y;

            //Tenta realizar o salto vertical
            if (IsOnGround && isJumping && timeSinceLastJump >= jumpCooldown)
            {
                velocity.Y = jumpSpeed;
                IsOnGround = false;
                timeSinceLastJump = 0; //Reseta o Cooldown do salto
                jumpSound.Play(); //Toca o som do salto
            }

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
                IsOnGround = true;
            }

            //Atualiza a posição do jogador
            Position = new Vector2(newX, newY);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);

            //Verifica a coleta de moedas
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                if (Rectangle.Intersects(coins[i].Rectangle))
                {
                    coins.RemoveAt(i);
                    platformManager.addCollectedCoins(); //Incrementa diretamente as moedas coletadas
                }
            }
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

        //Método para desenhar o jogador na tela
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        //Método para definir uma nova textura para o jogador
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }

        //Método para resetar a posição do jogador
        public void ResetPosition()
        {
            Position = StartPosition; //Reseta a posição para a posição inicial
            velocity = Vector2.Zero; //Reseta a velocidade
        }
    }
}

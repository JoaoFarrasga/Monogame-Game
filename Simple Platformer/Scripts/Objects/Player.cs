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
        private float gravity = 0.7f;
        private float jumpSpeed = -16;
        private float jumpCooldown = 0.5f; //Cooldown do salto em segundos
        private float timeSinceLastJump = 0; //Tempo desde o último salto

        private SoundEffectInstance jumpSound;

        //Construtor da classe Player que inicializa o jogador com um retângulo, uma textura e um som de salto
        private List<Rectangle> frames;
        private int frameWidth;
        private int frameHeight;
        private int currentFrame;
        private float frameTime;
        private float timeElapsed;

        public Player(Rectangle rectangle, Texture2D texture, SoundEffectInstance jumpSound)
    : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Position = new Vector2(rectangle.X, rectangle.Y);
            StartPosition = Position;
            Texture = texture;
            IsOnGround = false;
            this.jumpSound = jumpSound;

            // Initialize animation fields
            frameWidth = texture.Width / 6; // assuming 4 frames in a row
            frameHeight = texture.Height;
            frames = new List<Rectangle>();
            for (int i = 0; i < 6; i++)
            {
                frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
            }
            currentFrame = 0;
            frameTime = 0.1f; // 10 frames per second
            timeElapsed = 0;
        }

        //Método que atualiza o estado do jogador
        public void Update(GameTime gameTime, Vector2 direction, bool isJumping, List<Platform> platforms, List<Coin> coins, PlatformManager platformManager)
        {
            // Update jump cooldown
            if (timeSinceLastJump < jumpCooldown)
            {
                timeSinceLastJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Double the horizontal movement speed
            float newX = Position.X + direction.X * 4; // Changed from 2 to 4
            float newY = Position.Y;

            // Handle jumping
            if (IsOnGround && isJumping && timeSinceLastJump >= jumpCooldown)
            {
                velocity.Y = jumpSpeed;
                IsOnGround = false;
                timeSinceLastJump = 0; // Reset jump cooldown
                jumpSound.Play(); // Play jump sound
            }

            // Apply gravity to vertical velocity
            velocity.Y += gravity;
            newY += velocity.Y;

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
                IsOnGround = true;
            }

            // Update player position
            Position = new Vector2(newX, newY);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);

            // Check for coin collection
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                if (Rectangle.Intersects(coins[i].Rectangle))
                {
                    coins.RemoveAt(i);
                    platformManager.addCollectedCoins(); // Increment collected coins
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
            spriteBatch.Draw(Texture, Rectangle, frames[currentFrame], Color.White);
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

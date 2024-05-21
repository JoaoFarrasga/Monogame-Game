using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Mongame_Trying_to_do_Something.System;
using Mongame_Trying_to_do_Something.Scripts;
using Mongame_Trying_to_do_Something.Scripts.Managers;

namespace Mongame_Trying_to_do_Something
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlatformManager platformManager;
        InputManager inputManager;
        GameStateManager gameStateManager;
        GameState gameState;
        Camera2D camera;
        SpriteFont font;

        private SoundEffect backgroundMusic;
        private SoundEffect jumpSound;
        private SoundEffect doorSound;
        private SoundEffect winSound;
        private SoundEffect coinSound;
        
        private SoundEffectInstance backgroundMusicInstance;
        private SoundEffectInstance jumpSoundInstance;
        private SoundEffectInstance doorSoundInstance;
        private SoundEffectInstance winSoundInstance;
        private SoundEffectInstance coinSoundInstance;

        //Construtor do Jogo
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = GameState.Menu;
        }

        //Inicializa o Jogo
        protected override void Initialize()
        {
            //Crias as instancias que vão ser usadas no Jogo
            platformManager = new PlatformManager(GraphicsDevice, this);
            inputManager = new InputManager();
            gameStateManager = new GameStateManager(this, platformManager, inputManager);
            camera = new Camera2D(GraphicsDevice.Viewport);

            base.Initialize();
        }

        //Carrega o Content necessario para o Jogo
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Text"); //Carrega a Fonte para os Menus

            //Carrega a Musica de Fundo
            backgroundMusic = Content.Load<SoundEffect>("Background Music");
            backgroundMusicInstance = backgroundMusic.CreateInstance();
            backgroundMusicInstance.IsLooped = true; // Poe a Musica em Loop
            backgroundMusicInstance.Volume = 0.01f; // Poe o Volume da Musica a 1%
            backgroundMusicInstance.Play(); // Poe a Musica a dar

            //Carrega os Efeitos Sonoros
            jumpSound = Content.Load<SoundEffect>("Jump"); //Som para o Salto
            jumpSoundInstance = jumpSound.CreateInstance();
            jumpSoundInstance.Volume = 0.01f;

            doorSound = Content.Load<SoundEffect>("Door_Sound"); //Som para a Porta
            doorSoundInstance = doorSound.CreateInstance();
            doorSoundInstance.Volume = 0.01f;

            winSound = Content.Load<SoundEffect>("Victory"); //Som para a Vitoria
            winSoundInstance = winSound.CreateInstance();
            winSoundInstance.Volume = 0.01f;

            coinSound = Content.Load<SoundEffect>("Coin"); //Som para a Moeda
            coinSoundInstance = coinSound.CreateInstance();
            coinSoundInstance.Volume = 0.01f;

            //Poe os Efeitos Sonoros para o Platform Manager
            platformManager.addJumpSound(jumpSoundInstance); 
            platformManager.addCoinSound(coinSoundInstance);
        }

        //Change State muda o Estado do Jogo para o Estado desejado
        public void ChangeState(GameState newState)
        {
            gameState = newState;
        }

        //Update da update ao Jogo
        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                //Se for Menu trata do que for preciso em questão ao Menu
                case GameState.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        platformManager.resetGame(); //Reseta o Jogo no inicio isto para que quando o Jogador jogar Novamente não criar Problemas
                        gameState = GameState.Game; //Muda do Estado para o Jogo
                    }
                    break;
                //Se for Jogo trata do que for preciso em questão ao Jogo
                case GameState.Game:
                    gameStateManager.UpdateGame(gameTime); //Da Update ao gameStateManager
                    camera.Update(platformManager.Player.Position); //Da Update a Camara com a posição do Player

                    //Se o player toca na Porta passa para o proximo nivel e da o som da porta
                    if (platformManager.Player.Rectangle.Intersects(platformManager.Door.Rectangle))
                    {
                        doorSound.Play();
                        platformManager.NextLevel();
                    }
                    break;
                //Se for Vitoria trata do que for preciso em questão ao Vitoria
                case GameState.Victory:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        gameState = GameState.Menu;
                    break;
            }
            base.Update(gameTime);
        }

        //Renderiza o que é preciso no ecra
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Começa o SpriteBatch com a Camara
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

            switch (gameState)
            {
                //Renderiza a String do Menu
                case GameState.Menu:
                    spriteBatch.DrawString(font, "Simple Platformer\nPress Space to Play", new Vector2(-50, 0), Color.White);
                    break;
                //Renderiza o que é Preciso do Jogo
                case GameState.Game:
                    platformManager.DrawPlatforms(spriteBatch); //Rederiza as Plataformas
                    platformManager.DrawPlayer(spriteBatch); //Renderiza o Jogador
                    platformManager.DrawEnemies(spriteBatch); //Renderiza os Inimigos
                    platformManager.DrawDoor(spriteBatch); //Renderiza a Porta
                    platformManager.DrawCoins(spriteBatch); //Renderiza as Moedas
                    break;
                //Renderiza as Strings da Vitoria
                case GameState.Victory:
                    winSound.Play();
                    spriteBatch.DrawString(font, "You Won! Good Job\nPress Space to go to the Menu", new Vector2(200, 0), Color.White);
                    spriteBatch.DrawString(font, $"Enemies killed: {platformManager.EnemyKilled}", new Vector2(200, 50), Color.White);
                    spriteBatch.DrawString(font, $"Coins collected: {platformManager.CollectedCoins}", new Vector2(200, 100), Color.White);
                    break;
            }

            spriteBatch.End(); //Acaba a SpriteBatch

            //Renderiza o UI, no caso só as coins
            spriteBatch.Begin();
            if (gameState == GameState.Game)
            {
                spriteBatch.DrawString(font, $"Coins: {platformManager.CollectedCoins}", new Vector2(10, 10), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

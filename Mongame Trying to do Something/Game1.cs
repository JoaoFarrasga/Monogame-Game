using Microsoft.Xna.Framework;
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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = GameState.Menu;
        }

        protected override void Initialize()
        {
            platformManager = new PlatformManager(GraphicsDevice, this);
            inputManager = new InputManager();
            gameStateManager = new GameStateManager(this, platformManager, inputManager);
            camera = new Camera2D(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Text"); // Load the font for displaying the UI
        }

        public void ChangeState(GameState newState)
        {
            gameState = newState;
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        platformManager.ResetLevel(); // Reset the game to the first level
                        gameState = GameState.Game;
                    }
                    break;
                case GameState.Game:
                    gameStateManager.UpdateGame(gameTime);
                    camera.Update(platformManager.Player.Position); // Update camera position based on player position

                    if (platformManager.Player.Rectangle.Intersects(platformManager.Door.Rectangle))
                    {
                        platformManager.NextLevel();  // Load next level or change to Victory state
                    }
                    break;
                case GameState.Victory:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        gameState = GameState.Menu;
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw game world with camera transformation
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

            switch (gameState)
            {
                case GameState.Menu:
                    spriteBatch.DrawString(font, "Simple Platformer\nPress Space to Play", new Vector2(100, 100), Color.White);
                    break;
                case GameState.Game:
                    platformManager.DrawPlatforms(spriteBatch);
                    platformManager.DrawPlayer(spriteBatch);
                    platformManager.DrawEnemies(spriteBatch); // Draw enemies
                    platformManager.DrawDoor(spriteBatch);
                    platformManager.DrawCoins(spriteBatch); // Draw coins
                    break;
                case GameState.Victory:
                    spriteBatch.DrawString(font, "You Won! Good Job\nPress Space to go to the Menu", new Vector2(100, 100), Color.White);
                    break;
            }

            spriteBatch.End(); // End sprite batch once

            // Draw UI without camera transformation
            spriteBatch.Begin();
            if (gameState == GameState.Game)
            {
                spriteBatch.DrawString(font, $"Coins: {platformManager.CollectedCoins}", new Vector2(10, 10), Color.White);
            }
            spriteBatch.End(); // End sprite batch once

            base.Draw(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Mongame_Trying_to_do_Something.System;
using Mongame_Trying_to_do_Something.Scripts;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = GameState.Menu;
        }

        protected override void Initialize()
        {
            platformManager = new PlatformManager(GraphicsDevice);
            inputManager = new InputManager();
            gameStateManager = new GameStateManager(this, platformManager, inputManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
                        gameState = GameState.Game;
                    break;
                case GameState.Game:
                    gameStateManager.UpdateGame(gameTime);
                    if (platformManager.Player.Rectangle.Intersects(platformManager.Door.Rectangle))
                    {
                        platformManager.NextLevel();  // Load next level
                    }
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); // Begin sprite batch once

            switch (gameState)
            {
                case GameState.Menu:
                    spriteBatch.DrawString(Content.Load<SpriteFont>("Text"), "Simple Platformer\nPress Space to Play", new Vector2(100, 100), Color.White);
                    break;
                case GameState.Game:
                    platformManager.DrawPlatforms(spriteBatch);
                    platformManager.DrawPlayer(spriteBatch);
                    platformManager.DrawEnemies(spriteBatch); // Draw enemies
                    platformManager.DrawDoor(spriteBatch);
                    break;
            }

            spriteBatch.End(); // End sprite batch once
            base.Draw(gameTime);
        }

    }
}

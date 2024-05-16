using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using Mongame_Trying_to_do_Something.System;

namespace Mongame_Trying_to_do_Something.Scripts
{
    public class GameStateManager
    {
        private PlatformManager platformManager;
        private InputManager inputManager;
        private Game1 game;

        public GameStateManager(Game1 game, PlatformManager platformManager, InputManager inputManager)
        {
            this.game = game;
            this.platformManager = platformManager;
            this.inputManager = inputManager;
        }

        public void UpdateGame(GameTime gameTime)
        {
            Vector2 direction = inputManager.GetMovementDirection();
            bool isJumping = inputManager.IsJumping();
            platformManager.Player.Update(gameTime, direction, isJumping, platformManager.Platforms);

            List<Enemy> enemiesToRemove = new List<Enemy>();

            // Check for collisions with enemies
            foreach (var enemy in platformManager.Enemies)
            {
                if (platformManager.Player.Rectangle.Intersects(enemy.Rectangle))
                {
                    // Check if the player's bottom is above the enemy's top and falling down
                    if (platformManager.Player.Rectangle.Bottom <= enemy.Rectangle.Top + 10 && platformManager.Player.Velocity.Y > 0)
                    {
                        enemiesToRemove.Add(enemy);
                    }
                    else
                    {
                        platformManager.Player.ResetPosition();
                        break;
                    }
                }
            }

            foreach (var enemy in enemiesToRemove)
            {
                platformManager.Enemies.Remove(enemy);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.ChangeState(GameState.Menu);
        }
    }
}

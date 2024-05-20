using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using Mongame_Trying_to_do_Something.System;
using Mongame_Trying_to_do_Something.Scripts.Objects;

namespace Mongame_Trying_to_do_Something.Scripts.Managers
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
            platformManager.Player.Update(gameTime, direction, isJumping, platformManager.Platforms, platformManager.Coins, ref platformManager.CollectedCoins);

            List<Enemy> enemiesToRemove = new List<Enemy>();

            // Update enemies and check for collisions with player
            foreach (var enemy in platformManager.Enemies)
            {
                enemy.Update(gameTime, platformManager.Player, platformManager.Platforms);

                if (platformManager.Player.Rectangle.Intersects(enemy.Rectangle))
                {
                    // Check if the player's bottom is above the enemy's top and falling down
                    if (platformManager.Player.Rectangle.Bottom <= enemy.Rectangle.Top + 10 && platformManager.Player.Velocity.Y > 0)
                    {
                        enemiesToRemove.Add(enemy);
                    }
                    else
                    {
                        platformManager.ResetLevel();
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

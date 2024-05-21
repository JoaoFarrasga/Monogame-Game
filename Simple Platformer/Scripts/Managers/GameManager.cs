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

        //Construtor do GameStateManager que inicializa as referências para o Jogo, PlatformManager e InputManager
        public GameStateManager(Game1 game, PlatformManager platformManager, InputManager inputManager)
        {
            this.game = game;
            this.platformManager = platformManager;
            this.inputManager = inputManager;
        }

        //Método que atualiza o estado do jogo
        public void UpdateGame(GameTime gameTime)
        {
            //Obtém a direção do movimento e verifica se o jogador está a Saltar
            Vector2 direction = inputManager.GetMovementDirection();
            bool isJumping = inputManager.IsJumping();

            //Atualiza o estado do jogador
            platformManager.Player.Update(gameTime, direction, isJumping, platformManager.Platforms, platformManager.Coins, platformManager);

            List<Enemy> enemiesToRemove = new List<Enemy>();

            //Atualiza os inimigos e verifica se existem colisões com o Jogador
            foreach (var enemy in platformManager.Enemies)
            {
                enemy.Update(gameTime, platformManager.Player, platformManager.Platforms);

                //Verifica se o jogador colidiu com um inimigo
                if (platformManager.Player.Rectangle.Intersects(enemy.Rectangle))
                {
                    //Se a parte inferior do jogador está acima da parte superior do inimigo e o jogador está a cair
                    if (platformManager.Player.Rectangle.Bottom <= enemy.Rectangle.Top + 10 && platformManager.Player.Velocity.Y > 0)
                    {
                        enemiesToRemove.Add(enemy); //Adiciona o inimigo à lista para remover
                        platformManager.addEnemyKilled(); //Aumenta o contador de inimigos mortos
                    }
                    else
                    {
                        platformManager.ResetLevel(); //Reseta o nível se o jogador colide com o inimigo sem matá-lo
                        break;
                    }
                }
            }

            //Remove os inimigos mortos
            foreach (var enemy in enemiesToRemove)
            {
                platformManager.Enemies.Remove(enemy);
            }

            //Verifica se a tecla Esc foi pressionada para retornar ao menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.ChangeState(GameState.Menu);
        }
    }
}

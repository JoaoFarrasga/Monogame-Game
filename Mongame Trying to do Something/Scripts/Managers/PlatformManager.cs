using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mongame_Trying_to_do_Something.Scripts.Objects;
using System.Collections.Generic;
using System.IO;

namespace Mongame_Trying_to_do_Something.Scripts
{
    //Classe que gere as plataformas, inimigos, moedas e o jogador
    public class PlatformManager
    {
        private GraphicsDevice graphicsDevice;
        public List<Platform> Platforms;
        public Player Player;
        public List<Enemy> Enemies;
        public Door Door;
        public List<Coin> Coins;

        public SoundEffectInstance jumpSound;
        public SoundEffectInstance coinSound;

        public int CollectedCoins { get; private set; }
        public int EnemyKilled { get; private set; }

        private Texture2D platformTexture, playerTexture, enemyTexture, doorTexture, coinTexture;
        private const int tileSize = 32;

        private string[][] levels = new string[][]
        {
            new string[]  //Nível 1
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                "X-------------------------------------------X",
                "X---------E---------------------------------X",
                "X-------XXXX------D-----------------CC------X",
                "X--------------XXXXXX------CC------XXXX-----X",
                "X----------------------X--------------------X",
                "X----E--------------------XXXX--------------X",
                "X--XXXX------------E-------------C----------X",
                "X----------------XXXX-----------XXX----C----X",
                "X--------XXXXX------------------------XXX---X",
                "X-P------C---------CC----CC---------E-------X",
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            },
            new string[]  //Nível 2
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                "X-------------------------------------------X",
                "X-------------------------------------------X",
                "X---------------------------------------D---X",
                "X----------------------------------E---XXX--X",
                "X----------------------------E----XXX-------X",
                "X----------------------E----XXX-------------X",
                "X----------------E----XXX------------CCCC---X",
                "X-----------E---XXX-----------------XXXXXX--X",
                "X----------XXX------------------------------X",
                "X-P-----------------------------------------X",
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            },
            new string[]  //Nível 3
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                "X-------------------------------------------X",
                "X-------------------------------------------X",
                "X-----D-------------------------------------X",
                "X----XXX---------------------------CC-------X",
                "X---------------------------------XXXX------X",
                "XX-----------------------E-XXXX-------------X",
                "X------E-------------E--XX------------------X",
                "X----XXXXXX---------XXX--------------C------X",
                "X-------------XXX-------------------XXX-----X",
                "X----P----------------------E----E----------X",
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            },
            new string[]  //Nível 4
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                "X-------------------------------------------X",
                "X---------------D---------------------------X",
                "X--------------XXX--------------------------X",
                "X-------------------------------------------X",
                "X----------XX-------------------------------X",
                "X--------------------------------------C----X",
                "X------XX------------C-----C-----C----CCC---X",
                "X-------------------XXX---XXX---XXX---XXX---X",
                "X-XXX------------X--------------------------X",
                "X-------P-------X----E---E---E---E---E---E--X",
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            }
        };
        private int currentLevelIndex = 0;
        private Game1 game;

        //Construtor que inicializa o PlatformManager
        public PlatformManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;
            this.game = game;
            Platforms = new List<Platform>();
            Enemies = new List<Enemy>();
            Coins = new List<Coin>();
            CollectedCoins = 0;
            EnemyKilled = 0;
            LoadTextures();
            LoadLevel(currentLevelIndex);
        }

        //Carrega as texturas a partir dos ficheiros
        private void LoadTextures()
        {
            using (FileStream fileStream = new FileStream("Content/platform.png", FileMode.Open))
            {
                platformTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            playerTexture = new Texture2D(graphicsDevice, 1, 1);
            playerTexture.SetData(new Color[] { Color.Purple });

            enemyTexture = new Texture2D(graphicsDevice, 1, 1);
            enemyTexture.SetData(new Color[] { Color.Red });

            using (FileStream fileStream = new FileStream("Content/door2.png", FileMode.Open))
            {
                doorTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            coinTexture = new Texture2D(graphicsDevice, 1, 1);
            coinTexture.SetData(new Color[] { Color.DarkBlue });
        }

        //Carrega um nível específico
        public void LoadLevel(int levelIndex)
        {
            Platforms.Clear();
            Enemies.Clear();
            Coins.Clear();

            string[] levelMap = levels[levelIndex];

            for (int y = 0; y < levelMap.Length; y++)
            {
                for (int x = 0; x < levelMap[y].Length; x++)
                {
                    char tile = levelMap[y][x];
                    var rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    switch (tile)
                    {
                        case 'X':
                            Platforms.Add(new Platform(rect, platformTexture));
                            break;
                        case 'P':
                            Player = new Player(rect, playerTexture, jumpSound);
                            break;
                        case 'E':
                            Enemies.Add(new Enemy(rect, enemyTexture));
                            break;
                        case 'D':
                            Door = new Door(rect, doorTexture);
                            break;
                        case 'C':
                            Coins.Add(new Coin(rect, coinTexture));
                            break;
                    }
                }
            }
        }

        //Adiciona um inimigo morto ao contador
        public void addEnemyKilled()
        {
            EnemyKilled++;
        }

        //Adiciona uma moeda coletada ao contador e toca o som
        public void addCollectedCoins()
        {
            coinSound.Play();
            CollectedCoins++;
        }

        //Reseta o nível atual
        public void ResetLevel()
        {
            LoadLevel(currentLevelIndex);
        }

        //Define as texturas para os diferentes elementos do jogo
        public void SetTextures(Texture2D platformTexture, Texture2D playerTexture, Texture2D enemyTexture, Texture2D doorTexture, Texture2D coinTexture)
        {
            foreach (var platform in Platforms)
                platform.SetTexture(platformTexture);

            Player.SetTexture(playerTexture);

            foreach (var enemy in Enemies)
                enemy.SetTexture(enemyTexture);

            Door.SetTexture(doorTexture);

            foreach (var coin in Coins)
                coin.SetTexture(coinTexture);
        }

        //Carrega o próximo nível, ou muda para o estado de vitória se não houver mais níveis
        public void NextLevel()
        {
            if (currentLevelIndex < levels.Length - 1)
            {
                currentLevelIndex++;
                LoadLevel(currentLevelIndex);
            }
            else
            {
                game.ChangeState(GameState.Victory); //Muda para o estado de vitória
            }
        }

        //Reseta o jogo, carregando o primeiro nível
        public void resetGame()
        {
            LoadLevel(0);
        }

        //Adiciona o som do salto
        public void addJumpSound(SoundEffectInstance jumpSound)
        {
            this.jumpSound = jumpSound;
        }

        //Adiciona o som da moeda
        public void addCoinSound(SoundEffectInstance coinSound)
        {
            this.coinSound = coinSound;
        }

        //Desenha as plataformas no ecrã
        public void DrawPlatforms(SpriteBatch spriteBatch)
        {
            foreach (var platform in Platforms)
                platform.Draw(spriteBatch);
        }

        //Desenha o jogador no ecrã
        public void DrawPlayer(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }

        //Desenha os inimigos no ecrã
        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            foreach (var enemy in Enemies)
                enemy.Draw(spriteBatch);
        }

        //Desenha as moedas no ecrã
        public void DrawCoins(SpriteBatch spriteBatch)
        {
            foreach (var coin in Coins)
                coin.Draw(spriteBatch);
        }

        //Desenha a porta no ecrã
        public void DrawDoor(SpriteBatch spriteBatch)
        {
            if (Door != null)
                Door.Draw(spriteBatch);
        }
    }
}

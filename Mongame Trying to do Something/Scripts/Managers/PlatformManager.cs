using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mongame_Trying_to_do_Something.Scripts.Objects;
using System.Collections.Generic;
using System.IO;

namespace Mongame_Trying_to_do_Something.Scripts
{
    public class PlatformManager
    {
        private GraphicsDevice graphicsDevice;
        public List<Platform> Platforms;
        public Player Player;
        public List<Enemy> Enemies;
        public Door Door;
        public List<Coin> Coins;
        public int CollectedCoins;

        private Texture2D platformTexture, playerTexture, enemyTexture, doorTexture, coinTexture;
        private const int tileSize = 32;

        private string[][] levels = new string[][]
        {
            new string[]  // Level 1
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
            new string[]  // Level 2
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXX",
                "X--------P--------------X",
                "X-------XXXX------CC----X",
                "X---------------XXXXX---X",
                "X----E------------------X",
                "X--XXXX-----------C-----X",
                "X--------E------XXXX----X",
                "X--------XXXXX----------X",
                "X----------------D---C--X",
                "XXXXXXXXXXXXXXXXXXXXXXXXX"
            }
        };
        private int currentLevelIndex = 0;
        private Game1 game;

        public PlatformManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;
            this.game = game;
            Platforms = new List<Platform>();
            Enemies = new List<Enemy>();
            Coins = new List<Coin>();
            CollectedCoins = 0;
            LoadTextures();
            LoadLevel(currentLevelIndex);
        }

        private void LoadTextures()
        {
            using (FileStream fileStream = new FileStream("Content/platform.png", FileMode.Open))
            {
                platformTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            using (FileStream fileStream = new FileStream("Content/Run.png", FileMode.Open))
            {
                playerTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            using (FileStream fileStream = new FileStream("Content/enemy.png", FileMode.Open))
            {
                enemyTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            using (FileStream fileStream = new FileStream("Content/door2.png", FileMode.Open))
            {
                doorTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            using (FileStream fileStream = new FileStream("Content/coin.png", FileMode.Open))
            {
                coinTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            }
        }

        public void LoadLevel(int levelIndex)
        {
            Platforms.Clear();
            Enemies.Clear();
            Coins.Clear();
            CollectedCoins = 0;
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
                            Player = new Player(rect, playerTexture);
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

        public void ResetLevel()
        {
            LoadLevel(currentLevelIndex);
        }

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

        public void NextLevel()
        {
            if (currentLevelIndex < levels.Length - 1)
            {
                currentLevelIndex++;
                LoadLevel(currentLevelIndex);
            }
            else
            {
                game.ChangeState(GameState.Victory); // Change to Victory state
            }
        }

        public void DrawPlatforms(SpriteBatch spriteBatch)
        {
            foreach (var platform in Platforms)
                platform.Draw(spriteBatch);
        }

        public void DrawPlayer(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            foreach (var enemy in Enemies)
                enemy.Draw(spriteBatch);
        }

        public void DrawCoins(SpriteBatch spriteBatch)
        {
            foreach (var coin in Coins)
                coin.Draw(spriteBatch);
        }

        public void DrawDoor(SpriteBatch spriteBatch)
        {
            if (Door != null)
                Door.Draw(spriteBatch);
        }
    }
}

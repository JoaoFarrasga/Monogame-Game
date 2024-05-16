using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mongame_Trying_to_do_Something.Scripts
{
    public class PlatformManager
    {
        private GraphicsDevice graphicsDevice;
        public List<Platform> Platforms;
        public Player Player;
        public List<Enemy> Enemies;
        public Door Door;

        private Texture2D platformTexture, playerTexture, enemyTexture, doorTexture;
        private const int tileSize = 32;

        private string[][] levels = new string[][]
        {
            new string[]  // Level 1
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXX",
                "X-----------------------X",
                "X-------XXXX------------X",
                "X---------------XXXXX---X",
                "X----E------------------X",
                "X--XXXX------------E----X",
                "X----------------XXXX---X",
                "X--------XXXXX----------X",
                "X-P---------D-----------X",
                "XXXXXXXXXXXXXXXXXXXXXXXXX"
            },
            new string[]  // Level 2
            {
                "XXXXXXXXXXXXXXXXXXXXXXXXX",
                "X-----------------------X",
                "X-P-----XXXX------------X",
                "X---------------XXXXX---X",
                "X----E------------------X",
                "X--XXXX-----------------X",
                "X--------E------XXXX----X",
                "X--------XXXXX----------X",
                "X----------------D------X",
                "XXXXXXXXXXXXXXXXXXXXXXXXX"
            }
        };
        private int currentLevelIndex = 0;

        public PlatformManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            Platforms = new List<Platform>();
            Enemies = new List<Enemy>();
            LoadTextures();
            LoadLevel(currentLevelIndex);
        }

        private void LoadTextures()
        {
            platformTexture = new Texture2D(graphicsDevice, 1, 1);
            platformTexture.SetData(new Color[] { Color.Brown });

            playerTexture = new Texture2D(graphicsDevice, 1, 1);
            playerTexture.SetData(new Color[] { Color.Purple });

            enemyTexture = new Texture2D(graphicsDevice, 1, 1);
            enemyTexture.SetData(new Color[] { Color.Red });

            doorTexture = new Texture2D(graphicsDevice, 1, 1);
            doorTexture.SetData(new Color[] { Color.Yellow });
        }

        public void LoadLevel(int levelIndex)
        {
            Platforms.Clear();
            Enemies.Clear();
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
                    }
                }
            }
        }

        public void SetTextures(Texture2D platformTexture, Texture2D playerTexture, Texture2D enemyTexture, Texture2D doorTexture)
        {
            foreach (var platform in Platforms)
                platform.SetTexture(platformTexture);

            Player.SetTexture(playerTexture);

            foreach (var enemy in Enemies)
                enemy.SetTexture(enemyTexture); // Assign the texture to each enemy

            Door.SetTexture(doorTexture);

        }

        public void NextLevel()
        {
            currentLevelIndex = (currentLevelIndex + 1) % levels.Length;
            LoadLevel(currentLevelIndex);
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

        public void DrawDoor(SpriteBatch spriteBatch)
        {
            if (Door != null)
                Door.Draw(spriteBatch);
        }

    }
}

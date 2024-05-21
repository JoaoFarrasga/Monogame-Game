using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mongame_Trying_to_do_Something.Scripts.Objects;
using System.Collections.Generic;
using System.IO;

namespace Mongame_Trying_to_do_Something.Scripts.Managers
{
    public class PlayerAnimation
    {
        private List<Rectangle> frames;
        private Texture2D texture;
        private int frameWidth;
        private int frameHeight;
        private int currentFrame;
        private float frameTime;
        private float timeElapsed;

        public Texture2D Texture => texture;
        public Rectangle CurrentFrame => frames[currentFrame];

        public PlayerAnimation(List<Rectangle> frames, Texture2D texture, int frameWidth, int frameHeight, float frameTime = 0.2f)
        {
            this.frames = frames;
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameTime = frameTime;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= frameTime)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
                timeElapsed -= frameTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, frames[currentFrame], Color.White);
        }
    }
}

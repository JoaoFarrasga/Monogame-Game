using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Mongame_Trying_to_do_Something.Scripts.UI
{
    public class Button
    {
        private Texture2D texture;
        private SpriteFont font;
        private string text;
        private Vector2 position;
        private Rectangle bounds;
        private bool isHovering;

        public event EventHandler Click;
        public bool Clicked { get; private set; }

        public Button(Texture2D texture, SpriteFont font, string text, Vector2 position)
        {
            this.texture = texture;
            this.font = font;
            this.text = text;
            this.position = position;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(MouseState mouseState)
        {
            isHovering = bounds.Contains(mouseState.X, mouseState.Y);
            if (isHovering && mouseState.LeftButton == ButtonState.Pressed && !Clicked)
            {
                Clicked = true;
                Click?.Invoke(this, EventArgs.Empty);
            }
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                Clicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = isHovering ? Color.Gray : Color.White;
            spriteBatch.Draw(texture, bounds, color);

            var x = (bounds.X + (bounds.Width / 2)) - (font.MeasureString(text).X / 2);
            var y = (bounds.Y + (bounds.Height / 2)) - (font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
        }
    }
}

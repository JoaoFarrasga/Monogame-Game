using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Mongame_Trying_to_do_Something.Scripts.UI
{
    //Classe que representa um botão na interface do utilizador
    public class Button
    {
        //Propriedades e campos privados
        private Texture2D texture;
        private SpriteFont font;
        private string text;
        private Vector2 position;
        private Rectangle bounds;
        private bool isHovering;

        //Evento que é disparado quando o botão é clicado
        public event EventHandler Click;
        public bool Clicked { get; private set; }

        //Construtor da classe Button que inicializa o botão com uma textura, fonte, texto e posição
        public Button(Texture2D texture, SpriteFont font, string text, Vector2 position)
        {
            this.texture = texture;
            this.font = font;
            this.text = text;
            this.position = position;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        //Método que atualiza o estado do botão
        public void Update(MouseState mouseState)
        {
            //Verifica se o rato está sobre o botão
            isHovering = bounds.Contains(mouseState.X, mouseState.Y);

            //Verifica se o botão foi clicado
            if (isHovering && mouseState.LeftButton == ButtonState.Pressed && !Clicked)
            {
                Clicked = true;
                Click?.Invoke(this, EventArgs.Empty); //Dispara o evento de clique
            }
            //Verifica se o botão do rato foi libertado
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                Clicked = false;
            }
        }

        //Método que desenha o botão no ecrã
        public void Draw(SpriteBatch spriteBatch)
        {
            var color = isHovering ? Color.Gray : Color.White; //Muda a cor se o rato estiver sobre o botão
            spriteBatch.Draw(texture, bounds, color);

            //Calcula a posição do texto para que fique centralizado no botão
            var x = (bounds.X + (bounds.Width / 2)) - (font.MeasureString(text).X / 2);
            var y = (bounds.Y + (bounds.Height / 2)) - (font.MeasureString(text).Y / 2);

            //Desenha o texto no botão
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
        }
    }
}

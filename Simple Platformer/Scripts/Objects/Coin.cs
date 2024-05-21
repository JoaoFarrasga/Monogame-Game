using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    //Classe que representa a moeda no jogo, Herda da CollisionBox
    public class Coin : CollisionBox
    {
        //Propriedade que armazena a textura da moeda
        public Texture2D Texture { get; private set; }

        //Construtor da classe Coin que inicializa a moeda com um retângulo e uma textura
        public Coin(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
        }

        //Método para renderizar a moeda no Ecra
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        //Método para definir uma nova textura para a moeda
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

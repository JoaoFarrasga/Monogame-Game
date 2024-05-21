using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    //Classe que representa uma porta no jogo, herdando de CollisionBox
    public class Door : CollisionBox
    {
        //Propriedade que armazena a textura da porta
        public Texture2D Texture { get; private set; }

        //Construtor da classe Door que inicializa a porta com um retângulo e uma textura
        public Door(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
        }

        //Método para desenhar a porta no ecrã
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);  //Desenha a porta com a cor branca
        }

        //Método para definir uma nova textura para a porta
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

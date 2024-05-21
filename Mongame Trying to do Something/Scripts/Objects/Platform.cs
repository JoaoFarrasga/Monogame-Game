using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    //Classe que representa uma plataforma no jogo, herdando de CollisionBox
    public class Platform : CollisionBox
    {
        //Propriedade que armazena a textura da plataforma
        public Texture2D Texture { get; private set; }

        //Construtor da classe Platform que inicializa a plataforma com um retângulo e uma textura
        public Platform(Rectangle rectangle, Texture2D texture)
            : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            Texture = texture;
        }

        //Método para desenhar a plataforma no ecrã
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);  //Desenha a plataforma com a cor branca
        }

        //Método para definir uma nova textura para a plataforma
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}

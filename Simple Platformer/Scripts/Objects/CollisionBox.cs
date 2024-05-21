using Microsoft.Xna.Framework;

namespace Mongame_Trying_to_do_Something.Scripts.Objects
{
    //Classe que representa uma caixa de colisão
    public class CollisionBox
    {
        //Propriedade que armazena o retângulo de colisão
        public Rectangle Rectangle { get; set; }

        //Propriedade que obtém e define a posição da caixa de colisão
        public Vector2 Position
        {
            get => new Vector2(Rectangle.X, Rectangle.Y);
            set
            {
                //Atualiza o retângulo com a nova posição
                Rectangle = new Rectangle((int)value.X, (int)value.Y, Rectangle.Width, Rectangle.Height);
            }
        }

        //Construtor da classe CollisionBox que inicializa a caixa de colisão com as coordenadas e dimensões fornecidas
        public CollisionBox(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
        }
    }
}

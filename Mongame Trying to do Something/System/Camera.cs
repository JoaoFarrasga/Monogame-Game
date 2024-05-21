using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mongame_Trying_to_do_Something.System
{
    //Classe que representa a câmara 2D do jogo
    public class Camera2D
    {
        //Campos privados
        private Vector2 position;
        private float zoom;
        private float rotation;
        private Viewport viewport;

        //Construtor da classe Camera2D que inicializa a câmara com um viewport
        public Camera2D(Viewport viewport)
        {
            this.viewport = viewport;
            position = Vector2.Zero;
            zoom = 1.0f;
            rotation = 0.0f;
        }

        //Propriedade para obter e definir a posição da câmara
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //Propriedade para obter e definir o zoom da câmara
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        //Propriedade para obter e definir a rotação da câmara
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        //Método que retorna a matriz de visualização da câmara
        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-position, 0.0f)) * //Translação inversa para centrar a câmara
                   Matrix.CreateRotationZ(rotation) * //Rotação em torno do eixo Z
                   Matrix.CreateScale(zoom) * //Escala para o zoom
                   Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0.0f)); //Translação para o centro do viewport
        }

        //Método que atualiza a posição da câmara com base na posição do jogador
        public void Update(Vector2 playerPosition)
        {
            position = new Vector2(playerPosition.X, playerPosition.Y); //Atualiza a posição da câmara para seguir o jogador
        }
    }
}

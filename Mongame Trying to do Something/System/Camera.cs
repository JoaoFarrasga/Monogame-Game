using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mongame_Trying_to_do_Something.System
{
    public class Camera2D
    {
        private Vector2 position;
        private float zoom;
        private float rotation;
        private Viewport viewport;

        public Camera2D(Viewport viewport)
        {
            this.viewport = viewport;
            position = Vector2.Zero;
            zoom = 1.0f;
            rotation = 0.0f;
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-position, 0.0f)) *
                   Matrix.CreateRotationZ(rotation) *
                   Matrix.CreateScale(zoom) *
                   Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0.0f));
        }

        public void Update(Vector2 playerPosition)
        {
            position = new Vector2(playerPosition.X, playerPosition.Y);
        }
    }
}

using Microsoft.Xna.Framework;

namespace RPG
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        float scale = 1f;
        public void Follow (Player target)
        {
            var position = Matrix.CreateTranslation(-target.Position.X - (target.Rectangle.Width / 2), 
            -target.Position.Y - (target.Rectangle.Height / 2), 0);

            var offset = Matrix.CreateTranslation(Game1.WIDTH / 2 / scale, Game1.HEIGHT / 2 / scale, 0);  //multiplied by 1.5f to account for the zoom scaling

            var zoom = Matrix.CreateScale(scale, scale, 0);  //zooms in the camera view by 1.5f to enlarge the image

            Transform =  position * offset * zoom;
        }
    }
}
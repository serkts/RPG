using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
 
namespace RPG
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double elapsedTime = 0;
 
        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }
 
        public void Update(GameTime gt)
        {
            elapsedTime += gt.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= 1000)
            {
                currentFrame ++;
                elapsedTime = 0;
            }
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }
 
        public void Draw(SpriteBatch sb, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
 
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            sb.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
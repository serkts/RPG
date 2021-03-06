using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class Bed
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Rectangle hitbox;
        public Rectangle Hitbox { get { return hitbox; } }

        public Bed(Vector2 p)
        {
            position = p;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bed");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            hitbox = new Rectangle(rectangle.X + 8, rectangle.Y + 16, 48, 102);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }
}
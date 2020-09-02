using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class Tree
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Rectangle hitbox;

        public Rectangle Hitbox { get { return hitbox; } }

        public Tree(Vector2 p)
        {
            position = p;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("tree");
            rectangle = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            hitbox = new Rectangle(rectangle.X + 32, rectangle.Y + 96, 32, 32);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, Color.White);
        }
    }
}
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
        float layer;

        public Rectangle Hitbox { get { return hitbox; } }
        public float Layer { get { return layer; } set { layer = value; } }

        public Tree(Vector2 p)
        {
            position = p;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("eleaf");
            rectangle = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            hitbox = new Rectangle(rectangle.X + 30, rectangle.Y + 96, 36, 32);
            layer = 0.1f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layer);
        }
    }
}
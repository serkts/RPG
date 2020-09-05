using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Bush
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Rectangle hitbox;
        float layer; 

        public Rectangle Hitbox { get { return hitbox; } }
        public float Layer { get { return layer; } set { layer = value; } }

        public Bush(Vector2 p)
        {
            position = p;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bush");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            hitbox = new Rectangle(rectangle.X, rectangle.Y + 18, rectangle.Width, rectangle.Height - 18);
            layer = 0.1f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layer);
        }
    }
}
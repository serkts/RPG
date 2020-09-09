using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class Chair
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        public Rectangle Hitbox { get { return rectangle; } }

        public Chair(Vector2 p)
        {
            position = p;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("chair");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }
}
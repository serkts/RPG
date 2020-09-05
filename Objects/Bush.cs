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

        public Bush(Vector2 p)
        {
            position = p;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bush");
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }
}
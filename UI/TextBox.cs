using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class Textbox
    {
        string text;
        Vector2 position;
        SpriteFont textFont;

        public Textbox(Vector2 p, string s)
        {
            text = s;
            position = p;
        }

        public void LoadContent(ContentManager content)
        {
            textFont = content.Load<SpriteFont>("textbox");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(textFont, text, position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
        }
    }
}
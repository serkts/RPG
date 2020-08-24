using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Tile
    {
        Texture2D texture;
        Vector2 pos;
        Rectangle rect;
        char type;

        public Tile(Vector2 p, char t)
        {
            pos = p;
            type = t;
        }

        public Rectangle Rect
        {
            get { return rect; }
        }

        public void LoadContent(ContentManager content)
        {
            switch (type)
            {
                case 'g':
                    texture = content.Load<Texture2D>("grass");
                    break;
                case 'w':
                    texture = content.Load<Texture2D>("water");
                    break;
                case 'b':
                    texture = content.Load<Texture2D>("brick");
                    break;
                case 'd':
                    texture = content.Load<Texture2D>("wall");
                    break;
                case 'f':
                    texture = content.Load<Texture2D>("wood");
                    break;
            }
            rect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }

        public char Type
        {
            get { return type; }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
    }
}
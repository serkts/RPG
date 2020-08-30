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
        AnimatedSprite animsprite;

        public Tile(Vector2 p, char t)
        {
            pos = p;
            type = t;  //the character determines what type of block it is.
        }

        public Rectangle Rect
        {
            get { return rect; }
        }

        public void LoadContent(ContentManager content)
        {
            //this is executed per tile to determine its type, as noted in the lvl1.cs file.
            switch (type)
            {
                case 'g':
                    texture = content.Load<Texture2D>("grass");
                    break;
                case 'w':
                    texture = content.Load<Texture2D>("water");
                    animsprite = new AnimatedSprite(texture, 1, 2);
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
            rect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);  //each tile's rectangle is static so it's only loaded once in LoadContent
        }

        public char Type
        {
            get { return type; }  //used to retreive type for special tile properties
        }

        public void Update (GameTime gt)
        {
            if (type == 'w')
                animsprite.Update(gt);
        }

        public void Draw(SpriteBatch sb)
        {
            if (type == 'w')
            {
                animsprite.Draw(sb, pos);
            }
            else 
                sb.Draw(texture, rect, Color.White);
        }
    }
}
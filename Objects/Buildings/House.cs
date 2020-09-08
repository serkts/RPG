using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class House
    {
        Vector2 position;
        int houseWidth;
        int houseHeight;
        int tileSize = 64;
        char side;
        Bed bed;

        Rectangle bedHitbox;
        public Rectangle BedHitbox { get { return bedHitbox; } }

        public House(Vector2 p, int w, int h, char s)
        {
            position = p;
            houseWidth = w * tileSize;
            houseHeight = h * tileSize;
            side = s;
        }

        public void LoadContent(ContentManager content)
        {
            if (side == 'r')
                bed = new Bed(new Vector2(position.X + houseWidth - (2 * tileSize), position.Y + tileSize));
            if (side == 'l')
                bed = new Bed(new Vector2(position.X + tileSize, position.Y + tileSize));
            bed.LoadContent(content);
            bedHitbox = bed.Hitbox;
        }

        public void Draw(SpriteBatch sb)
        {
            bed.Draw(sb);
        }
    }
}
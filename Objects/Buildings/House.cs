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
        Table table;
        Chair chair;

        Rectangle bedHitbox;
        Rectangle tableHitbox;
        Rectangle chairHitbox;
        public Rectangle BedHitbox { get { return bedHitbox; } }
        public Rectangle TableHitbox { get { return tableHitbox; } }
        public Rectangle ChairHitbox { get { return chairHitbox; } }

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
            {
                bed = new Bed(new Vector2(position.X + houseWidth - (2 * tileSize), position.Y + tileSize));
                table = new Table(new Vector2(position.X + 48 + tileSize, position.Y + tileSize + 32));
                chair = new Chair(new Vector2(position.X + 16 + tileSize, position.Y + tileSize + 32));
            }
            if (side == 'l')
            {
                bed = new Bed(new Vector2(position.X + tileSize, position.Y + tileSize));
                table = new Table(new Vector2(position.X + houseWidth - (2 * tileSize) - 16, position.Y + tileSize + 32));
                chair = new Chair(new Vector2(position.X + houseWidth - (2 * tileSize) - 48, position.Y + tileSize + 32));
            }

            bed.LoadContent(content);
            table.LoadContent(content);
            chair.LoadContent(content);

            bedHitbox = bed.Hitbox;
            tableHitbox = table.Hitbox;
            chairHitbox = chair.Hitbox;
        }

        public void Draw(SpriteBatch sb)
        {
            bed.Draw(sb);
            table.Draw(sb);
            chair.Draw(sb);
        }
    }
}
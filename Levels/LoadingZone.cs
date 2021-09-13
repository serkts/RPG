using Microsoft.Xna.Framework;

namespace RPG
{
    public class LoadingZone
    {
        const int tileSize = 64;

        int x;
        int y;
        int width;
        int height;
        Rectangle rectangle;

        public LoadingZone(int xCoordinate, int yCoordinate, int zoneWidth, int zoneHeight)
        {
            x = xCoordinate * tileSize;
            y = yCoordinate * tileSize;
            width = zoneWidth * tileSize;
            height = zoneHeight * tileSize;
            rectangle = new Rectangle(x, y, width, height);
        }

        public Rectangle Area { get { return rectangle; } }
        public int Y { get { return y; } }
        public int X { get { return x; } }
    }
}
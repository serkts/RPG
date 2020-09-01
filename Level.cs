using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Level
    {
        int blockX;
        int blockY;
        int tileSize;
        Tile[,] tiles;
        String filename;
        char[,] types;
        String[] line;
        Player player;
        Camera camera;

        public Level(String f)
        {
            filename = f;
        }

        public void Initialize()
        {
            tileSize = 64;  //pixel size of the tile (64 x 64)
            blockX = 50; //# of tiles on the map X
            blockY = 50; //# of tiles on the map Y
            tiles = new Tile[blockX, blockY];
            types = new char[blockY, blockX];
            player = new Player(new Vector2(5 * tileSize, 7 * tileSize));
            camera = new Camera();

        }
        public void LoadContent(ContentManager content)
        {
            //loading the map from a file, split by commas
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    for (int i = 0; i < blockY; i++)
                    {
                        line = reader.ReadLine().Split(',');
                        for (int j = 0; j < blockX; j++)
                            types[i, j] = Convert.ToChar(line[j]);
                    }
                }
            }
            //initializes each tile as per the level file
            for (int y = 0; y < blockY; y++)
                for (int x = 0; x < blockX; x++)
                    tiles[x, y] = new Tile(new Vector2(x * tileSize, y * tileSize), types[y, x]);
            foreach (Tile tile in tiles)
                tile.LoadContent(content);
            player.LoadContent(content);
        }

        public void Update(GameTime gt)
        {
            player.Update(gt);
            camera.Follow(player);
            foreach (Tile tile in tiles)
            {
                tile.Update(gt);
                if (tile.Collision)
                {
                    if (collidedRight(player.Hitbox, tile.Rect))
                    {
                        player.CollidedRight = true;
                    }
                    if (collidedLeft(player.Hitbox, tile.Rect))
                    {
                        player.CollidedLeft = true;
                    }
                    if (collidedUp(player.Hitbox, tile.Rect))
                    {
                        player.CollidedUp = true;
                    }
                    if (collidedDown(player.Hitbox, tile.Rect))
                    {
                        player.CollidedDown = true;
                    }
                }
            }
            if (player.Position.X <= 0)
                player.CollidedLeft = true;
            if (player.Hitbox.Right >= tileSize * blockX)
                player.CollidedRight = true;
            if (player.Hitbox.Top <= 0)
                player.CollidedUp = true;
            if (player.Hitbox.Bottom >= tileSize * blockY)
                player.CollidedDown = true;
        }

        protected bool collidedRight(Rectangle hitbox, Rectangle rectangle)
        {
            return hitbox.Right >= rectangle.Left && hitbox.Right <= rectangle.Right && hitbox.Center.Y >= rectangle.Top && hitbox.Center.Y  <= rectangle.Bottom;
        }
        protected bool collidedLeft(Rectangle hitbox, Rectangle rectangle)
        {
            return hitbox.Left <= rectangle.Right && hitbox.Left >= rectangle.Left && hitbox.Center.Y >= rectangle.Top && hitbox.Center.Y  <= rectangle.Bottom;        
        }
        protected bool collidedUp(Rectangle hitbox, Rectangle rectangle)
        {
            return hitbox.Top <= rectangle.Bottom && hitbox.Top >= rectangle.Top && hitbox.Center.X >= rectangle.Left && hitbox.Center.X <= rectangle.Right;
        }
        protected bool collidedDown(Rectangle hitbox, Rectangle rectangle)
        {
            return hitbox.Bottom >= rectangle.Top && hitbox.Bottom <= rectangle.Bottom && hitbox.Center.X >= rectangle.Left && hitbox.Center.X <= rectangle.Right;
        }

        public void Draw(SpriteBatch sb)
        {
            //transform matrix used to account for the camera in the level
            sb.Begin(transformMatrix: camera.Transform);
            foreach (Tile tile in tiles)
                tile.Draw(sb);
            player.Draw(sb);
            sb.End();
        }
    }
}
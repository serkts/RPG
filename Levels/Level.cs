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
        SpriteFont spriteFont;

        TreeLoader treeloader;
        Rectangle[] treeboxes;
        BushLoader bushLoader;
        Rectangle[] bushboxes;
        int treeCount = 0;
        int bushCount = 0;

        House house1;
        House house2;

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
            treeloader = new TreeLoader(1);
            bushLoader = new BushLoader(1);
            house1 = new House(new Vector2(2 * tileSize, tileSize), 7, 5, 'r');
            house2 = new House(new Vector2(31 * tileSize, tileSize), 7, 5, 'l');
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
            treeloader.LoadContent(content);
            treeboxes = treeloader.Hitboxes;
            bushLoader.LoadContent(content);
            bushboxes = bushLoader.Hitboxes;
            spriteFont = content.Load<SpriteFont>("position");
            house1.LoadContent(content);
            house2.LoadContent(content);
        }

        public void Update(GameTime gt)
        {
            player.Update(gt);
            camera.Follow(player);
            foreach (Tile tile in tiles)
            {
                tile.Update(gt);
                if (tile.Collision)
                    Collided(player.Hitbox, tile.Rect);
            }
            if (player.Position.X <= 0)
                player.CollidedLeft = true;
            if (player.Hitbox.Right >= tileSize * blockX)
                player.CollidedRight = true;
            if (player.Hitbox.Top <= 0)
                player.CollidedUp = true;
            if (player.Hitbox.Bottom >= tileSize * blockY)
                player.CollidedDown = true;

            foreach (Rectangle treebox in treeboxes)
            {
                Collided(player.Hitbox, treebox);
                if (player.Hitbox.Bottom >= treebox.Bottom)
                    treeloader.Zorder(treeCount, 0.1f);
                else
                    treeloader.Zorder(treeCount, 0.3f);
                treeCount++;
            }
            foreach (Rectangle bushbox in bushboxes)
            {
                Collided(player.Hitbox, bushbox);
                if (player.Hitbox.Bottom >= bushbox.Bottom)
                    bushLoader.Zorder(bushCount, 0.1f);
                else
                    bushLoader.Zorder(bushCount, 0.3f);
                bushCount++;
            }
            Collided(player.Hitbox, house1.BedHitbox);
            Collided(player.Hitbox, house2.BedHitbox);
            Collided(player.Hitbox, house1.TableHitbox);
            Collided(player.Hitbox, house2.TableHitbox);
            Collided(player.Hitbox, house1.ChairHitbox);
            Collided(player.Hitbox, house2.ChairHitbox);

            if (treeCount == 20)
                treeCount = 0;
            if (bushCount == 20)
                bushCount = 0;
        }

        void Collided(Rectangle r1, Rectangle r2)
        {
            if (collidedRight(r1, r2))
                player.CollidedRight = true;
            if (collidedLeft(r1, r2))
                player.CollidedLeft = true;
            if (collidedUp(r1, r2))
                player.CollidedUp = true;
            if (collidedDown(r1, r2))
                player.CollidedDown = true;
        }

        protected bool collidedRight(Rectangle hitbox, Rectangle rectangle)
        {
            return hitbox.Right >= rectangle.Left && hitbox.Right <= rectangle.Right && hitbox.Center.Y >= rectangle.Top && hitbox.Center.Y <= rectangle.Bottom;
        }
        protected bool collidedLeft(Rectangle hitbox, Rectangle rectangle)
        {
            return hitbox.Left <= rectangle.Right && hitbox.Left >= rectangle.Left && hitbox.Center.Y >= rectangle.Top && hitbox.Center.Y <= rectangle.Bottom;        
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
            sb.Begin(SpriteSortMode.FrontToBack, transformMatrix: camera.Transform);
            foreach (Tile tile in tiles)
                tile.Draw(sb);
            player.Draw(sb);
            treeloader.Draw(sb);
            bushLoader.Draw(sb);
            sb.DrawString(spriteFont, "x: " + (int)player.Position.X / tileSize + "\ny: " + (int)player.Position.Y / tileSize, new Vector2(player.Position.X - Game1.WIDTH / 3.2f, player.Position.Y - Game1.HEIGHT / 3.5f), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
            house1.Draw(sb);
            house2.Draw(sb);
            sb.End();
        }
    }
}
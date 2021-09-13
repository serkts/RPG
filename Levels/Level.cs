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
        const int tileSize = 64;

        Tile[,] tiles;

        String tileMap;
        String levelSettings;

        char[,] types;
        String[] line;
        Player player;
        //Npc npc1;
        //Textbox npcText;
        //bool drawText;

        Camera camera;
        SpriteFont spriteFont;
        //float framerate;

        TreeLoader treeloader;
        Rectangle[] treeboxes;
        BushLoader bushLoader;
        Rectangle[] bushboxes;
        int treeCount = 0;
        int maxTrees;
        int bushCount = 0;
        int maxBushes;

        public Level(String map, String settings)
        {
            tileMap = map;
            levelSettings = settings;
        }

        public string Map { set { tileMap = value; } }
        public string Settings { set { levelSettings = value; } }

        public Rectangle PlayerHitbox { get { return player.Hitbox; } }
        public Vector2 PlayerPosition { set { player.Position = value; } }
        
        public void Initialize()
        {
            player = new Player(new Vector2(5 * tileSize, 7 * tileSize));
        }
        
        public void LoadContent(ContentManager content)
        {
            treeCount = 0;
            bushCount = 0;
            using (StreamReader reader = new StreamReader(levelSettings))
            {
                string strMapWidth = reader.ReadLine();
                blockX = int.Parse(strMapWidth.Substring(strMapWidth.IndexOf("=") + 1));
                string strMapHeight = reader.ReadLine();
                blockY = int.Parse(strMapHeight.Substring(strMapHeight.IndexOf("=") + 1));
                string strMaxTrees = reader.ReadLine();
                maxTrees = int.Parse(strMaxTrees.Substring(strMaxTrees.IndexOf("=") + 1));
                string strMaxBushes = reader.ReadLine();
                maxBushes = int.Parse(strMaxBushes.Substring(strMaxBushes.IndexOf("=") + 1));
            }
            tiles = new Tile[blockX, blockY];
            types = new char[blockY, blockX];
            camera = new Camera();
            treeloader = new TreeLoader(levelSettings);
            bushLoader = new BushLoader(levelSettings);
            using (StreamReader reader = new StreamReader(tileMap))
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

            /*
            if ((levelState) == LevelState.Town)
            {
                npc1.LoadContent(content);
                npcText.LoadContent(content);
            }
            */
        }
        public int MapWidth { get { return blockX; } }
        public int MapHeight { get { return blockY; } }

        public void Update(GameTime gt)
        {
            if (treeCount >= maxTrees)
                treeCount = 0;
            if (bushCount >= maxBushes)
                bushCount = 0;
            player.Update(gt);
            //npc1.Update(gt);
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
            /*
            Collided(player.Hitbox, npc1.Rectangle);
            if (player.Hitbox.Bottom >= npc1.Rectangle.Bottom)
                    npc1.Zorder(0.1f);
                else
                    npc1.Zorder(0.3f);

            //textbox
            if (collidedRight(player.Hitbox, npc1.Rectangle) || collidedLeft(player.Hitbox, npc1.Rectangle) || collidedUp(player.Hitbox, npc1.Rectangle))
            {
                drawText = true;
            } else drawText = false;
            */           
            
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
            sb.DrawString(spriteFont, "x: " + (int)player.Position.X / tileSize + "\ny: " + (int)player.Position.Y / tileSize, new Vector2(player.Position.X - Game1.WIDTH / 2.1f, player.Position.Y - Game1.HEIGHT / 2.3f), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
            //sb.DrawString(spriteFont, "fps: " + framerate, new Vector2(player.Position.X - Game1.WIDTH / 2.1f, player.Position.Y - Game1.HEIGHT / 2.6f), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
            /*if (levelState == LevelState.Town)
            {
                npc1.Draw(sb);
                if (drawText)
                    npcText.Draw(sb);
            }*/
            sb.End();
        }
    }
}
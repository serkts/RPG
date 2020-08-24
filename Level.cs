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

        public Level(String f)
        {
            filename = f;
        }

        public void Initialize()
        {
            tileSize = 64;
            blockX = 50;
            blockY = 50;
            tiles = new Tile[blockX, blockY];
            types = new char[blockY, blockX];

        }
        public void LoadContent(ContentManager content)
        {

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
            for (int y = 0; y < blockY; y++)
                for (int x = 0; x < blockX; x++)
                    tiles[x, y] = new Tile(new Vector2(x * tileSize, y * tileSize), types[y, x]);
            foreach (Tile tile in tiles)
                tile.LoadContent(content);
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Tile tile in tiles)
                tile.Draw(sb);
        }
    }
}
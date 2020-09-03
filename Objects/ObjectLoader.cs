using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class ObjectLoader
    {
        int tileSize;
        int blockX;
        int blockY;
        Tree[] trees;
        Random random;

        public ObjectLoader()
        {
            tileSize = 64;
            blockX = 50;
            blockY = 50;
            trees = new Tree[20];
            random = new Random();
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < trees.Length; i++)
                trees[i] = new Tree(new Vector2(random.Next(blockX*tileSize), random.Next(blockY*tileSize)));

            foreach (Tree tree in trees)
                tree.LoadContent(content);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Tree tree in trees)
                tree.Draw(sb);
        }
    }
}
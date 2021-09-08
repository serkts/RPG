using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class TreeLoader
    {
        const int tileSize = 64;
        string level;
        int count = 0;
        Tree[] trees;
        float[] xCoordinates;
        float[] yCoordinates;
        Rectangle[] hitboxes;
        public Rectangle[] Hitboxes { get { return hitboxes; } }


        public TreeLoader(string lvl)
        {
            level = lvl;
        }

        public void LoadContent(ContentManager content)
        {
            switch (level)
            {
                case ("Levels/LevelFiles/town.lvl"):
                {
                    ReadData("Levels/LevelFiles/townTrees.csv");
                    break;
                }
            }

            foreach (Tree tree in trees)
            {
                tree.LoadContent(content);
                hitboxes[count] = tree.Hitbox;
                count++;
            }
        }

        public void Zorder(int c, float l)
        {
            trees[c].Layer = l;
        }

        private void ReadData(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string[] xLine = reader.ReadLine().Split(',');
                xCoordinates = new float[xLine.Length];
                for (int i = 0; i < xLine.Length; i++)
                    xCoordinates[i] = float.Parse(xLine[i]);
                string[] yLine = reader.ReadLine().Split(',');
                yCoordinates = new float[yLine.Length];
                for (int i = 0; i < yLine.Length; i++)
                    yCoordinates[i] = float.Parse(yLine[i]);
            }
            trees = new Tree[xCoordinates.Length];
            for (int i = 0; i < xCoordinates.Length; i++)
                trees[i] = new Tree(new Vector2(xCoordinates[i] * tileSize, yCoordinates[i] * tileSize));
            hitboxes = new Rectangle[xCoordinates.Length];
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Tree tree in trees)
                tree.Draw(sb);
        }
    }
}
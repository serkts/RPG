using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class TreeLoader
    {
        int tileSize;
        int level;
        int count = 0;
        Tree[] trees;
        float[] lvl1X = new float[20]{11, 14, 20, 25, 29,  5,  6, 37.5f, 38, 39, 29, 45,  8, 11, 12, 19, 28, 18, 31, 43};
        float[] lvl1Y = new float[20]{ 2,  4,  2,  4,  2, 14, 32, 14.5f, 36, 28, 36, 46, 37, 15, 45, 47, 47, 19, 15, 14};
        Rectangle[] hitboxes = new Rectangle[20];
        public Rectangle[] Hitboxes { get { return hitboxes; } }


        public TreeLoader(int lvl)
        {
            level = lvl;
            tileSize = 64;
            trees = new Tree[20];
        }

        public void LoadContent(ContentManager content)
        {
            if (level == 1)
            {
                for (int i = 0; i < lvl1X.Length; i++)
                    trees[i] = new Tree(new Vector2(lvl1X[i] * tileSize, lvl1Y[i] * tileSize));
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

        public void Draw(SpriteBatch sb)
        {
            foreach (Tree tree in trees)
                tree.Draw(sb);
        }
    }
}
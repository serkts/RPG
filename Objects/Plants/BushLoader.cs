using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class BushLoader
    {
        int tileSize;
        int level;
        int count = 0;
        Bush[] bushes;
        float[] lvl1X = new float[20]{11, 14, 21, 25, 28,  3,  6, 37, 32, 46, 32, 41, 11, 13, 19, 18, 31, 16.5f, 31, 43};
        float[] lvl1Y = new float[20]{ 4,  2,  5,  2,  4, 18, 35, 10, 28, 39, 36, 45, 18, 27, 37, 45, 45,    21, 19, 20};
        Rectangle[] hitboxes = new Rectangle[20];
        public Rectangle[] Hitboxes { get { return hitboxes; } }


        public BushLoader(int lvl)
        {
            level = lvl;
            tileSize = 64;
            bushes = new Bush[20];
        }

        public void LoadContent(ContentManager content)
        {
            if (level == 1)
            {
                for (int i = 0; i < lvl1X.Length; i++)
                    bushes[i] = new Bush(new Vector2(lvl1X[i] * tileSize, lvl1Y[i] * tileSize));
            }

            foreach (Bush bush in bushes)
            {
                bush.LoadContent(content);
                hitboxes[count] = bush.Hitbox;
                count++;
            }
        }

        public void Zorder(int c, float l)
        {
            bushes[c].Layer = l;
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Bush bush in bushes)
                bush.Draw(sb);
        }
    }
}
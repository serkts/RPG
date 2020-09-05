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
        float[] lvl1X = new float[20]{11, 14, 20, 25, 29, 3, 6, 37.5f, 32, 39, 29, 45, 8, 6, 12, 19, 42, 18, 31, 38};
        float[] lvl1Y = new float[20]{2, 4, 2, 4, 2, 16, 32, 14.5f, 20, 28, 36, 46, 37, 29, 45, 47, 47, 19, 15, 21};
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
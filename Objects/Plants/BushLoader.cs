using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    public class BushLoader
    {
        const int tileSize = 64;
        string level;
        int count = 0;
        Bush[] bushes;
        float[] xCoordinates;
        float[] yCoordinates;
        Rectangle[] hitboxes;
        public Rectangle[] Hitboxes { get { return hitboxes; } }


        public BushLoader(string lvl)
        {
            level = lvl;
        }

        public void LoadContent(ContentManager content)
        {
            switch (level)
            {
                case ("Content/LevelFiles/Town/town.settings"):
                {
                    ReadData("Content/LevelFiles/Town/town.bush");
                    break;
                }
                case ("Content/LevelFiles/Forest/forest.settings"):
                {
                    ReadData("Content/LevelFiles/Forest/forest.bush");
                    break;
                }
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
            bushes = new Bush[xCoordinates.Length];
            for (int i = 0; i < xCoordinates.Length; i++)
                bushes[i] = new Bush(new Vector2(xCoordinates[i] * tileSize, yCoordinates[i] * tileSize));
            hitboxes = new Rectangle[xCoordinates.Length];
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Bush bush in bushes)
                bush.Draw(sb);
        }
    }
}
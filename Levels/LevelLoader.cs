using System;
using System.IO;
using Microsoft.Xna.Framework.Content;


namespace RPG
{
    public class LevelLoader
    {
        string strMapHeight;
        string strMapWidth;
        string strTreeCount;
        string strBushCount;

        int mapHeight;
        int mapWidth;
        int treeCount;
        int bushCount;

        public LevelLoader()
        {

        }

        public void LoadContent(ContentManager content)
        {
            using (StreamReader reader = new StreamReader("Levels/LevelFiles/town.lvl"))
            {
                strMapWidth = reader.ReadLine();
                strMapHeight = reader.ReadLine();
                strTreeCount = reader.ReadLine();
                strBushCount = reader.ReadLine();
            }

            mapWidth = Int32.Parse(strMapWidth.Substring(strMapWidth.IndexOf("=") + 1));
            mapHeight = Int32.Parse(strMapHeight.Substring(strMapHeight.IndexOf("=") + 1));
            treeCount = Int32.Parse(strTreeCount.Substring(strTreeCount.IndexOf("=") + 1));
            bushCount = Int32.Parse(strBushCount.Substring(strBushCount.IndexOf("=") + 1));

            Console.WriteLine(mapWidth);
            Console.WriteLine(mapHeight);
            Console.WriteLine(treeCount);
            Console.WriteLine(bushCount);
        }

        
    }
}
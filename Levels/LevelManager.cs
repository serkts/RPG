using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG
{
    enum LevelState
    {
        Town, Forest
    }
    public class LevelManager
    {
        const int tileSize = 64;
        Level level;
        ContentManager content;

        LoadingZone zone1;

        public LevelManager(ContentManager c)
        {
            content = c;
        }

        public void Initialize()
        {
            level = new Level("Content/LevelFiles/Town/town.csv", "Content/LevelFiles/Town/town.lvl");
            level.Initialize();
            zone1 = new LoadingZone(0, 7, 1, 2);
        }
        
        public void LoadContent()
        {
            level.LoadContent(content);
        }

        private void ChangeLevel(string levelMap, string levelSettings)
        {
            level.Map = levelMap;
            level.Settings = levelSettings;
            level.LoadContent(content);
            level.PlayerPosition = new Vector2(level.MapWidth * tileSize - tileSize, zone1.Y);
        }

        public void Update(GameTime gameTime)
        {
            level.Update(gameTime);
            if (level.PlayerHitbox.Intersects(zone1.Area))
            {
                ChangeLevel("Content/LevelFiles/Forest/forest.csv", "Content/LevelFiles/Forest/forest.lvl");
            }

        }

        public void Draw(SpriteBatch sb)
        {
            level.Draw(sb);
        }
    }
}
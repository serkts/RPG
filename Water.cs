using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    class Water
    {
        Texture2D texture;
        double timeSinceLast = 0;
        Rectangle f1 = new Rectangle(0, 0, 32, 32);
        Rectangle f2 = new Rectangle(32, 0, 32, 32);
        Rectangle current;
        
        public Water()
        {
            current = f1;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("water");
        }
        public void Update(GameTime gt)
        {
            
        }
        public void Draw(SpriteBatch sb)
        {
            
        }
    }
}
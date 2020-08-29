using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    class Water
    {
        Texture2D texture;
        AnimatedSprite animsprite; 



        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("water");
            animsprite = new AnimatedSprite(texture, 1, 2);
        }
        public void Update(GameTime gt)
        {
            animsprite.Update(gt);
        }
        public void Draw(SpriteBatch sb)
        {
            animsprite.Draw(sb, new Vector2(400,400));
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Npc
    {
        const int npcWidth = 48;
        const int npcHeight = 96;

        Texture2D texture;
        Vector2 pos;
        Rectangle rect;
        float layer = 0.1f;

        public Vector2 Position { get { return pos; } set { pos = value;} }
        public Rectangle Rectangle { get { return rect; } }

        public Npc (Vector2 p)
        {
            pos = p;
        }

        public void Zorder(float l)
        {
            layer = l;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("npc1");
        }

        public void Update(GameTime gameTime)
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, npcWidth, npcHeight);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
        }
    }
}
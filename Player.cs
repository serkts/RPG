using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Player
    {
        Texture2D texture;
        Texture2D up;
        Texture2D down;
        Texture2D left;
        Texture2D right;
        Vector2 pos;
        Rectangle rect;
        float speed;
        KeyboardState ks;

        public Vector2 Position { get { return pos; } }
        public Rectangle Rectangle { get {return rect;} }

        public Player(Vector2 p)
        {
            pos = p;
            speed = 3f;
        }

        public void LoadContent(ContentManager content)
        {
            //inefficient way to determine which texture to draw, so this is temporary
            texture = content.Load<Texture2D>("player");
            up = content.Load<Texture2D>("playerup");
            down = content.Load<Texture2D>("player");
            left = content.Load<Texture2D>("playerleft");
            right = content.Load<Texture2D>("playerright");
        }

        public void Update(GameTime gt)
        {
            var delta = 60 * (float)gt.ElapsedGameTime.TotalSeconds;  //delta is used to keep everything consisten around the 60fps mark

            ks = Keyboard.GetState();

            //movement keyinputs
            if(ks.IsKeyDown(Keys.Up))
            {
                pos.Y -= speed * delta;
                texture = up;
            }
            if(ks.IsKeyDown(Keys.Down))
            {
                pos.Y += speed * delta;
                texture = down;
            }
            if(ks.IsKeyDown(Keys.Left))
            {
                pos.X -= speed * delta;
                texture = left;
            }
            if(ks.IsKeyDown(Keys.Right))
            {
                pos.X += speed * delta;
                texture = right;
            }

            //sprinting
            if (ks.IsKeyDown(Keys.LeftShift))
                speed = 5f;
            else speed = 3f;

            rect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
    }
}
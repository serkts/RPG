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
        Rectangle hitbox;
        float speed;
        KeyboardState ks;

        bool collidedRight;
        bool collidedLeft;
        bool collidedUp;
        bool collidedDown;

        public Vector2 Position { get { return pos; } set { pos = value; } }
        public Rectangle Rectangle { get { return rect; } }
        public Rectangle Hitbox { get { return hitbox; } }
        
        public bool CollidedRight { set { collidedRight = value; } }
        public bool CollidedLeft { set { collidedLeft = value; } }
        public bool CollidedUp { set { collidedUp = value; } }
        public bool CollidedDown { set { collidedDown = value; } }


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

            Move(delta);

            rect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
            hitbox = new Rectangle((int)pos.X, (int)pos.Y + texture.Height / 2, texture.Width, texture.Height / 2);
        }

        public void Move(float delta)
        {
            ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.Up))
            {
                if (collidedUp)
                {
                    pos.Y += 0;
                    collidedUp = false;
                }
                else 
                {
                    pos.Y -= speed * delta;
                    texture = up;
                }
            }
            if(ks.IsKeyDown(Keys.Down))
            {
                if (collidedDown)
                {
                    pos.Y -= 0;
                    collidedDown = false;
                }
                else 
                {
                    pos.Y += speed * delta;
                    texture = down;
                }
            }
            if(ks.IsKeyDown(Keys.Left))
            {
                if (collidedLeft)
                {
                    pos.X += 0;
                    collidedLeft = false;
                }
                else 
                {
                    pos.X -= speed * delta;
                    texture = left;
                }
            }
            if(ks.IsKeyDown(Keys.Right))
            {
                if (collidedRight)
                {
                    pos.X -= 0;
                    collidedRight = false;
                }
                else 
                {
                    pos.X += speed * delta;
                    texture = right;
                }
            }

            //sprinting
            if (ks.IsKeyDown(Keys.LeftShift))
                speed = 5f;
            else speed = 3f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, Color.White);
        }
    }
}
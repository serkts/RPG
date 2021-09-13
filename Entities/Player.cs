using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Player
    {
        const int playerWidth = 48;
        const int playerHeight = 96;

        Texture2D texture;
        Vector2 pos;
        Rectangle rect;
        Rectangle hitbox;
        Rectangle sourceRect;  //used to determine which cell of the spritesheet to draw
        float speed;
        KeyboardState ks;

        bool collidedRight;
        bool collidedLeft;
        bool collidedUp;
        bool collidedDown;

        public Vector2 Position { get { return pos; } set { pos.X = value.X; pos.Y = value.Y; } }

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
            texture = content.Load<Texture2D>("playerSheet");
            sourceRect = new Rectangle(0, 0, playerWidth, playerHeight);  //sets the source rectangle to the first cell of the spritesheet
        }

        public void Update(GameTime gt)
        {
            var delta = 60 * (float)gt.ElapsedGameTime.TotalSeconds;  //delta is used to keep everything consistent around the 60fps mark

            Move(delta);

            rect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width / 4, texture.Height);  //width divided by the number of cells for the sprite
            hitbox = new Rectangle((int)pos.X, (int)pos.Y + texture.Height / 2, texture.Width / 4, texture.Height / 2);

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
                    sourceRect.X = 3 * playerWidth; //number of cell (0-3) multiplied by cell width
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
                    sourceRect.X = 0;
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
                    sourceRect.X = playerWidth;
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
                    sourceRect.X = 2 * playerWidth;
                }
            }

            //sprinting
            if (ks.IsKeyDown(Keys.LeftShift))
                speed = 5f;
            else speed = 3f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rect, sourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.2f);
        }
    }
}
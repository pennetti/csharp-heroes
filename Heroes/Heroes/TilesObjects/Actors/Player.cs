using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class Player : Actor
    {
        public List<Enemy> prisoners;
        public Player(Point location, Texture2D texture, int health, int attack, int defense)
            : base(location, texture, health, attack, defense)
        {

            Initialize();
        }

        public void Initialize()
        {
            prisoners = new List<Enemy>();
            //Players start with 3 health
            _health = 3;
            base.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
        /*public void receiveUpdate(String message, Object data)
        {
            switch (message)
            {
                case "MOVE":
                    break;

                default:
                    break;
                    
            }
        }*/
    }
}

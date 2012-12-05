using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    class Player : Actor
    {
        public List<Enemy> prisoners;
        public Player(Game game, Point location, Texture2D texture, int health, int attack, int defense)
            : base(game, location, texture, health, attack, defense)
        {
            Initialize();
        }

        public override void Initialize()
        {
            prisoners = new List<Enemy>();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
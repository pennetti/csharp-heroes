using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class Weapon : TileObject
    {
        public int attack { get; set; }
        public int defense { get; set; }
        public int price { get; set; }

        public Weapon(Point location, Texture2D texture, int attack, int defense, int price) : base(location, texture)
        {
            this.attack = attack;
            this.defense = defense;
            this.price = price;
            this.Initialize();
        }

        public void Initialize()
        {
            base.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void receiveUpdate(Constants.GAME_UPDATE message, Object data)
        {
            switch (message)
            {
                case Constants.GAME_UPDATE.Capture:
                    break;

                default:
                    break;

            }
        }
    }
}

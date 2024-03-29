using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heroes
{
    public class Potion : TileObject
    {
        public Potion(Point location, Texture2D texture) : base(location, texture)
        {
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

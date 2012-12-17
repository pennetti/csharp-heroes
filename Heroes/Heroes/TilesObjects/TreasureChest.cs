using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heroes
{
    public class TreasureChest: TileObject
    {
        public bool _isBossChest { get; set; }

        public TreasureChest(Point location, Texture2D texture) 
            : base(location, texture)
        {
            this.Initialize();
        }

        public void Initialize()
        {
            this._isBossChest = false;
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

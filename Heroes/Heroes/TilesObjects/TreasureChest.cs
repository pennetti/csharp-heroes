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

        public TreasureChest(Game game, Point location, Texture2D texture) 
            : base(game, location, texture)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this._isBossChest = false;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

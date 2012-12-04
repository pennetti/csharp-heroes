using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heroes
{
    public class Key : TileObject
    {
        public bool isBossKey { get; set; }
        public Doorway door { get; private set; }

        public Key(Game game, Point location, Texture2D texture, Doorway door) : base(game, location, texture)
        {
            this.door = door;
            this.Initialize();
        }

        public override void Initialize()
        {
            this.isBossKey = false;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}

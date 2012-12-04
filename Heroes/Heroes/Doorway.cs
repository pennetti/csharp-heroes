using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heroes
{
    public class Doorway : TileObject
    {
        public bool isUnlocked { get; private set; }

        public Doorway(Game game, Point location, Texture2D texture) : base(game, location, texture)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this.isUnlocked = false;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void unlock()
        {
            this.isUnlocked = true;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    abstract class Actor : TileObject
    {
        public int _health;
        public int _attack;
        public int _defense;

        public Actor(Game game, Point location, Texture2D texture, int health, int attack, int defense)
            : base(game, location, texture)
        {
            _health = health;
            _attack = attack;
            _defense = defense;
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

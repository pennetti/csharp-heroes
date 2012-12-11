using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public abstract class Actor : TileObject
    {
        public int _health;
        public int _attack;
        public int _defense;

        public Actor(Point location, Texture2D texture, int health, int attack, int defense)
            : base(location, texture)
        {
            _health = health;
            _attack = attack;
            _defense = defense;
            Initialize();
        }

        public void Initialize()
        {
            base.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

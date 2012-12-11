using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class Enemy : Actor
    {
        public bool _isLastBoss;
        public Enemy(Point location, Texture2D texture, int health, int attack, int defense, Boolean isLastBoss)
            : base(location, texture, health, attack, defense)
        {
            _isLastBoss = isLastBoss;
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

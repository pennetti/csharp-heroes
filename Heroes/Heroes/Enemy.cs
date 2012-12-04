using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    class Enemy : Actor
    {
        public bool _isLastBoss;
        public Enemy(Game game, Point location, Texture2D texture, int health, int attack, int defense, Boolean isLastBoss)
            : base(game, location, texture, health, attack, defense)
        {
            _isLastBoss = isLastBoss;
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

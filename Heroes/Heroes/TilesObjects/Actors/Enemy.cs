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
            _health = 1;
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
                    Tuple<TileObject, TileObject> bundle = data as Tuple<TileObject, TileObject>;
                    if (bundle._item2.Equals(this))
                    {
                        _location = new Point();
                        _health = 0;
                        _current = null;
                    }
                    break;

                default:
                    break;

            }
        }
    }
}

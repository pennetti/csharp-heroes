using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class Player : Actor
    {
        public List<Enemy> prisoners;
        public List<TileObject> collectedItems;
        public Player(Point location, Texture2D texture, int health, int attack, int defense)
            : base(location, texture, health, attack, defense)
        {

            Initialize();
        }

        public void Initialize()
        {
            prisoners = new List<Enemy>();
            //Players start with 3 health
            _health = 3;
            collectedItems = new List<TileObject>();
            base.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void receiveUpdate(Constants.GAME_UPDATE message, Object data)
        {
            switch (message)
            {
                case Constants.GAME_UPDATE.Capture:
                    Tuple<TileObject, TileObject> bundle = data as Tuple<TileObject, TileObject>;
                    if (bundle._item1.GetType() == typeof(Player) && bundle._item1.Equals(this))
                    {
                        if (bundle._item2.GetType() == typeof(Enemy))
                        {
                            prisoners.Add((Enemy)bundle._item2);
                        }
                        else
                        {
                            collectedItems.Add(bundle._item2);
                        }
                    }
                    break;

                default:
                    break;
                    
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Heroes
{
    public class TileObject : Observer
    {
        public Texture2D _texture { get; set; }
        public Point _location { get; set; }
        public Tile _current { get; set; }

        public TileObject(Point location, Texture2D texture)
        {
            _texture = texture;
            _location = location;
            Initialize();
        }

        public void Initialize()
        {
            //base.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public void receiveUpdate(object message, object data)
        {
            //Do nothing
        }
    }
}

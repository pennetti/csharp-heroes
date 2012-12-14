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
    public class Tile
    {
        public Texture2D _texture { get; set; }
        public Point _location { get; set; }
        public TileObject _tileObject { get; set; }
        public bool _active { get; set; }
        public bool _isBattleTile;
        //Initialize
        public Tile _top { get; set; }
        public Tile _bottom { get; set; }
        public Tile _left { get; set; }
        public Tile _right { get; set; }

        public Tile(Point location)
        {
            _location = location;
            Initialize();
        }

        public Tile(Point location, Texture2D texture)
        {
            _texture = texture;
            _location = location;
            Initialize();
        }

        public void Initialize()
        {
            _active = false;
            _isBattleTile = false;
            _top = null;
            _bottom = null;
            _left = null;
            _right = null;
        }

        public void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public Boolean HasTileObject()
        {
            return _tileObject != null;
        }
    }
}

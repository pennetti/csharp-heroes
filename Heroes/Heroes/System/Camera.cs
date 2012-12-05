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
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Vector2 _cameraPosition;
        public Point _location { get; set; }
        public TileMap _tileMap { get; set; }

        public Camera(Game game, Point location, TileMap tileMap)
            : base(game)
        {
            _location = location;
            _tileMap = tileMap;
            Initialize();
        }

        public override void Initialize()
        {
            _cameraPosition = new Vector2(_location.X, _location.Y);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void MoveCamera(Vector2 cameraDirection)
        {
            _cameraPosition += cameraDirection;

            if (_cameraPosition.X < Constants.MARGIN_LEFT)
                _cameraPosition.X = Constants.MARGIN_LEFT;
            if (_cameraPosition.Y < Constants.MARGIN_TOP)
                _cameraPosition.Y = Constants.MARGIN_TOP;
            if (_cameraPosition.X > (_tileMap.MapWidth - Constants.TILES_WIDE) * Constants.TILE_WIDTH * Constants.MARGIN_RIGHT)
                _cameraPosition.X = (_tileMap.MapWidth - Constants.TILES_WIDE) * Constants.TILE_WIDTH + 25;
            if (_cameraPosition.Y > (_tileMap.MapHeight - Constants.TILES_HIGH) * Constants.TILE_HEIGHT - Constants.MARGIN_BOTTOM)
                _cameraPosition.Y = (_tileMap.MapHeight - Constants.TILES_HIGH) * Constants.TILE_HEIGHT - Constants.MARGIN_BOTTOM;
        }
    }
}

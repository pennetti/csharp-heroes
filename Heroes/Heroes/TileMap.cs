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
    public class TileMap : Microsoft.Xna.Framework.GameComponent
    {
        public Camera _camera { get; set; }

        public Point _startLocation { get; private set; }

        public List<Player> _players { get; set; }

        /*TODO: Allow for different map (level) sizes*/
        Tile[,] _tiles = new Tile[18, 16];
        TileObject[,] _tileObjects = new TileObject[18, 16];

        List<Texture2D> _tileTextures = new List<Texture2D>();
        List<Texture2D> _tileObjectTextures = new List<Texture2D>();
        /*TODO: Create a map generator*/
        int[,] textureMap = new int[18, 16]
        {
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
        };

        int[,] objectsTextureMap = new int[18, 16]
        {
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
        };

        public TileMap(Game game)
            : base(game)
        {
            this._camera = new Camera(game, new Point((Constants.TILE_WIDTH ^ 2) / 2, Constants.TILE_HEIGHT), this);
            Initialize();
        }

        public override void Initialize()
        {
            _players = new List<Player>();
            this._startLocation = new Point(3, 3);
            base.Initialize();
        }

        public void AddPlayer(Game game)
        {
            //player is a rock for now
            _players.Add(new Player(game, this._startLocation, this._tileObjectTextures[1], 100, 100, 100));
            objectsTextureMap[this._startLocation.Y, this._startLocation.Y] = 1;
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public Tile GetTile(Point point)
        {
            int row = point.Y;
            int col = point.X;
            return _tiles[row, col];
        }

        //Change parameter to Point
        public bool MoveTileObject(TileObject tileObject, Point point)
        {
            if (tileObject == null)
                return false;

            //also check here to make sure that there is no enemy or locked doorway or any other obstruction on that tile
            if (!this.GetTile(point)._active)
                return false;

            int tileObjectTextureIndex = objectsTextureMap[tileObject._location.Y, tileObject._location.X];
            objectsTextureMap[tileObject._location.Y, tileObject._location.X] = -1;
            objectsTextureMap[point.Y, point.X] = tileObjectTextureIndex;
            tileObject._location = point;
            tileObject._current = _tiles[point.Y, point.X];
            return true;
        }
        
        public int MapWidth
        {
            get { return textureMap.GetLength(1); }
        }

        public int MapHeight
        {
            get { return textureMap.GetLength(0); }
        }

        public void AddTileTexture(Texture2D texture)
        {
            _tileTextures.Add(texture);
        }

        public void AddTileObjectTexture(Texture2D texture)
        {
            _tileObjectTextures.Add(texture);
        }
        /*Seperate from Drawing TileObjects for efficiency, Where to call?*/
        public void AddTiles(int[,] tileTextureMap)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    int tileTextureIndex = tileTextureMap[y, x];
                    if (tileTextureIndex != -1) _tiles[y, x] = new Tile(this.Game, new Point(x, y), _tileTextures[tileTextureIndex]);

                    if (y - 1 >= 0)
                        _tiles[y, x]._left = _tiles[y - 1, x];
                    if (y + 1 < MapHeight)
                        _tiles[y, x]._left = _tiles[y + 1, x];
                    if (x - 1 >= 0)
                        _tiles[y, x]._left = _tiles[y, x - 1];
                    if (x + 1 < MapWidth)
                        _tiles[y, x]._left = _tiles[y, x + 1];
                }
            }
        }

        public void AddTileObjects(int[,] tileObjectTextureMap)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    int tileObjectTextureIndex = tileObjectTextureMap[y, x];
                    if (tileObjectTextureIndex != -1) _tiles[y, x]._tileObject = new TileObject(this.Game, new Point(x, y), _tileObjectTextures[tileObjectTextureIndex]);
                }
            }
        }

        public void AddTilesAndTileObjects(int[,] tileTextureMap, int[,] tileObjectTextureMap)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    int tileTextureIndex = tileTextureMap[y, x];
                    if (tileTextureIndex != -1) _tiles[y, x] = new Tile(this.Game, new Point(x, y), _tileTextures[tileTextureIndex]);

                    if (tileTextureIndex == 1) _tiles[y, x]._active = true;
                    //For tiles that are stone, mark them as active, this will need better implementation to allow for different active textures
                    int tileObjectTextureIndex = tileObjectTextureMap[y, x];
                    if (tileObjectTextureIndex != -1) _tiles[y, x]._tileObject = new TileObject(this.Game, new Point(x, y), _tileObjectTextures[tileObjectTextureIndex]);

                    if (y - 1 >= 0)
                        _tiles[y, x]._left = _tiles[y - 1, x];
                    if (y + 1 < MapHeight)
                        _tiles[y, x]._left = _tiles[y + 1, x];
                    if (x - 1 >= 0)
                        _tiles[y, x]._left = _tiles[y, x - 1];
                    if (x + 1 < MapWidth)
                        _tiles[y, x]._left = _tiles[y, x + 1];
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            this.AddTilesAndTileObjects(textureMap, objectsTextureMap);

            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    var left = x * Constants.TILE_WIDTH - (int)_camera._cameraPosition.X;
                    var top = y * Constants.TILE_HEIGHT - (int)_camera._cameraPosition.Y;
                    var top2 = y * (Constants.TILE_HEIGHT - Constants.TILE_OFFSET) - (int)_camera._cameraPosition.Y;

                    /*Draw tiles*/
                    if (_tiles[y, x] == null) continue;//DOES NOT ALLOW FOR TILE OBJECT WITHOUT TILE

                    if (y == 0)
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), Color.White);
                    else if (_tileTextures[2] == _tiles[y, x]._texture) //Dirt
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top2 + Constants.TILE_OFFSET, 101, 45),
                            new Rectangle(0, Constants.DIRT_OFFSET, Constants.TILE_WIDTH, Constants.DIRT_HEIGHT), Color.White);
                    else
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top2, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), Color.White);

                    /*Draw tile objects*/
                    if (_tiles[y, x]._tileObject == null) continue;

                    batch.Draw(_tiles[y, x]._tileObject._texture, new Rectangle(left, top2, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), Color.White);
                }
            }
        }


    }
}
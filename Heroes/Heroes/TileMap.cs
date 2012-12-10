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
    public class TileMap : ObserverGameComponent
    {
        public Camera _camera { get; set; }

        public Point _startLocation { get; private set; }

        public List<Player> _players { get; set; }

        /*TODO: Allow for different map (level) sizes*/
        Tile[,] _tiles = new Tile[18, 16];
        TileObject[,] _tileObjects = new TileObject[18, 16];
        List<Tile> _inRangeTiles = new List<Tile>();
        List<int> inRangeHash = new List<int>();
        List<int> drawHash = new List<int>();
        List<Texture2D> _tileTextures = new List<Texture2D>();
        List<Texture2D> _tileObjectTextures = new List<Texture2D>();
        /*TODO: Create a map generator*/
        int[,] textureMap = new int[18, 16]
        {
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
            {3, 3, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3},
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

            if (!this.GetTile(point)._active)
                return false;

            if (!_inRangeTiles.Contains(_tiles[point.Y, point.X]))
                return false;

            int tileObjectTextureIndex = objectsTextureMap[tileObject._location.Y, tileObject._location.X];
            objectsTextureMap[tileObject._location.Y, tileObject._location.X] = -1;
            objectsTextureMap[point.Y, point.X] = tileObjectTextureIndex;
            _tiles[tileObject._location.Y, tileObject._location.X]._tileObject = null;
            tileObject._location = point;
            tileObject._current = _tiles[point.Y, point.X];
            _tiles[tileObject._location.Y, tileObject._location.X]._tileObject = tileObject;
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
                }
            }
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    if (y - 1 >= 0)
                        _tiles[y, x]._top = _tiles[y - 1, x];
                    if (y + 1 < MapHeight)
                        _tiles[y, x]._bottom = _tiles[y + 1, x];
                    if (x - 1 >= 0)
                        _tiles[y, x]._left = _tiles[y, x - 1];
                    if (x + 1 < MapWidth)
                        _tiles[y, x]._right = _tiles[y, x + 1];
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

        public void LoadTiles()
        {
            this.AddTilesAndTileObjects(textureMap, objectsTextureMap);
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
                }
            }
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    if (y - 1 >= 0)
                        _tiles[y, x]._top = _tiles[y - 1, x];
                    if (y + 1 < MapHeight)
                        _tiles[y, x]._bottom = _tiles[y + 1, x];
                    if (x - 1 >= 0)
                        _tiles[y, x]._left = _tiles[y, x - 1];
                    if (x + 1 < MapWidth)
                        _tiles[y, x]._right = _tiles[y, x + 1];
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            //this.AddTilesAndTileObjects(textureMap, objectsTextureMap);
            Color shadeColor = Color.White;
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    shadeColor = Color.White;
                    var left = x * Constants.TILE_WIDTH - (int)_camera._cameraPosition.X;
                    var top = y * Constants.TILE_HEIGHT - (int)_camera._cameraPosition.Y;
                    var top2 = y * (Constants.TILE_HEIGHT - Constants.TILE_OFFSET) - (int)_camera._cameraPosition.Y;

                    /*Draw tiles*/
                    if (_tiles[y, x] == null) continue;//DOES NOT ALLOW FOR TILE OBJECT WITHOUT TILE
                    foreach (Tile highlightedTile in _inRangeTiles)
                    {
                        int drawHash = _tiles[y, x].GetHashCode();
                        int availableHash = highlightedTile.GetHashCode();
                        if (highlightedTile.GetHashCode() == _tiles[y, x].GetHashCode())
                        {
                            shadeColor = Color.Yellow;
                            break;
                        }
                    }
                    if (y == 0)
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), shadeColor);
                    else if (_tileTextures[2] == _tiles[y, x]._texture) //Dirt
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top2 + Constants.TILE_OFFSET, 101, 45),
                            new Rectangle(0, Constants.DIRT_OFFSET, Constants.TILE_WIDTH, Constants.DIRT_HEIGHT), shadeColor);
                    else
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top2, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), shadeColor);

                    /*Draw tile objects*/
                    if (_tiles[y, x]._tileObject == null) continue;

                    batch.Draw(_tiles[y, x]._tileObject._texture, new Rectangle(left, top2, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), Color.White);
                }
            }
        }

        public void receiveUpdate(Constants.GAME_UPDATE message, Object data)
        {
            switch (message)
            {
                case Constants.GAME_UPDATE.Roll:
                    Tuple<int, Point> bundle = data as Tuple<int, Point>;
                    int roll = bundle._item1;
                    Point startLoc = bundle._item2;
                    _inRangeTiles.Clear();
                    FindMoveableTiles(roll, startLoc, true);
                    foreach (Tile tile in _inRangeTiles)
                    {
                        inRangeHash.Add(tile.GetHashCode());
                    }
                    Console.WriteLine();
                    break;
            }
        }

        private void FindMoveableTiles(int roll, Point loc, bool start)
        {
            if (roll == 0)
            {
                return;
            }
            if (_inRangeTiles.Count() == 0)
            {

                Tile currTile = GetTile(loc);
                _inRangeTiles.Add(currTile);
            }
            List<Tile> newTiles = new List<Tile>();
            foreach (Tile tile in _inRangeTiles)
            {
                if (tile != null && tile._left._active && !tileBeenAdded(tile._left, newTiles))
                {
                    newTiles.Add(tile._left);
                }
                if (tile != null && tile._right._active && !tileBeenAdded(tile._right, newTiles))
                {
                    newTiles.Add(tile._right);
                }
                if (tile != null && tile._bottom._active && !tileBeenAdded(tile._bottom, newTiles))
                {
                    newTiles.Add(tile._bottom);
                }
                if (tile != null && tile._top._active && !tileBeenAdded(tile._top, newTiles))
                {
                    newTiles.Add(tile._top);
                }
            }
            _inRangeTiles.AddRange(newTiles);
            FindMoveableTiles(roll - 1, loc, false);
            //As the list starts out empty, and each new moveable space is added to the end, the initial
            //starting space will always be at the front of the list
            if (start)
            {
                _inRangeTiles.RemoveAt(0);
            }
        }

        private bool tileBeenAdded(Tile tileToCheck, List<Tile> newTiles)
        {
            foreach (Tile match in _inRangeTiles)
            {
                if (match == tileToCheck)
                {
                    return true;
                }
            }
            foreach (Tile match in newTiles)
            {
                if (match == tileToCheck)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
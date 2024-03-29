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
    public class TileMap
    {
        public Camera _camera { get; private set; }
        public Point _startLocation { get; private set; }
        public List<Player> _players { get; private set; }

        int[,] tileTextureMap;
        int[,] tileObjectTextureMap;
        Tile[,] _tiles;
        TileObject[,] _tileObjects;
        List<TileObject> _observers;

        List<Tile> _inRangeTiles = new List<Tile>();
        List<int> inRangeHash = new List<int>();
        List<int> drawHash = new List<int>();

        public TileMap(TextureMap map, Point start)
        {
            this.tileTextureMap = map._textureMap;
            this.tileObjectTextureMap = map._objectTextureMap;
            this._tiles = new Tile[this.MapHeight, this.MapWidth];
            this._tileObjects = new TileObject[this.MapHeight, this.MapWidth];
            this._startLocation = start;
            this._camera = new Camera(this._startLocation, this);
            this._players = new List<Player>();
            this._observers = new List<TileObject>();
        }

        public void AddPlayer()
        {
            _players.Add(new Player(this._startLocation, Texture.tileObjectTextures[4], 100, 100, 100));
            tileObjectTextureMap[this._startLocation.Y, this._startLocation.Y] = 4;
        }

        public bool RemoveTileObject(TileObject tileObject)
        {
            if (tileObject != null)
            {
                tileObjectTextureMap[tileObject._location.Y, tileObject._location.X] = -1;
                _tiles[tileObject._location.Y, tileObject._location.X]._tileObject = null;

                if (tileTextureMap[tileObject._location.Y, tileObject._location.X] == 1)
                    _tiles[tileObject._location.Y, tileObject._location.X]._active = true;
                
                return true;
            }

            return false;
        }

        public Enemy FindAdjacentEnemies(Tile tile)
        {
            if (tile._left != null && tile._left._tileObject is Enemy)
                return tile._left._tileObject as Enemy;
            if (tile._right != null && tile._right._tileObject is Enemy)
                return tile._right._tileObject as Enemy;
            if (tile._top != null && tile._top._tileObject is Enemy)
                return tile._top._tileObject as Enemy;
            if (tile._bottom != null && tile._bottom._tileObject is Enemy)
                return tile._bottom._tileObject as Enemy;

            return null;
        }

        public bool IsBattleTile(Tile tile)
        {
            if (FindAdjacentEnemies(tile) != null)
                return true;

            return false;
        }

        public Tile GetTile(Point point)
        {
            int row = point.Y;
            int col = point.X;
            if (row < MapWidth && col < MapHeight)
                return _tiles[row, col];
            return null;
        }

        public bool MoveTileObject(TileObject tileObject, Point point)
        {
            if (tileObject == null)
                return false;

            //also check here to make sure that there is no enemy or locked doorway or any other obstruction on that tile
            if (!this.GetTile(point)._active)
                return false;

            if (!_inRangeTiles.Contains(_tiles[point.Y, point.X]))
                return false;

            int tileObjectTextureIndex = tileObjectTextureMap[tileObject._location.Y, tileObject._location.X];
            tileObjectTextureMap[tileObject._location.Y, tileObject._location.X] = -1;
            tileObjectTextureMap[point.Y, point.X] = tileObjectTextureIndex;
            _tiles[tileObject._location.Y, tileObject._location.X]._tileObject = null;
            tileObject._location = point;
            tileObject._current = _tiles[point.Y, point.X];
            _tiles[tileObject._location.Y, tileObject._location.X]._tileObject = tileObject;
            return true;
        }
        
        public int MapWidth
        {
            get { return tileTextureMap.GetLength(1); }
        }

        public int MapHeight
        {
            get { return tileTextureMap.GetLength(0); }
        }

        public void LoadTiles()
        {
            this.AddTilesAndTileObjects(tileTextureMap, tileObjectTextureMap);
        }

        public void AddTilesAndTileObjects(int[,] tileTextureMap, int[,] tileObjectTextureMap)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    int tileTextureIndex = tileTextureMap[y, x];
                    if (tileTextureIndex != -1) _tiles[y, x] = new Tile(new Point(x, y), Texture.tileTextures[tileTextureIndex]);
                    if (tileTextureIndex == 1) _tiles[y, x]._active = true;
                    //For tiles that are stone, mark them as active, this will need better implementation to allow for different active textures
                    int tileObjectTextureIndex = tileObjectTextureMap[y, x];
                    if (tileObjectTextureIndex != -1)
                    {
                        //these two conditionals probably shouldnt be here
                        if (tileObjectTextureIndex == 4)
                        {
                            _tiles[y, x]._tileObject = _players[0];
                            _observers.Add(_players[0]);
                        }
                        //make tile inactive if the tile object is an enemy
                        else if (tileObjectTextureIndex == 5)
                        {
                            Enemy enemy = new Enemy(new Point(x, y), Texture.tileObjectTextures[tileObjectTextureIndex], 100, 100, 100, false);
                            _tiles[y, x]._tileObject = enemy;
                            _tiles[y, x]._active = false;
                            _observers.Add(enemy);
                        }
                        else
                        {
                            TileObject to = new TileObject(new Point(x, y), Texture.tileObjectTextures[tileObjectTextureIndex]);
                            _tiles[y, x]._tileObject = to;
                            _observers.Add(to);
                        }
                    }
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
            //this.AddTilesAndTileObjects(tileTextureMap, tileObjectTextureMap);
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
                    if (_tiles[y, x] == null) continue; //DOES NOT ALLOW FOR TILE OBJECT WITHOUT TILE

                    foreach (Tile highlightedTile in _inRangeTiles)
                    {
                        int drawHash = _tiles[y, x].GetHashCode();
                        int availableHash = highlightedTile.GetHashCode();
                        if (highlightedTile.GetHashCode() == _tiles[y, x].GetHashCode())
                        {
                            shadeColor = Color.Yellow;
                            if (IsBattleTile(highlightedTile))
                                shadeColor = Color.Red;
                            break;
                        }
                    }
                    if (y == 0)
                        batch.Draw(_tiles[y, x]._texture, new Rectangle(left, top, Constants.TILE_WIDTH, Constants.TILE_HEIGHT), shadeColor);
                    else if (Texture.tileTextures[2] == _tiles[y, x]._texture) //Dirt
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

        public void notifyObservers(Constants.GAME_UPDATE message, Object data)
        {
            if (data == null)
            {
                return;
            }
            foreach (TileObject observer in _observers)
            {
                observer.receiveUpdate(message, data);
            }
        }
        public void FindMoveableTiles(Tuple<int, Point> bundle)
        {
            int roll = bundle._item1;
            Point startLoc = bundle._item2;
            _inRangeTiles.Clear();
            FindMoveableTiles(roll, startLoc, true);
            /*foreach (Tile tile in _inRangeTiles)
            {
                inRangeHash.Add(tile.GetHashCode());
            }*/
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
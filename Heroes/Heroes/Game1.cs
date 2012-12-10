using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Heroes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileMap tileMap;
        Constants.GAME_STATE gameState;
        List<Texture2D> diceTextures = new List<Texture2D>();
        double lastRollDisplayed;
        Texture2D currentDieTexture;
        List<Tuple<int, Tile>> moveableTiles;
        
        public Game1()
        {
            tileMap = new TileMap(this);
           
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = tileMap.MapWidth * 101;
            graphics.PreferredBackBufferHeight = tileMap.MapHeight * 171;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TouchPanel.EnabledGestures = GestureType.FreeDrag | GestureType.Tap;
            gameState = Constants.GAME_STATE.Roll;
            lastRollDisplayed = 0;
            currentDieTexture = null;
            moveableTiles = new List<Tuple<int, Tile>>();
            base.Initialize();  
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Grass Block"));
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Stone Block"));
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Dirt Block"));
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Water Block"));

            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Tree Tall"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Rock"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Open Door"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Closed Door"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_one"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_two"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_three"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_four"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_five"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_six"));

            tileMap.AddPlayer(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //test variables (remove later)
        public int curx = 3;
        public int cury = 3;

        protected override void Update(GameTime gameTime)
        {
            if (lastRollDisplayed > 0 && gameTime.TotalGameTime.TotalSeconds - lastRollDisplayed > 2)
            {
                lastRollDisplayed = 0;
                currentDieTexture = null;
            }
            switch (gameState)
            {
                case Constants.GAME_STATE.Roll:
                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();
                        if (gesture.GestureType == GestureType.Tap)
                        {
                            lastRollDisplayed = gameTime.TotalGameTime.TotalSeconds;
                            int roll = Die.getInstance().roll();
                            currentDieTexture = diceTextures.ElementAt<Texture2D>(roll - 1);
                            //Calculate moveable squares
                            FindMoveableTiles(roll, true);
                            gameState = Constants.GAME_STATE.Move;
                            Console.WriteLine(moveableTiles.ToString());
                        }
                    }
                    break;

                case Constants.GAME_STATE.FirstAction:
                    break;

                case Constants.GAME_STATE.Move:

                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();

                        if (gesture.GestureType == GestureType.FreeDrag)
                            tileMap._camera.MoveCamera(-gesture.Delta);

                        //can give you the tile instance that the user clicked on
                        if (gesture.GestureType == GestureType.Tap)
                        {
                            int x = (int)tileMap._camera._cameraPosition.X + (int)gesture.Position.X - Constants.MARGIN_LEFT;
                            int y = (int)tileMap._camera._cameraPosition.Y + (int)gesture.Position.Y - Constants.MARGIN_TOP;
                            TileObject to = tileMap.GetTile(new Point(curx, cury))._tileObject;
                            Tile t = tileMap.GetTile(new Point(x / Constants.TILE_WIDTH, y / (Constants.TILE_HEIGHT - Constants.TILE_OFFSET)));
                            if (tileMap.MoveTileObject(to, new Point(t._location.X, t._location.Y)))
                            {
                                curx = t._location.X;
                                cury = t._location.Y;
                            }
                        }
                    }
                    break;

                case Constants.GAME_STATE.SecondAction:
                    break;

                case Constants.GAME_STATE.End:
                    break;
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
             
            // TODO: Add your update logic here
            /*if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.FreeDrag)
                    tileMap._camera.MoveCamera(-gesture.Delta);

                //can give you the tile instance that the user clicked on
                if (gesture.GestureType == GestureType.Tap)
                {
                    int x = (int)tileMap._camera._cameraPosition.X + (int)gesture.Position.X - Constants.MARGIN_LEFT;
                    int y = (int)tileMap._camera._cameraPosition.Y + (int)gesture.Position.Y - Constants.MARGIN_TOP;
                    TileObject to = tileMap.GetTile(new Point(curx, cury))._tileObject;
                    Tile t = tileMap.GetTile(new Point(x / Constants.TILE_WIDTH, y / (Constants.TILE_HEIGHT - Constants.TILE_OFFSET)));
                    if (tileMap.MoveTileObject(to, new Point(t._location.X, t._location.Y)))
                    {
                        curx = t._location.X;
                        cury = t._location.Y;
                    }
                }
            }*/

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            tileMap.Draw(spriteBatch);
            if (currentDieTexture != null)
            {
                spriteBatch.Draw(currentDieTexture, new Rectangle(75, 75, 300, 300), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void FindMoveableTiles(int roll, bool start)
        {
            if (roll == 0)
            {
                return;
            }
            if (moveableTiles.Count() == 0)
            {
                Tile currTile = tileMap.GetTile(new Point(curx, cury));
                moveableTiles.Add(new Tuple<int, Tile>(0, currTile));
            }
            List<Tuple<int, Tile>> newTiles = new List<Tuple<int, Tile>>();
            foreach (Tuple<int, Tile> tile in moveableTiles)
            {
                if (tile._item2._left != null && tile._item2._left._active && !tileBeenAdded(tile._item2._left, newTiles))
                {
                    newTiles.Add(new Tuple<int, Tile>(tile._item1 + 1, tile._item2._left));
                }
                if (tile._item2._right != null && tile._item2._right._active && !tileBeenAdded(tile._item2._right, newTiles))
                {
                    newTiles.Add(new Tuple<int, Tile>(tile._item1 + 1, tile._item2._right));
                }
                if (tile._item2._bottom != null && tile._item2._bottom._active && !tileBeenAdded(tile._item2._bottom, newTiles))
                {
                    newTiles.Add(new Tuple<int, Tile>(tile._item1 + 1, tile._item2._bottom));
                }
                if (tile._item2._top != null && tile._item2._top._active && !tileBeenAdded(tile._item2._top, newTiles))
                {
                    newTiles.Add(new Tuple<int, Tile>(tile._item1 + 1, tile._item2._top));
                }
            }
            moveableTiles.AddRange(newTiles);
            FindMoveableTiles(roll - 1, false);
            //As the list starts out empty, and each new moveable space is added to the end, the initial
            //starting space will always be at the front of the list
            if (start)
            {
                moveableTiles.RemoveAt(0);
            }
        }

        private bool tileBeenAdded(Tile tileToCheck, List<Tuple<int, Tile>> newTiles)
        {
            foreach (Tuple<int, Tile> match in moveableTiles)
            {
                if (match._item2 == tileToCheck)
                {
                    return true;
                }
            }
            foreach (Tuple<int, Tile> match in newTiles)
            {
                if (match._item2 == tileToCheck)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

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
    /// This is the main type for your gamez
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
        int movesLeft;
        Tile current;
        int playerRoll;
        int enemyRoll;
        Enemy engagedEnemy;
        
        public Game1()
        {
            tileMap = TileMapFactory.GetTileMapById(1);
           
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
            movesLeft = 0;
            currentDieTexture = null;
            base.Initialize();  
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // this requires tilemap and tilemapfactory to know the indexes of each texture (change this?)
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Grass Block"));
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Stone Block"));
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Dirt Block"));
            tileMap.AddTileTexture(Content.Load<Texture2D>("Tiles\\Water Block"));

            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Tree Tall"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Rock"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Open Door"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Closed Door"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Player"));
            tileMap.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Enemy"));


            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_one"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_two"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_three"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_four"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_five"));
            diceTextures.Add(Content.Load<Texture2D>("Dice\\dice_six"));

            tileMap.AddPlayer();
            tileMap.LoadTiles();
            current = tileMap.GetTile(tileMap._startLocation);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

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
                            movesLeft = Die.getInstance().roll();
                            currentDieTexture = diceTextures.ElementAt<Texture2D>(movesLeft - 1);
                            //Calculate moveable squares
                            Tuple<int, Point> message = new Tuple<int, Point>(movesLeft, current._location);
                            tileMap.receiveUpdate(Constants.GAME_UPDATE.Roll, message);
                            gameState = Constants.GAME_STATE.Move;
                        }
                    }
                    break;

                case Constants.GAME_STATE.FirstAction:
                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();

                        if (gesture.GestureType == GestureType.Tap)
                        {
                            lastRollDisplayed = gameTime.TotalGameTime.TotalSeconds;
                            playerRoll = Die.getInstance().roll();
                            currentDieTexture = diceTextures.ElementAt<Texture2D>(playerRoll - 1);
                            gameState = Constants.GAME_STATE.SecondAction;

                        }
                    }
                    break;

                case Constants.GAME_STATE.Move:
                    engagedEnemy = tileMap.FindAdjacentEnemies(current);
                    if (engagedEnemy != null)
                    {
                        gameState = Constants.GAME_STATE.FirstAction;
                        break;
                    }

                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();

                        if (gesture.GestureType == GestureType.FreeDrag)
                            tileMap._camera.MoveCamera(-gesture.Delta);

                        if (gesture.GestureType == GestureType.Tap)
                        {
                            int x = (int)tileMap._camera._cameraPosition.X + (int)gesture.Position.X - Constants.MARGIN_LEFT;
                            int y = (int)tileMap._camera._cameraPosition.Y + (int)gesture.Position.Y - Constants.MARGIN_TOP;
                            TileObject to = current._tileObject;
                            Tile t = tileMap.GetTile(new Point(x / Constants.TILE_WIDTH, y / (Constants.TILE_HEIGHT - Constants.TILE_OFFSET)));
                            
                            if (tileMap.MoveTileObject(to, new Point(t._location.X, t._location.Y)))
                            {
                                movesLeft -= (Math.Abs(current._location.X - t._location.X) + Math.Abs(current._location.Y - t._location.Y));
                                current = t;
                                Tuple<int, Point> message = new Tuple<int, Point>(movesLeft, current._location);
                                tileMap.receiveUpdate(Constants.GAME_UPDATE.Roll, message);

                                if (movesLeft == 0)
                                {
                                    gameState = Constants.GAME_STATE.Roll;
                                }
                            }
                        }
                    }
                    break;

                case Constants.GAME_STATE.SecondAction:
                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();

                        if (gesture.GestureType == GestureType.Tap)
                        {
                            lastRollDisplayed = gameTime.TotalGameTime.TotalSeconds;
                            enemyRoll = Die.getInstance().roll();
                            currentDieTexture = diceTextures.ElementAt<Texture2D>(enemyRoll - 1);
                            if (playerRoll > enemyRoll)
                                tileMap.RemoveTileObject(engagedEnemy);

                            if (movesLeft == 0)
                                gameState = Constants.GAME_STATE.Roll;
                            else
                            {
                                Tuple<int, Point> message = new Tuple<int, Point>(movesLeft, current._location);
                                tileMap.receiveUpdate(Constants.GAME_UPDATE.Roll, message);
                                gameState = Constants.GAME_STATE.Move;
                            }
                        }
                    }
                    break;

                case Constants.GAME_STATE.End:
                    break;
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

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
                spriteBatch.Draw(currentDieTexture, new Rectangle(30, 30, 150, 150), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

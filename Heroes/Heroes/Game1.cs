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
        double lastRollDisplayed;
        Texture2D playerDieTexture;
        Texture2D enemyDieTexture;
        int movesLeft;
        Tile current;
        int playerRoll;
        int enemyRoll;
        Enemy engagedEnemy;
        
        public Game1()
        {
            tileMap = TileMapFactory.GetTileMapById(1);

            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

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
            playerDieTexture = null;
            enemyDieTexture = null;
            base.Initialize();  
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture.AddTileTexture(Content.Load<Texture2D>("Tiles\\Grass Block"));
            Texture.AddTileTexture(Content.Load<Texture2D>("Tiles\\Stone Block"));
            Texture.AddTileTexture(Content.Load<Texture2D>("Tiles\\Dirt Block"));
            Texture.AddTileTexture(Content.Load<Texture2D>("Tiles\\Water Block"));

            Texture.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Tree Tall"));
            Texture.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Rock"));
            Texture.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Open Door"));
            Texture.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Closed Door"));
            Texture.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Player"));
            Texture.AddTileObjectTexture(Content.Load<Texture2D>("TileObjects\\Enemy"));

            Texture.AddDiceTexture(Content.Load<Texture2D>("Dice\\dice_one"));
            Texture.AddDiceTexture(Content.Load<Texture2D>("Dice\\dice_two"));
            Texture.AddDiceTexture(Content.Load<Texture2D>("Dice\\dice_three"));
            Texture.AddDiceTexture(Content.Load<Texture2D>("Dice\\dice_four"));
            Texture.AddDiceTexture(Content.Load<Texture2D>("Dice\\dice_five"));
            Texture.AddDiceTexture(Content.Load<Texture2D>("Dice\\dice_six"));

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
                playerDieTexture = null;
                enemyDieTexture = null;
            }
            switch (gameState)
            {
                case Constants.GAME_STATE.Roll:
                    if (tileMap.IsBattleTile(current))
                    {
                        gameState = Constants.GAME_STATE.SecondAction;
                        break;
                    }

                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();

                        if (gesture.GestureType == GestureType.Tap)
                        {
                            lastRollDisplayed = gameTime.TotalGameTime.TotalSeconds;
                            movesLeft = Die.getInstance().roll();
                            playerDieTexture = Texture.diceTextures.ElementAt<Texture2D>(movesLeft - 1);
                            //Calculate moveable squares
                            Tuple<int, Point> message = new Tuple<int, Point>(movesLeft, current._location);
                            tileMap.FindMoveableTiles(message);
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
                            gameState = Constants.GAME_STATE.SecondAction;

                        }
                    }
                    break;

                case Constants.GAME_STATE.Move:
                    engagedEnemy = tileMap.FindAdjacentEnemies(current);
                    if (engagedEnemy != null)
                    {
                        gameState = Constants.GAME_STATE.SecondAction;
                        break;
                    }

                    if (movesLeft == 0)
                        gameState = Constants.GAME_STATE.Roll;

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
                                tileMap.FindMoveableTiles(message);

                                if (tileMap.IsBattleTile(current))
                                    break;

                                if (movesLeft == 0)
                                    gameState = Constants.GAME_STATE.Roll;

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
                            Player player = (Player) current._tileObject;
                            lastRollDisplayed = gameTime.TotalGameTime.TotalSeconds;
                            playerRoll = Die.getInstance().roll();
                            playerDieTexture = Texture.diceTextures.ElementAt<Texture2D>(playerRoll - 1);
                            enemyRoll = Die.getInstance().roll();
                            enemyDieTexture = Texture.diceTextures.ElementAt<Texture2D>(enemyRoll - 1);
                            if (playerRoll > enemyRoll)
                            {
                                engagedEnemy._health -= 1;
                                if (engagedEnemy._health == 0)
                                {
                                    tileMap.RemoveTileObject(engagedEnemy);
                                    Tuple<TileObject, TileObject> bundle = new Tuple<TileObject, TileObject>(player, engagedEnemy);
                                    tileMap.notifyObservers(Constants.GAME_UPDATE.Capture, bundle);
                                }
                                if (movesLeft == 0)
                                    gameState = Constants.GAME_STATE.Roll;
                            }
                            else if (playerRoll == enemyRoll)
                            {
                                engagedEnemy._health -= 1;
                                if (engagedEnemy._health == 0)
                                {
                                    tileMap.RemoveTileObject(engagedEnemy);
                                    Tuple<TileObject, TileObject> bundle = new Tuple<TileObject, TileObject>(player, engagedEnemy);
                                    tileMap.notifyObservers(Constants.GAME_UPDATE.Capture, bundle);
                                }
                                player._health -= 1;
                                //GAME OVER!
                                if (player._health == 0)
                                    tileMap.RemoveTileObject(player);
                                if (movesLeft == 0)
                                    gameState = Constants.GAME_STATE.Roll;
                            }
                            else
                            {
                                player._health -= 1;
                                //GAME OVER!
                                if (player._health == 0)
                                    tileMap.RemoveTileObject(player);
                                if (movesLeft == 0)
                                    gameState = Constants.GAME_STATE.Roll;
                            }

                            Tuple<int, Point> message = new Tuple<int, Point>(movesLeft, current._location);
                            tileMap.FindMoveableTiles(message);
                            gameState = Constants.GAME_STATE.Move;
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
            if (playerDieTexture != null)
            {
                spriteBatch.Draw(playerDieTexture, new Rectangle(30, 600, 150, 150), Color.White);
            }
            if (enemyDieTexture != null)
            {
                spriteBatch.Draw(enemyDieTexture, new Rectangle(300, 600, 150, 150), Color.White);
            }
            int playerHealth = 0;
            if (current._tileObject != null)
            {
                playerHealth = ((Player)current._tileObject)._health;
            }
            if (playerHealth != 0)
            {
                Texture2D rect = new Texture2D(graphics.GraphicsDevice, 30, 90);

                Color[] data = new Color[30 * 90];
                for (int j = 0; j < data.Length; ++j)
                {
                    data[j] = Color.Red;
                }
                rect.SetData(data);

                for (int i = 0; i < playerHealth; i++)
                {
                    //spriteBatch.Draw(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(30 + i * 30, 30, 75, 75), Color.Red);

                    Vector2 coor = new Vector2(30 + i * 60, 30);
                    spriteBatch.Draw(rect, coor, Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

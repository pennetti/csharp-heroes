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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
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
                    //Change getTile to accept a Point
                    TileObject to = tileMap.GetTile(new Point(cury, curx))._tileObject;
                    Tile t = tileMap.GetTile(new Point(x / Constants.TILE_WIDTH, y / (Constants.TILE_HEIGHT - Constants.TILE_OFFSET)));
                    if (t._active) tileMap.MoveTileObject(to, new Point(t._location.X, t._location.Y));
                    curx = t._location.X;
                    cury = t._location.Y;

                    System.Diagnostics.Debug.WriteLine("clicked");
                    System.Diagnostics.Debug.WriteLine("camera x: " + (int)tileMap._camera._cameraPosition.X);
                    System.Diagnostics.Debug.WriteLine("camera y: " + (int)tileMap._camera._cameraPosition.Y);
                    System.Diagnostics.Debug.WriteLine("click x: " + (int)gesture.Position.X);
                    System.Diagnostics.Debug.WriteLine("click y: " + (int)gesture.Position.Y);
                    System.Diagnostics.Debug.WriteLine("x: " + x);
                    System.Diagnostics.Debug.WriteLine("y: " + y);
                    System.Diagnostics.Debug.WriteLine("target tile x: " + curx);
                    System.Diagnostics.Debug.WriteLine("target tile y: " + cury);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            tileMap.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

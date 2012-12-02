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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TileMap : Microsoft.Xna.Framework.GameComponent
    {
        const int TILE_HEIGHT = 171;
        const int TILE_WIDTH = 101;
        const int TILE_OFFSET = 87;

        const int DIRT_HEIGHT = 45;
        const int DIRT_OFFSET = 126;

        const int TILES_WIDE = 5;
        const int TILES_HIGH = 13;

        const int MARGIN_LEFT = 0;
        const int MARGIN_RIGHT = 25;
        const int MARGIN_TOP = 49;
        const int MARGIN_BOTTOM = 93;

        /*Paramter is the starting position of the camera.*/
        Vector2 CameraPosition = new Vector2((TILE_WIDTH ^ 2) / 2, TILE_HEIGHT);

        List<Texture2D> tileTextures = new List<Texture2D>();
        List<Texture2D> tileObjectTextures = new List<Texture2D>();

        int[,] map = new int[,]
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

        int[,] tileObjects = 
        {
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
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

        public int MapWidth
        {
            get {return map.GetLength(1); }
        }

        public int MapHeight
        {
            get {return map.GetLength(0); }
        }

        public void AddTileTexture(Texture2D texture) 
        {
            tileTextures.Add(texture);
        }

        public void AddTileObjectTexture(Texture2D texture)
        {
            tileObjectTextures.Add(texture);
        }

        public void MoveCamera(Vector2 cameraDirection)
        {
            CameraPosition += cameraDirection;

            if (CameraPosition.X < MARGIN_LEFT)
                CameraPosition.X = MARGIN_LEFT;
            if (CameraPosition.Y < MARGIN_TOP)
                CameraPosition.Y = MARGIN_TOP;
            if (CameraPosition.X > (MapWidth - TILES_WIDE) * TILE_WIDTH * MARGIN_RIGHT)
                CameraPosition.X = (MapWidth - TILES_WIDE) * TILE_WIDTH + 25;
            if (CameraPosition.Y > (MapHeight - TILES_HIGH) * TILE_HEIGHT - MARGIN_BOTTOM)
                CameraPosition.Y = (MapHeight - TILES_HIGH) * TILE_HEIGHT - MARGIN_BOTTOM;
        }

        public void Draw(SpriteBatch batch)
        {
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    var left = x * TILE_WIDTH - (int)CameraPosition.X;
                    var top = y * TILE_HEIGHT - (int)CameraPosition.Y;
                    var top2 = y * (TILE_HEIGHT - TILE_OFFSET) - (int)CameraPosition.Y;

                    /*Draw tiles*/
                    var textureIndex = map[y, x];
                    if (textureIndex == -1) continue;

                    var texture = tileTextures[textureIndex];
                    if (y == 0)
                        batch.Draw(texture, new Rectangle(left, top, TILE_WIDTH, TILE_HEIGHT), Color.White);
                    else if (textureIndex == 2) //Dirt
                        batch.Draw(texture, new Rectangle(left, top2 + TILE_OFFSET, 101, 45),
                            new Rectangle(0, DIRT_OFFSET, TILE_WIDTH, DIRT_HEIGHT), Color.White);
                    else
                        batch.Draw(texture, new Rectangle(left, top2, TILE_WIDTH, TILE_HEIGHT), Color.White);


                    var tileObjectIndex = tileObjects[y, x];
                    if (tileObjectIndex == -1) continue;

                    var tileObject = tileObjectTextures[tileObjectIndex];
                    batch.Draw(tileObject, new Rectangle(left, top2, TILE_WIDTH, TILE_HEIGHT), Color.White);
                }
            }
        }

        public TileMap(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}

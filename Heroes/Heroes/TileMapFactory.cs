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
    public class TileMapFactory
    {
        public TileMapFactory()
        {
        }

        public static TileMap GetTileMapById(int id)
        {
            TextureMap textureMap;
            TileMap tileMap;

            switch (id)
            {
                case 1:
                    textureMap = new TextureMap(new int[18, 16]
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
                                                    },
                                                    new int[18, 16]
                                                    {
                                                        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                                                        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                                                        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                                                        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                                                        {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                                                        {-1, -1, -1, -1, -1, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
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
                                                    });

                    tileMap = new TileMap(textureMap, new Point(3, 3));
                    return tileMap;

                case 2:
                    textureMap = new TextureMap(new int[2, 2]
                                                    {
                                                        {1, 1},
                                                        {1, 1}
                                                    },
                                                    new int[2, 2]
                                                    {
                                                        {0, -1},
                                                        {2, 3}
                                                    });

                    tileMap = new TileMap(textureMap, new Point(0, 0));
                    return tileMap;


            }
            return null;
        }
    }
}

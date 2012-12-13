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
    public static class Constants 
    {
        public const int TILE_HEIGHT = 171;
        public const int TILE_WIDTH = 101;
        public const int TILE_OFFSET = 87;

        public const int DIRT_HEIGHT = 45;
        public const int DIRT_OFFSET = 126;

        public const int TILES_WIDE = 5;
        public const int TILES_HIGH = 13;

        public const int MARGIN_LEFT = 0;
        public const int MARGIN_RIGHT = 25;
        public const int MARGIN_TOP = 49;
        public const int MARGIN_BOTTOM = 93;

        public const int BOARD_1 = 1;
        
        public enum GAME_STATE
        {
            Roll, FirstAction, Move, SecondAction, End
        };

        public enum GAME_UPDATE
        {
            Roll, Draw
        };
    }
}

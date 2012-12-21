using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Heroes
{
    public class Texture
    {
        public static List<Texture2D> tileTextures = new List<Texture2D>();
        public static List<Texture2D> tileObjectTextures = new List<Texture2D>();
        public static List<Texture2D> diceTextures = new List<Texture2D>();

        public static void AddTileTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public static void AddTileObjectTexture(Texture2D texture)
        {
            tileObjectTextures.Add(texture);
        }

        public static void AddDiceTexture(Texture2D texture)
        {
            diceTextures.Add(texture);
        }
    }
}

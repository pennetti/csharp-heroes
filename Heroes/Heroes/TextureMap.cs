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
    public class TextureMap : Microsoft.Xna.Framework.GameComponent
    {
        public int[,] _textureMap;
        public int[,] _objectTextureMap;

        public TextureMap(Game game, int[,] textureMap, int[,] objectTextureMap)
            : base(game)
        {
            _textureMap = textureMap;
            _objectTextureMap = objectTextureMap;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public int TextureMapWidth
        {
            get { return _textureMap.GetLength(1); }
        }

        public int TextureMapHeight
        {
            get { return _textureMap.GetLength(0); }
        }

        public int ObjectTextureMapWidth
        {
            get { return _objectTextureMap.GetLength(1); }
        }

        public int ObjectTextureMapHeight
        {
            get { return _objectTextureMap.GetLength(0); }
        }
    }
}

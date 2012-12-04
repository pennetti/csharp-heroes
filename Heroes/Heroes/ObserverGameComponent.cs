using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public abstract class ObserverGameComponent : Microsoft.Xna.Framework.GameComponent
    {
        public void receiveUpdate(String message, Object data)
        {
            //Do Nothing
        }

        public ObserverGameComponent(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

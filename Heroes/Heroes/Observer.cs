using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public interface Observer
    {
        void receiveUpdate(Constants.GAME_UPDATE message, Object data);
    }
}

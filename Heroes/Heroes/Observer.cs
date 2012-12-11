using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public interface Observer
    {
        void receiveUpdate(Object message, Object data);
    }
}

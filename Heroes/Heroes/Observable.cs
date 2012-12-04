using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public abstract class Observable
    {
        public List<ObserverGameComponent> observers = null;

        public void addObserver(ObserverGameComponent o)
        {
            if (observers.Contains(o))
            {
                return;
            }
            observers.Add(o);
        }

        //Might change String to an int later
        public void notifyObservers(String message, Object data)
        {
            foreach (ObserverGameComponent o in observers)
            {
                o.receiveUpdate(message, data);
            }
        }

        public void removeObserver(ObserverGameComponent o)
        {
            //Safe as observers removes true if removed and false if not found in list, rather than generating exception
            observers.Remove(o);
        }
    }
}

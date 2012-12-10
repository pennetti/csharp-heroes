using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class Die
    {
        public static Die instance;
        private Random rand;

        private Die()
        {
            rand = new Random();
        }

        public static Die getInstance()
        {
            if (instance == null)
            {
                instance = new Die();
            }
            return instance;
        }

        public int roll()
        {
            return (int)Math.Ceiling(rand.NextDouble() * 4);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    class Die
    {
        private static Die instance = null;
        private Random rand;
        private Die()
        {
            rand = new Random();
        }

        public Die getInstance()
        {
            if (instance == null)
            {
                instance = new Die();
            }
            return instance;
        }

        public int roll()
        {
            return (int)Math.Ceiling(rand.NextDouble() * 6);
        }
    }
}

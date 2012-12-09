using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class Tuple<T1, T2>
    {
        public T1 _item1 { get; set; }
        public T2 _item2 { get; set; }

        public Tuple(T1 item1, T2 item2)
        {
            _item1 = item1;
            _item2 = item2;
        }
    }
}

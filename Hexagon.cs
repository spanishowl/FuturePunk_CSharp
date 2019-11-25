using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturePunk
{
    class Point
    {
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public readonly double x;
        public readonly double y;
    }
    
    class Hexagon
    {
        public Hexagon(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;

            if (q + r + s != 0) throw new ArgumentException("q + r + s must be 0");
        }
        public readonly int q;
        public readonly int r;
        public readonly int s;

    }
}


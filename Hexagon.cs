using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturePunk
{
    
    class Hexagon
    {
        public Hexagon(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            if (this.x + y + z != 0) throw new ArgumentException("x + y + z must be 0");
        }
        public readonly int x;
        public readonly int y;
        public readonly int z;

        public Hexagon Add(Hexagon b)
        {
            return new Hexagon(x + b.x, y + b.y, z + b.z);
        }
        public Hexagon Subtract(Hexagon b)
        {
            return new Hexagon(x - b.x, y - b.y, z - b.z);
        }
        public Hexagon Scale(int k)
        {
            return new Hexagon(x * k, y * k, z * k);
        }
        public Hexagon RotateLeft()
        {
            return new Hexagon(-z, -x, -y);
        }
        public Hexagon RotateRight()
        {
            return new Hexagon(-y, -z, -x);
        }
        static public List<Hexagon> directions = new List<Hexagon>
        {
            new Hexagon(1, 0, -1),
            new Hexagon(1, -1, 0),
            new Hexagon(0, -1, 1),
            new Hexagon(-1, 0, 1),
            new Hexagon(-1, 1, 0),
            new Hexagon(0, -1, 1)
        };
        static public Hexagon Direction(int direction)
        {
            return Hexagon.directions[direction];
        }
        public Hexagon Neighbor(int direction)
        {
            return Add(Hexagon.Direction(direction));
        }
        static public List<Hexagon> diagonals = new List<Hexagon>
        {
            new Hexagon(2, -1, -1),
            new Hexagon(1, -2, 1),
            new Hexagon(-1, -1, 2),
            new Hexagon(-2, 1, 1),
            new Hexagon(-1, 2, -1),
            new Hexagon(1, 1, -2)
        };
        public Hexagon DiagonalNeighbor(int direction)
        {
            return Add(Hexagon.diagonals[direction]);
        }
        public int Length()
        {
            return (int)((Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2);
        }
        public int Distance(Hexagon b)
        {
            return Subtract(b).Length();
        }
    }

    class FractionalHex
    {
        public FractionalHex(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            if (Math.Round(x + y + z) != 0) throw new ArgumentException("x + y + Z must be 0");
        }
        public readonly double x;
        public readonly double y;
        public readonly double z;

        public Hexagon HexRound()
        {
            int xi = (int)(Math.Round(x));
            int yi = (int)(Math.Round(y));
            int zi = (int)(Math.Round(z));
            double xdiff = Math.Abs(xi - x);
            double ydiff = Math.Abs(yi - y);
            double zdiff = Math.Abs(zi - z);
            if (xdiff > ydiff && xdiff > zdiff)
            {
                xi = -yi - zi;
            }
            else
                if (ydiff > zdiff)
            {
                yi = -xi - zi;
            }
            else
            {
                zi = -xi - yi;
            }
            return new Hexagon(xi, yi, zi);
        }
        public FractionalHex HexLerp(FractionalHex b, double t)
        {
            return new FractionalHex(x * (1.0 - t) + b.x * t, y * (1.0 - t) + b.y * t, z * (1.0 - t) + b.z * t);
        }
        static public List<Hexagon> HexLinedraw(Hexagon a, Hexagon b)
        {
            int N = a.Distance(b);
            FractionalHex anudge = new FractionalHex(a.x + 1e-06, a.y + 1e-06, a.z - 2e-06);
            FractionalHex bnudge = new FractionalHex(b.x + 1e-06, b.y + 1e-06, b.z - 2e-06);
            List<Hexagon> results = new List<Hexagon> { };
            double step = 1.0 / Math.Max(N, 1);
            for 
                (int i = 0; i <= N; i++)
            {
                results.Add(anudge.HexLerp(bnudge, step * i).HexRound());
            }
            return results;
        }
    }

    class OffsetCoord
    {
        public OffsetCoord(int col, int row)
        {
            this.col = col;
            this.row = row;
        }
        public readonly int col;
        public readonly int row;
        static public int EVEN = 1;
        static public int ODD = -1;

        static public OffsetCoord XoffsetFromCube(int offset, Hexagon h)
        {
            int col = h.x;
            int row = h.y + (int)((h.x + offset * (h.x & 1)) / 2);
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new OffsetCoord(col, row);
        }
        static public Hexagon XoffsetToCube(int offset, OffsetCoord h)
        {
            int x = h.col;
            int y = h.row - (int)((h.col + offset * (h.col & 1)) / 2);
            int z = -x - y;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new Hexagon(x, y, z);
        }
        static public OffsetCoord YoffsetFromCube(int offset, Hexagon h)
        {
            int col = h.x + (int)((h.y + offset * (h.y & 1)) / 2);
            int row = h.y;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new OffsetCoord(col, row);
        }
        static public Hexagon YoffsetToCube(int offset, OffsetCoord h)
        {
            int x = h.col - (int)((h.row + offset * (h.row & 1)) / 2);
            int y = h.row;
            int z = -x - y;
            if (offset != OffsetCoord.EVEN && offset != OffsetCoord.ODD)
            {
                throw new ArgumentException("offset must be EVEN (+1) or ODD (-1)");
            }
            return new Hexagon(x, y, z);
        }

        class DoubledCoord
        {
            public DoubledCoord(int col, int row)
            {
                this.col = col;
                this.row = row;
            }
            public readonly int col;
            public readonly int row;

            static public DoubledCoord XdoubledFromCube(Hexagon h)
            {
                int col = h.x;
                int row = 2 * h.y + h.x;
                return new DoubledCoord(col, row);
            }
            public Hexagon XdoubledToCube()
            {
                int x = col;
                int y = (int)((row - col) / 2);
                int z = -x - y;
                return new Hexagon(x, y, z);
            }
            static public DoubledCoord YdoubledFromCube(Hexagon h)
            {
                int col = 2 * h.x + h.y;
                int row = h.y;
                return new DoubledCoord(col, row);
            }
            public Hexagon YdoubledToCube()
            {
                int x = (int)((col - row) / 2);
                int y = row;
                int z = -x - y;
                return new Hexagon(x, y, z);
            }
            
        }

        class Orientation
        {
            public Orientation(double f0, double f1, double f2, double f3,
                               double b0, double b1, double b2, double b3,
                               double startAngle)
            {
                this.f0 = f0;
                this.f1 = f1;
                this.f2 = f2;
                this.f3 = f3;
                this.b0 = b0;
                this.b1 = b1;
                this.b2 = b2;
                this.b3 = b3;
                this.startAngle = startAngle;
            }
            public readonly double f0;
            public readonly double f1;
            public readonly double f2;
            public readonly double f3;
            public readonly double b0;
            public readonly double b1;
            public readonly double b2;
            public readonly double b3;
            public readonly double startAngle;
        }

        class Layout
        {
            public Layout(Orientation orientation, Point size, Point origin)
            {
                this.orientation = orientation;
                this.size = size;
                this.origin = origin;
            }
            public readonly Orientation orientation;
            public readonly Point size;
            public readonly Point origin;

            static public Orientation pointy = new Orientation(
                Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0,
                -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
            static public Orientation flat = new Orientation(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0,
                Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);
            public Point HexToPixel(Hexagon h)
            {
                Orientation M = orientation;
                double x = (M.f0 * h.x + M.f1 * h.y) * size.x;
                double y = (M.f2 * h.x + M.f3 * h.y) * size.y;
                return new Point(x + origin.x, y + origin.y);
            }
            public FractionalHex PixelToHex(Point p)
            {
                Orientation M = orientation;
                Point pt = new Point((p.x - origin.x) / size.x, (p.y - origin.y) / size.y);
                double x = M.b0 * pt.x + M.b1 * pt.y;
                double y = M.b2 * pt.x + M.b3 * pt.y;
                return new FractionalHex(x, y, -x - y);
            }
            public Point HexCornerOffset(int corner)
            {
                Orientation M = orientation;
                double angle = -2.0 * Math.PI * (M.startAngle - corner) / 6.0;
                return new Point(size.x * Math.Cos(angle), size.y * Math.Sin(angle));
            }
            public List<Point> PolygonCorners(Hexagon h)
            {
                List<Point> corners = new List<Point> { };
                Point center = HexToPixel(h);
                for (int i = 0; i < 6; i++)
                {
                    Point offset = HexCornerOffset(i);
                    corners.Add(new Point(center.x + offset.x, center.y + offset.y));
                }
                return corners;
            }
        }
    }

}


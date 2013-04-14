using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntownCarCounter
{
    class PointD
    {
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X
        { get; set; }
        public double Y
        { get; set; }

        public override string ToString()
        {
            return String.Format("({0:0.00}, {1:0.00})", X, Y);
        }

        public static PointD operator -(PointD a, PointD b)
        {
            return new PointD(a.X - b.X, a.Y - b.Y);
        }
        public static PointD operator +(PointD a, PointD b)
        {
            return new PointD(a.X + b.X, a.Y + b.Y);
        }

        public double Magnitude
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }
    }
}

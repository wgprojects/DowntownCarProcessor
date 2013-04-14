using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DowntownCarCounter
{
    /// <summary>
    /// Blips are points of input data - like blips on a radar screen.
    /// </summary>
    class Blip
    {
        public Blip(double _X, double _Y, int _time, int _area, int _id)
        {
            X = _X;
            Y = _Y;
            time = _time / 100d;
            area = (short)_area;
            id = (short)id;
        }

        public double X
        {
            get;
            set;
        }
        public double Y
        {
            get;
            set;
        }
        public double time
        {
            get;
            set;
        }
        public short area
        {
            get;
            set;
        }
        public short id
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("Blip {4} @ {2:0000.0}sec ({0:0.0}, {1:0.0}); area {3}", X, Y, time, area, id);
        }

        public static Velocity operator-(Blip a, Blip b)
        {
            return new Velocity(a.X - b.X, a.Y - b.Y, a.time - b.time);
        }
            
    }
}

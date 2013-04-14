using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntownCarCounter
{
    class Acceleration
    {


        public Acceleration(Velocity a, Velocity b, double _deltaTime)
        {
            if(_deltaTime < 0.5)
                _deltaTime = 0.5;

            deltaTime = _deltaTime;

            AccX = (a.VelX - b.VelX) / deltaTime;
            AccY = (a.VelY - b.VelY) / deltaTime;

        }

        double deltaTime;
        public double AccX
        { get; set; }
        public double AccY
        { get; set; }
        public double PolarAcc
        {
            get
            {
                return Math.Sqrt(AccX * AccX + AccY * AccY);
            }
        }
        public double PolarAngle
        {
            get
            {
                return Math.Atan2(AccX, AccY);
            }
        }

        //public PointD PositionChange()
        //{

        //    if (deltaTime == null)
        //        return null;
        //    else
        //        return new PointD(VelX * deltaTime.Value, VelY * deltaTime.Value);
                
        //}

        //public double DistanceTravelled
        //{
        //    get
        //    {
        //        if (deltaTime == null)
        //            return 0;
        //        else
        //            return Math.Sqrt(Math.Pow(VelX * deltaTime.Value, 2) + Math.Pow(VelY * deltaTime.Value, 2));
        //    }
        //}

        public override string ToString()
        {
            return String.Format("({0:0.00}, {1:0.00}) or {2:0.00}<{3:0.00}", AccX, AccY, PolarAcc, PolarAngle);
        }

        public static Velocity operator*(Acceleration a, double time)
        {
            return new Velocity(a.AccX * time, a.AccY * time);
        }
    }
}

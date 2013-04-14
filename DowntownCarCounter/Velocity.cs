using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntownCarCounter
{
    class Velocity
    {

        public Velocity(double deltaX, double deltaY, double _deltaTime)
        {
            deltaTime = _deltaTime;
            if(deltaTime < 0.5)
                deltaTime = 0.5;

            VelX = deltaX / deltaTime.Value;
            VelY = deltaY / deltaTime.Value;
        }
        public Velocity(double velX, double velY)
        {
            VelX = velX;
            VelY = velY;
        }

        public Velocity(Blip a, Blip b):this(a.X - b.X, a.Y - b.Y, a.time - b.time)
        {
           
        }

        double? deltaTime;
        public double VelX
        { get; set; }
        public double VelY
        { get; set; }
        public double PolarVel
        {
            get
            {
                return Math.Sqrt(VelX * VelX + VelY * VelY);
            }
        }
        public double PolarAngle
        {
            get
            {
                return Math.Atan2(VelX, VelY);
            }
        }

        public PointD PositionChange()
        {

            if (deltaTime == null)
                return null;
            else
                return new PointD(VelX * deltaTime.Value, VelY * deltaTime.Value);
                
        }

        public double DistanceTravelled
        {
            get
            {
                if (deltaTime == null)
                    return 0;
                else
                    return Math.Sqrt(Math.Pow(VelX * deltaTime.Value, 2) + Math.Pow(VelY * deltaTime.Value, 2));
            }
        }

        public override string ToString()
        {
            return String.Format("({0:0.00}, {1:0.00}) or {2:0.00}<{3:0.00}", VelX, VelY, PolarVel, PolarAngle);
        }

        public static Velocity operator-(Velocity a, Velocity b)
        {
            return new Velocity(a.VelX - b.VelX, a.VelY - b.VelY);
        }
        public static Velocity operator+(Velocity a, Velocity b)
        {
            return new Velocity(a.VelX + b.VelX, a.VelY + b.VelY);
        }
        public static Velocity operator/(Velocity a, double divisor)
        {
            return new Velocity(a.VelX / divisor, a.VelY / divisor);
        }
        public static PointD operator *(Velocity a, double time)
        {
            return new PointD(a.VelX * time, a.VelY * time);
        }
    }
}

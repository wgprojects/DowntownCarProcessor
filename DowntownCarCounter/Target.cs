using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntownCarCounter
{
    class Target
    {
        const double MAX_ACCEL = 10;//(3 * 9.81)*1; //units of 'pixels/s^2'. 3 gee acceleration, converted using an estimate of 30pix=3m.
        const double MAX_TELEPORT = 20; //units of 'pixels'. Blips are assumed to be incapable of teleporting further than this distance

        public Target(Blip start, int _targetID)
        {
            targetID = _targetID;

            //estimatedVel = null;
            firstBlip = start;
            lastBlip = firstBlip;

            Blips = new List<Blip>();
            Blips.Add(lastBlip);
        }

        public int targetID
        {
            get;
            private set;
        }
        //Velocity estimatedVel;

        public Blip firstBlip
        {
            get;
            private set;
        }
        public Blip lastBlip
        {
            get;
            private set;
        }
        public Velocity lastVelocity; //between lastBlip and the blip before that.
        public Acceleration lastAcceleration; //I know this is cheating, but oh well. Consider the Velocity class to be 'a higher derivative'.
        public List<Blip> Blips;
        public double newVelocityTime, lastVelocityTime;

        /// <summary>
        /// Returns a score indicating how likely it is that the 'testBlip' is a measurement of the target.
        /// </summary>
        /// <param name="newBlip"></param>
        /// <returns></returns>
        public double TestNewBlip(Blip testBlip)
        {
            PointD estPos = this.EstimatedPos(testBlip.time);
            PointD tb = new PointD(testBlip.X, testBlip.Y);
            PointD diff = estPos - tb;

            double dist = diff.Magnitude;

            if (dist >= MAX_TELEPORT)
                return double.MaxValue;

            //if (testBlip.time == lastBlip.time)
            //    dist *= 10;

            //return dist;

            Velocity testVelocity = testBlip - lastBlip;


            double testVelocityTime = (testBlip.time + lastBlip.time) / 2;

            Acceleration testAcceleration;
            if (lastVelocity == null || testVelocity == null)
                return dist; //Distance has to be the score because acceleration is undefined.
            else
                testAcceleration = new Acceleration(testVelocity, lastVelocity, testVelocityTime - lastVelocityTime);

            
            if (testAcceleration.PolarAcc > MAX_ACCEL)
                return double.MaxValue;

            return testAcceleration.PolarAcc;
            
            //double score = 0;

            //Velocity delta = lastBlip - testBlip;
            //if (delta.DistanceTravelled > MAX_TELEPORT)
            //    return double.MaxValue;
            //else
            //{
            //    double deltaT = testBlip.time - lastBlip.time;
            //    if (deltaT < 0.5)
            //        deltaT = 0.5;

            //    if (lastVelocity == null)
            //    {
            //        return delta.DistanceTravelled;
            //    }
            //    else
            //    {
            //        double accel = (delta - lastVelocity).PolarVel / deltaT;

            //        if (accel > MAX_ACCEL)
            //            return double.MaxValue;
            //        else
            //        {
            //            if (deltaT > 30)
            //                return double.MaxValue;
            //            else
            //                score += accel;
            //        }
            //    }
            //}

            ////TODO: Needs to take into account area and id!

            //return score;
        }

        //Given the last velocity and acceleration, where do we expect the new position to be at time t?
        public PointD EstimatedPos(double atTimeT)
        {
            PointD x1 = new PointD(lastBlip.X, lastBlip.Y);
            Velocity vInitial = lastVelocity;

            if (vInitial == null)
                return x1;

            double deltaT = atTimeT - lastBlip.time;

            if (lastAcceleration == null)
                return x1 + vInitial * deltaT;
            
            Velocity vFinal = lastVelocity + lastAcceleration * deltaT;

            Velocity vAvg = (vFinal + vInitial)/2;

            PointD x2 = x1 + vAvg * deltaT;

            return x2;
        }

        public void AddBlip(Blip newBlip)
        {
            

            Velocity newVelocity = newBlip - lastBlip;


            newVelocityTime = (newBlip.time + lastBlip.time) / 2;

            if (lastVelocity == null || newVelocity == null)
                lastAcceleration = null;
            else
                lastAcceleration = new Acceleration(newVelocity, lastVelocity, newVelocityTime - lastVelocityTime);

            lastVelocityTime = newVelocityTime;

            lastVelocity = newVelocity;

            Blips.Add(lastBlip);
            lastBlip = newBlip;
        }

        public Velocity AverageVel
        {
            get { return lastBlip - firstBlip; }
        }

        public override string ToString()
        {
            return String.Format("Target {0:000} ({4} blips) at {1} with current velocity {2}; average velocity {3}", targetID, lastBlip, lastVelocity, AverageVel, Blips.Count);
        }
    }
}

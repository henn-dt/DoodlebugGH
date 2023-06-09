using Illustrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illest
{
    public class Ill_PathPoint
    {

        
        private PathPoint pp;


        public Ill_PathPoint(PathPoint pathPoint)
        {
            pp = pathPoint;

            Anchor = new double[] { pp.Anchor[0], pp.Anchor[1] };
            Left = new double[] { pp.LeftDirection[0], pp.LeftDirection[1] };
            Right = new double[] { pp.RightDirection[0], pp.RightDirection[1] };
        }

        public Ill_PathPoint(double[] anchor, double[] left, double[] right)
        {
            Anchor = anchor;
            Left = left;
            Right = right;
        }



        public double[] Anchor
        {
            get; set;

        }


        public double[] Left
        {
            get; set;
        }

        public double[] Right
        {
            get; set;
        }


    }
}

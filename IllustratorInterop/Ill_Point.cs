using Illustrator;

namespace Illest
{
    public class Ill_Point
    {
        private double x, y;
        public Ill_Point(double x, double y)
        {
            this.x = x;
            this.y = y;

        }

        public double[] Point
        {
            get
            {
                return new double[] { x, y };
            }
        }
    }
}
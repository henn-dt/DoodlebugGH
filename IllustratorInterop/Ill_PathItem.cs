using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illustrator;

namespace Illest
{
    public class Ill_PathItem
    {
        private PathItem pathItem;
        public Ill_PathItem(PathItem item)
        {
            pathItem = item;
        }

        internal PathItem Path
        {
            get
            {
                return pathItem;
            }
        }

        public double strokeWidth
        {
            get
            {
                return pathItem.StrokeWidth;
            }
            set
            {
                pathItem.StrokeWidth = value;
            }
        }

        public bool IsClosed
        {
            get
            {
                return pathItem.Closed;
            }
        }

        public bool hasStroke
        {
            get
            {
                return pathItem.Stroked;
            }

            set
            {
                pathItem.Stroked = value;
            }
        }

        public bool hasFill
        {
            get
            {
                return pathItem.Filled;
            }

            set
            {
                pathItem.Filled = value;
            }
        }

        public System.Drawing.Color fillColor
        {
            get
            {
                return colorFromIllColor(pathItem.FillColor);
            }

            set
            {

                pathItem.FillColor = value.ToIllColor();
                pathItem.Opacity = ((double)value.A / (double)255) * 100;
            }
        }

        public System.Drawing.Color strokeColor
        {
            get
            {
                return colorFromIllColor(pathItem.StrokeColor);
            }

            set
            {

                pathItem.StrokeColor = value.ToIllColor();
            }
        }

        public List<Ill_PathPoint> PathPoints
        {
            get
            {
                var pointList = new List<Ill_PathPoint>();
                foreach (PathPoint p in pathItem.PathPoints)
                {
                    pointList.Add(new Ill_PathPoint(p));
                }
                return pointList;
            }
        }

        

        public System.Drawing.Color colorFromIllColor(object illColor)
        {
            return illColor.ToSysColor(pathItem.Opacity);
        }

    }
}

using Grasshopper.Kernel.Parameters;
using Illest;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IllestForGrasshopper
{
    public static class ConversionUtils
    {
        public static Polyline ToPolyLine(this Ill_PathItem pathItem)
        {
            var pointList = pathItem.PathPoints;
            Polyline P = new Polyline();
            for (int i = 0; i < pointList.Count; i++)
            {
                P.Add(pointList[i].Anchor.ToPoint3d());
            }
            if (pathItem.IsClosed) P.Add(pointList[0].Anchor.ToPoint3d());

            return P;
        }

        public static Point3d ToPoint3d(this double[] coords)
        {
            Point3d pt = new Point3d(coords[0], coords[1], 0);
            return pt;
        }

   

        public static List<List<double[]>> ToProtoPathPoints(this Curve C, double tolerance = 0.1, double angleTolerance = 0.1, bool closed = false)
        {
            List<List<double[]>> points = new List<List<double[]>>();

            BezierCurve[] bezierSpans = BezierCurve.CreateCubicBeziers(C, tolerance, angleTolerance);
            int count = bezierSpans.Length;
            if (closed)
            {

            }
            else
            {
                //add the first point
                BezierCurve firstSpan = bezierSpans[0];
                points.Add(new List<double[]>() { firstSpan.GetControlVertex3d(0).ToCoords(), firstSpan.GetControlVertex3d(0).ToCoords(), firstSpan.GetControlVertex3d(1).ToCoords() });
                for (int i = 1; i < count; i++)
                {
                    BezierCurve previousSpan = bezierSpans[i - 1];
                    BezierCurve span = bezierSpans[i];
                    points.Add(new List<double[]>() { span.GetControlVertex3d(0).ToCoords(), previousSpan.GetControlVertex3d(2).ToCoords(), span.GetControlVertex3d(1).ToCoords() });
                }
                BezierCurve lastSpan = bezierSpans[count - 1];
                points.Add(new List<double[]>() { lastSpan.GetControlVertex3d(3).ToCoords(), lastSpan.GetControlVertex3d(2).ToCoords(), lastSpan.GetControlVertex3d(3).ToCoords() });
            }


            return points;
        }


        public static List<Ill_PathPoint> ToPathPoints(this Curve C, double tolerance = 0.1, double angleTolerance = 0.1, bool closed = false)
        {
            List<Ill_PathPoint> points = new List<Ill_PathPoint>();

            BezierCurve[] bezierSpans = BezierCurve.CreateCubicBeziers(C, tolerance, angleTolerance);
            int count = bezierSpans.Length;
            if (closed)
            {

            }
            else
            {
                //add the first point
                BezierCurve firstSpan = bezierSpans[0];
                points.Add(new Ill_PathPoint(firstSpan.GetControlVertex3d(0).ToCoords(), firstSpan.GetControlVertex3d(0).ToCoords(), firstSpan.GetControlVertex3d(1).ToCoords()));
                for (int i = 1; i < count; i++)
                {
                    BezierCurve previousSpan = bezierSpans[i - 1];
                    BezierCurve span = bezierSpans[i];
                    points.Add(new Ill_PathPoint(span.GetControlVertex3d(0).ToCoords(), previousSpan.GetControlVertex3d(2).ToCoords(), span.GetControlVertex3d(1).ToCoords()));
                }
                BezierCurve lastSpan = bezierSpans[count - 1];
                points.Add(new Ill_PathPoint(lastSpan.GetControlVertex3d(3).ToCoords(), lastSpan.GetControlVertex3d(2).ToCoords(), lastSpan.GetControlVertex3d(3).ToCoords()));
            }


            return points;
        }

        public static Ill_Point ToPoint(this Point3d p)
        {
            return new Ill_Point(p.X, p.Y);
        }



        public static double[] ToCoords(this Point3d p)
        {
            return new double[] { p.X, p.Y };
        }

        public static List<object[]> ToPolylineCoords(this Polyline P)
        {
            List<object[]> coords = new List<object[]>();
            foreach (Point3d p in P)
            {
                coords.Add(new object[] { p.X, p.Y });
            }
            return coords;
        }

        public static Curve[] ToCurve(this Ill_PathItem pathItem)
        {

            List<NurbsCurve> segments = new List<NurbsCurve>();
            List<Ill_PathPoint> pointList = pathItem.PathPoints;
            if (pathItem.IsClosed)
            {
                for (int j = 0; j < pointList.Count; j++)
                {
                    int i = j % pointList.Count;
                    NurbsCurve nurbsCurve = new NurbsCurve(3, 4);
                    nurbsCurve.Points.SetPoint(0, pointList[i].Anchor.ToPoint3d());
                    nurbsCurve.Points.SetPoint(1, pointList[i].Right.ToPoint3d());
                    nurbsCurve.Points.SetPoint(2, pointList[(i + 1) % pointList.Count].Left.ToPoint3d());
                    nurbsCurve.Points.SetPoint(3, pointList[(i + 1) % pointList.Count].Anchor.ToPoint3d());

                    nurbsCurve.Knots.CreateUniformKnots(1.0);
                    segments.Add(nurbsCurve);
                }
            }
            else
            {
                for (int i = 0; i < pointList.Count - 1; i++)
                {
                    NurbsCurve nurbsCurve = new NurbsCurve(3, 4);
                    nurbsCurve.Points.SetPoint(0, pointList[i].Anchor.ToPoint3d());
                    nurbsCurve.Points.SetPoint(1, pointList[i].Right.ToPoint3d());

                    nurbsCurve.Points.SetPoint(2, pointList[i + 1].Left.ToPoint3d());
                    nurbsCurve.Points.SetPoint(3, pointList[i + 1].Anchor.ToPoint3d());
                    nurbsCurve.Knots.CreateUniformKnots(1.0);
                    segments.Add(nurbsCurve);
                }
            }

            return Curve.JoinCurves(segments);
        }
    }
}

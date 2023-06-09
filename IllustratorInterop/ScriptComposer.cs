using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illest
{
    public static class ScriptComposer
    {
        public static string AddPolylinePathItem(string documentName, string layerName, List<object[]> polylineCoords)
        {
            string script = "";
            script += SetDocument(documentName);
            script += SetLayer(layerName);
            script += "var piRef = lay.pathItems;\n";
            script += "var pathRef = piRef.add();\n";
            script += "pathRef.setEntirePath(new Array(";
            foreach (object[] polylineCoord in polylineCoords)
            {
                script += string.Format("new Array({0},{1}),", polylineCoord[0], polylineCoord[1]);
            }
            script += "));";
            return script;
        }



        public static string AddBezierPathItem(string documentName, string layerName, List<List<double[]>> bezierCoords)
        {
            string script = "";
            script += SetDocument(documentName);
            script += SetLayer(layerName);
            script += "var piRef = lay.pathItems;\n";
            script += "var pathRef = piRef.add();\n";

            foreach (List<double[]> PathPoint in bezierCoords)
            {
                script += "var point = pathRef.pathPoints.add();\n";
                script += string.Format("point.anchor = [{0},{1}];\n", PathPoint[0][0], PathPoint[0][1]);
                script += string.Format("point.leftDirection = [{0},{1}];\n", PathPoint[1][0], PathPoint[1][1]);
                script += string.Format("point.rightDirection = [{0},{1}];\n", PathPoint[2][0], PathPoint[2][1]);
            }

            return script;
        }

        public static string setPathProperty(string propertyName, object value)
        {
            return string.Format("pathRef.{0} = {1};\n", propertyName, value);
        }

        public static string defineColor(System.Drawing.Color color, string colorName)
        {
            string script = string.Format("var {0} = new RGBColor();\n", colorName);
            script += string.Format("{0}.red = {1};\n", colorName, color.R);
            script += string.Format("{0}.green = {1};\n", colorName, color.G);
            script += string.Format("{0}.blue = {1};\n", colorName, color.B);
            return script;
        }

        public static string SetLayer(string layerName)
        {
            return string.Format("var lay = doc.layers.getByName(\"{0}\");\n", layerName);
        }

        public static string SetDocument(string documentName)
        {
            return string.Format("var doc = app.documents.getByName(\"{0}\");\n", documentName);

        }
    }
}

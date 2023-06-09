using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illustrator;

namespace Illest
{
    public enum BlendMode
    {
        NormalBlend = 0,
        Multiply = 1,
        Screen = 2,
        Overlay = 3,
        SoftLight = 4,
        HardLight = 5,
        ColorDodge = 6,
        ColorBurn = 7,
        Darken = 8,
        Lighten = 9,
        Difference = 10,
        Exclusion = 11,
        Hue = 12,
        SaturationBlend = 13,
        ColorBlend = 14,
        Luminosity = 15
    }

    public class Ill_Layer
    {
        private Layer layer;

        public Ill_Layer(Layer lay)
        {
            layer = lay;

        }

        public BlendMode BlendMode
        {
            get
            {
                return (BlendMode)layer.BlendingMode;
            }
            set
            {

                layer.BlendingMode = (AiBlendModes)value;
            }
        }

        public void SendToBack()
        {
            layer.ZOrder(AiZOrderMethod.aiSendToBack);
        }

        public void BringToFront()
        {
            layer.ZOrder(AiZOrderMethod.aiBringToFront);
        }

        public List<Ill_PathItem> PathItems()
        {
            List<Ill_PathItem> items = new List<Ill_PathItem>();
            foreach (var item in layer.PathItems)
            {
                items.Add(new Ill_PathItem(item as PathItem));
            }
            return items;
        }

        public List<Ill_TextItem> TextItems()
        {
            List<Ill_TextItem> items = new List<Ill_TextItem>();
            foreach (var item in layer.TextFrames)
            {
                items.Add(new Ill_TextItem(item as TextFrame));
            }

            return items;
        }

        public double Opacity
        {
            get
            {
                return layer.Opacity;
            }
            set
            {
                layer.Opacity = value;
            }
        }

        public string LayerName { get { return layer.Name; } }


        public Ill_PathItem AddPolylinePathItem(List<object[]> coords, bool closed = false)
        {
            PathItem pi = layer.PathItems.Add();
            object[] coordArray = closed ? new object[coords.Count + 1] : new object[coords.Count];
            for (int i = 0; i < coordArray.Length; i++)
            {
                coordArray[i] = coords[i % coords.Count];
            }
            pi.SetEntirePath(coordArray);
            return new Ill_PathItem(pi);
        }

        public Ill_PathItem AddPathItem(List<Ill_PathPoint> pathPoints, bool closed = false)
        {
            PathItem pi = layer.PathItems.Add();
            foreach (Ill_PathPoint pp in pathPoints)
            {
                PathPoint ipp = pi.PathPoints.Add();

                ipp.Anchor = new object[] { pp.Anchor[0], pp.Anchor[1] };
                ipp.LeftDirection = new object[] { pp.Left[0], pp.Left[1] };
                ipp.RightDirection = new object[] { pp.Right[0], pp.Right[1] };

            }


            return new Ill_PathItem(pi);
        }

        public Ill_TextItem AddTextItem(Ill_Point location)
        {
            var textFrame = layer.TextFrames.Add();
            textFrame.Position = new object[] { location.Point[0], location.Point[1] };
            return new Ill_TextItem(textFrame);
        }

        public Ill_TextItem AddTextItem(Ill_Point location, string content)
        {
            var textFrame = layer.TextFrames.Add();
            textFrame.Position = new object[] { location.Point[0], location.Point[1] };
            var text = new Ill_TextItem(textFrame);
            text.Content = content;
            return text;
        }

        public Ill_TextItem AddTextItem(Ill_Point corner1, Ill_Point corner2)
        {
            var illRect = AddPolylinePathItem(new List<object[]>()
            {
                new object[] {corner1.Point[0],corner1.Point[1]},
                new object[] {corner2.Point[0],corner1.Point[1]},
                new object[] {corner2.Point[0],corner2.Point[1]},
                new object[] {corner1.Point[0],corner2.Point[1]}
            }, true);
            var textFrame = layer.TextFrames.AreaText(illRect.Path);
            // textFrame.Position = new object[] { corner1.Point[0], corner1.Point[1] };
            return new Ill_TextItem(textFrame);
        }

        public Ill_Application Application
        {
            get
            {
                return new Ill_Application(layer.Application);
            }
        }
          
     

        public Ill_TextItem AddTextItem(Ill_Point corner1, Ill_Point corner2, string content)
        {
            var text = AddTextItem(corner1, corner2);
            text.Content = content;
            return text;
        }

        public Ill_Document Document()
        {

            Layer tempLayer = layer;
            if (tempLayer.Parent is Document)
            {
                return new Ill_Document(tempLayer.Parent as Document);
            }
            while (!(tempLayer.Parent is Document))
            {
                tempLayer = tempLayer.Parent as Layer;
            }
            return new Ill_Document(tempLayer.Parent as Document);

        }

        public void ClearPaths()
        {
            layer.PathItems.RemoveAll();
        }
        public void ClearText()
        {
            layer.TextFrames.RemoveAll();
        }

        public override string ToString()
        {
            return LayerName;
        }
    }
}

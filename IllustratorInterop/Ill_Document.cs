using Illustrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illest
{
    public enum Units
    {
        Pixels,
        Inches,
        Points,
        Centimeters,
        Millimeters,
        Picas

    }

    public class Ill_Document
    {
        Document illDoc;
        public Ill_Document(Document doc)
        {
            illDoc = doc;
        }


        public List<Ill_Layer> Layers
        {
            get
            {
                List<Ill_Layer> layers = new List<Ill_Layer>();
                foreach (Layer l in illDoc.Layers)
                {
                    layers.Add(new Ill_Layer(l));
                }
                return layers;
            }
        }

        public Units UnitSystem
        {
            get
            {
                switch (illDoc.RulerUnits)
                {
                    case AiRulerUnits.aiUnitsCM:
                        return Units.Centimeters;
                    case AiRulerUnits.aiUnitsInches:
                        return Units.Inches;
                    case AiRulerUnits.aiUnitsMM:
                        return Units.Millimeters;
                    case AiRulerUnits.aiUnitsPicas:
                        return Units.Picas;
                    case AiRulerUnits.aiUnitsPixels:
                        return Units.Picas;
                    case AiRulerUnits.aiUnitsPoints:
                    case AiRulerUnits.aiUnitsQ:
                    case AiRulerUnits.aiUnitsUnknown:
                    default:
                        return Units.Points;
                }
            }


        }

        public List<double[]> Corners()
        {
            List<double[]> corners = new List<double[]>();
            var artboardEnumerator = illDoc.Artboards.GetEnumerator();
            artboardEnumerator.MoveNext();
            Artboard ab = artboardEnumerator.Current as Artboard;
            object[] rect = ab.ArtboardRect;
            corners.Add(new double[] { (double)rect[0], (double)rect[1] });
            corners.Add(new double[] { (double)rect[2], (double)rect[3] });
            return corners;
        }

        public void SetLayerOrder(List<int> order)
        {

            if (order.Count != illDoc.Layers.Count) return;
            List<Ill_Layer> reordered = order.Select(i => Layers[i]).ToList();

            foreach (Ill_Layer lay in reordered)
            {
                lay.BringToFront();
            }

        }

        public void AddLayers(List<string> layersToAdd)
        {
            var layers = Layers;
            foreach(string layerToAdd in layersToAdd)
            {
                int numFound = layers.Where(l => l.LayerName == layerToAdd).Count();
                if (numFound == 0)
                {
                    Layer lay = illDoc.Layers.Add();
                    lay.Name = layerToAdd;
                }
            }

        }

        public void AddLayer(string layerToAdd)
        {
            int numFound = Layers.Where(l => l.LayerName == layerToAdd).Count();
            if (numFound == 0)
            {
                Layer lay = illDoc.Layers.Add();
                lay.Name = layerToAdd;
            }
        }

        public Ill_Layer GetLayerByName(string name)
        {
            return Layers.Find(l => l.LayerName == name);
        }

        public void SetLayerOrder(List<int> order, List<string> names)
        {

            if (order.Count != Layers.Count || names.Count != Layers.Count) return;
            List<string> reorderedNames = order.Select(i => names[i]).ToList();
            List<Ill_Layer> reordered = reorderedNames.Select(name => Layers.Find(l => l.LayerName == name)).ToList();
            foreach (Ill_Layer lay in reordered)
            {
                lay.BringToFront();
            }

        }

        public void Export(string fileType, string filePath)
        {
            illDoc.Application.ActiveDocument = illDoc;
            AiExportType exportType = AiExportType.aiJPEG;
            object exportOptions = null;
            switch (fileType.ToUpper())
            {
                case "JPG":
                case "JPEG":
                    exportType = AiExportType.aiJPEG;
                    break;
                case "PNG":
                case "PNG24":
                    exportType = AiExportType.aiPNG24;
                    break;
                case "SVG":
                    exportType = AiExportType.aiSVG;
                    break;
                case "AUTOCAD":
                case "DWG":
                    exportType = AiExportType.aiAutoCAD;
                    break;
                case "TIFF":
                case "TIF":
                    exportType = AiExportType.aiTIFF;
                    break;

            }
            if (exportOptions == null)
            {
                illDoc.Export(filePath, exportType);
            }
            else
            {
                illDoc.Export(filePath, exportType, exportOptions);
            }
        }

        public void SetLayerOrder(List<string> names)
        {

            if (names.Count != illDoc.Layers.Count) return;

            List<Ill_Layer> reordered = names.Select(name => Layers.Find(l => l.LayerName == name)).ToList();
            foreach (Ill_Layer lay in reordered)
            {
                lay.BringToFront();
            }

        }



        public List<string> LayerNames
        {
            get
            {
                return Layers.Select(l => l.LayerName).ToList();
            }
        }

        public void Close()
        {
            illDoc.Close();
        }

        public void Close(bool saving)
        {
            AiSaveOptions saveOptions = saving ? AiSaveOptions.aiSaveChanges : AiSaveOptions.aiDoNotSaveChanges;
            illDoc.Close(saveOptions);
        }

        public override string ToString()
        {
            return illDoc.Name;
        }
    }
}

using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Grasshopper.Kernel.Parameters;

namespace IllestForGrasshopper
{
    public class CreateText : GH_Component
    {

        private List<string> clearedLayers;
        /// <summary>
        /// Initializes a new instance of the CreateText class.
        /// </summary>
        public CreateText()
          : base("Create Text on Layer", "CreateText",
              "Use this component to create text items on a layer. Specify points for point text, or a rectangle for area text.",
              "Doodlebug", "Doodlebug")
        {
            clearedLayers = new List<string>();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "The layer to which to add the text", GH_ParamAccess.item);
            pManager.AddTextParameter("Text", "T", "The text content for the text item", GH_ParamAccess.item);
            pManager.AddGeometryParameter("Location", "Lo", "The location for the text. Supply a point for point text or a rectangle for area text.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Size", "S", "The text size", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddTextParameter("Font", "F", "The font to use. A closest-match search will be used - illustrator fonts take the format Name-Style like \"Arial-Regular\".", GH_ParamAccess.item);
            pManager[4].Optional = true;
            pManager.AddGenericParameter("Text Color", "C", "Color of the text", GH_ParamAccess.item);
            pManager[5].Optional = true;
            pManager.AddIntegerParameter("Justification", "J", "The text justification", GH_ParamAccess.item);
            pManager[6].Optional = true;
            Param_Integer justParam = pManager[6] as Param_Integer;
            justParam.AddNamedValue("Left", 0);
            justParam.AddNamedValue("Center", 2);
            justParam.AddNamedValue("Right", 1);
            justParam.AddNamedValue("Justify", 6);
            pManager.AddBooleanParameter("Delete On Layer", "D", "Before adding elements, delete everything on the layer", GH_ParamAccess.item, false);


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Text Item", "TI", "The text item created", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Layer lay = null;
            string text = "";
            object geo = null;
            double size = -1;
            string font = "";
            Color textCol = Color.Transparent;
            bool delete = false;
            int justification = -1;


            if (!DA.GetData("Layer", ref lay)) return;
            if (!DA.GetData("Text", ref text)) return;
            if (!DA.GetData("Location", ref geo)) return;
            DA.GetData<bool>("Delete On Layer", ref delete);

            if(DA.Iteration == 0)
            {
                clearedLayers.Clear();
            }


            if (delete && !clearedLayers.Contains(lay.LayerName))
            {
                lay.ClearText();
                clearedLayers.Add(lay.LayerName);

            }


            bool hasSize = DA.GetData("Size", ref size);
            bool hasFont = DA.GetData("Font", ref font);
            bool hasColor = DA.GetData("Text Color", ref textCol);
            bool hasJustification = DA.GetData("Justification", ref justification);
            Ill_TextItem item = null;
            if(geo is GH_Point)
            {
                Point3d pt = ((GH_Point)geo).Value;
                item = lay.AddTextItem(pt.ToPoint(), text);
            } else if (geo is GH_Rectangle || geo is GH_Curve)
            {
                BoundingBox bbox = ((IGH_GeometricGoo)geo).Boundingbox;
               item = lay.AddTextItem(new Ill_Point(bbox.Min.X,bbox.Min.Y), new Ill_Point(bbox.Max.X, bbox.Max.Y), text);
            } else
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "This geometry type is not recognized for location. Supply a point or a rectangle.");
            }
            if(item != null)
            {
                if (hasSize)
                {
                    item.Size = size;
                }
                if (hasFont)
                {
                    item.Font = font;
                }
                if (hasColor)
                {
                    item.Color = textCol;
                }
                if (hasJustification)
                {
                    item.Justification = (Ill_Justification)justification;
                }
            }
            DA.SetData("Text Item", item);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.TextToIllustrator;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3d634a2d-9e0f-4e6d-af19-d2e04f8c007e}"); }
        }
    }
}
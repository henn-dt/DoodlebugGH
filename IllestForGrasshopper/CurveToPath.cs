using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Illest;

namespace IllestForGrasshopper
{
    public class CurveToPath : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GeometryToPath class.
        /// </summary>
        public CurveToPath()
          : base("Curve To Path", "CrvToPath",
              "Convert a Rhino Curve to a bezier curve path in Illustrator",
              "Doodlebug", "Doodlebug")
        {
            clearedLayers = new List<string>();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "The layer to which to add the path", GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve", "C", "Curve to convert", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Stroked", "S", "True if the path has a stroke", GH_ParamAccess.item);
            pManager.AddGenericParameter("Stroke Color", "SC", "Color of the stroke", GH_ParamAccess.item);
            pManager.AddNumberParameter("Stroke Weight", "SW", "Weight of the stroke", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Filled", "F", "True if the path has a fill", GH_ParamAccess.item);
            pManager.AddGenericParameter("Fill Color", "FC", "Color of the fill", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Delete On Layer", "D", "Before adding elements, delete everything on the layer", GH_ParamAccess.item,false);
            for (int i = 2; i < pManager.ParamCount-1; i++)
            {
                pManager[i].Optional = true;
            }


        }

        private List<string> clearedLayers;

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Path", "P", "The created path item", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve C = null;
            Ill_Layer lay = null;
            bool stroked = false;
            bool filled = false;
            double strokeweight = 0;
            Color fillCol = Color.Transparent;
            Color strokeCol = Color.Transparent;
            bool delete = false;

            if (!DA.GetData<Curve>("Curve", ref C)) return;
            if (!DA.GetData<Ill_Layer>("Layer", ref lay)) return;
            DA.GetData<bool>("Delete On Layer",ref delete);


            if (DA.Iteration == 0)
            {
                clearedLayers.Clear();
            }


            if (delete && !clearedLayers.Contains(lay.LayerName))
            {
                lay.ClearPaths();
                clearedLayers.Add(lay.LayerName);

            }

            Ill_PathItem p = null;
            if (C.IsPolyline())
            {
                Polyline PL = null;
                if(C.TryGetPolyline(out PL))
                {
                    List<object[]> polylineCoords = PL.ToPolylineCoords();
                    
                    p = lay.AddPolylinePathItem(polylineCoords);
                }
               
            }
            else {
                List<Ill_PathPoint> points = C.ToPathPoints();

                p = lay.AddPathItem(points);
            }

            if(p == null)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,"Something went wrong in conversion.");
                return;
            }

            if (DA.GetData<bool>("Stroked", ref stroked))
            {
                p.hasStroke = stroked;
            }
            if (DA.GetData<bool>("Filled", ref filled))
            {
                p.hasFill = filled;
            }
            if (DA.GetData<double>("Stroke Weight", ref strokeweight))
            {
                p.strokeWidth = strokeweight;

            }
            if (DA.GetData<Color>("Stroke Color", ref strokeCol))
            {
                p.strokeColor = strokeCol;
            }

            if (DA.GetData<Color>("Fill Color", ref fillCol))
            {
                p.fillColor = fillCol;
            }
            DA.SetData("Path", p);
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
                return Properties.Resources.CurveToPath;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{34f73e70-da2f-4253-9505-c8d93feb05eb}"); }
        }
    }
}
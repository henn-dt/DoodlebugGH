using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class PathItemGeometry : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PathItemGeometry class.
        /// </summary>
        public PathItemGeometry()
          : base("Path Geometry", "PathGeom",
              "Get the curve / polyline geometry of the path items",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Path", "P", "The Path item to process", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Straight", "S", "If true, will be interpreted as a polyline, otherwise curves will be taken into account", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Curves", "C", "The resulting path geometry", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_PathItem pi = null;
            if (!DA.GetData<Ill_PathItem>("Path", ref pi)) return;
            bool ProcessAsPolyline = false;
            if (!DA.GetData<bool>("Straight", ref ProcessAsPolyline)) return;

            if (ProcessAsPolyline)
            {
                PolylineCurve p = new PolylineCurve(pi.ToPolyLine());
                DA.SetDataList("Curves", new List<Curve>() { p });
            } else
            {
                Curve[] c = pi.ToCurve();
                DA.SetDataList("Curves", c);
            }

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
                return Properties.Resources.PathGeometry;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{49a94823-c0e2-4a2f-b3cc-b1500b59720e}"); }
        }
    }
}
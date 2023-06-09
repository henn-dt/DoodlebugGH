using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;
using System.Drawing;

namespace IllestForGrasshopper
{
    public class SetPathItemProperties : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SetPathItemProperties class.
        /// </summary>
        public SetPathItemProperties()
          : base("Set Path Properties", "SetPathProp",
              "Set the display properties of Paths",
             "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Path Items", "P", "Path items to modify", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Stroked", "S", "True if the path has a stroke", GH_ParamAccess.item);
            pManager.AddGenericParameter("Stroke Color", "SC", "Color of the stroke", GH_ParamAccess.item);
            pManager.AddNumberParameter("Stroke Weight", "SW", "Weight of the stroke", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Filled", "F", "True if the path has a fill", GH_ParamAccess.item);
            pManager.AddGenericParameter("Fill Color", "FC", "Color of the fill", GH_ParamAccess.item);
            for(int i = 1; i < pManager.ParamCount; i++)
            {
                pManager[i].Optional = true;
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Path Items", "P", "The modified path items", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_PathItem p = null;
            bool stroked = false;
            bool filled = false;
            double strokeweight = 0 ;
            Color fillCol = Color.Transparent;
            Color strokeCol = Color.Transparent;
            if (!DA.GetData<Ill_PathItem>("Path Items", ref p)) return;
            if(DA.GetData<bool>("Stroked", ref stroked))
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
                p.fillColor = fillCol ;
            }
            DA.SetData("Path Items", p);
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
                return Properties.Resources.SetPathItemProperties;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{17b79606-2e5a-4a7c-9fb9-58d67ee0ffde}"); }
        }
    }
}
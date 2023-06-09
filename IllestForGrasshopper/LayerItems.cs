using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class LayerItems : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the LayerItems class.
        /// </summary>
        public LayerItems()
          : base("Get Paths on Layer", "LayPaths",
              "Gets the Path items on a layer",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "The layer to modify", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Path Items", "P", "Path items on the layer", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Stroked", "S", "True if the path has a stroke", GH_ParamAccess.list);
            pManager.AddGenericParameter("Stroke Color", "SC", "Color of the stroke", GH_ParamAccess.list);
            pManager.AddNumberParameter("Stroke Weight", "SW", "Weight of the stroke", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Filled", "F", "True if the path has a fill", GH_ParamAccess.list);
            pManager.AddGenericParameter("Fill Color", "FC", "Color of the fill", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Layer lay = null;
            if (!DA.GetData<Ill_Layer>("Layer", ref lay)) return;
            List<Ill_PathItem> pathItems = lay.PathItems();
            DA.SetDataList("Path Items", pathItems);
            DA.SetDataList("Stroked", pathItems.Select(p => p.hasStroke));
            DA.SetDataList("Stroke Color", pathItems.Select(p => p.strokeColor));
            DA.SetDataList("Stroke Weight",  pathItems.Select(p => p.strokeWidth));
            DA.SetDataList("Filled", pathItems.Select(p => p.hasFill));
            DA.SetDataList("Fill Color", pathItems.Select(p => p.fillColor));
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
                return Properties.Resources.LayerItems;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{1e11923b-f911-4e62-8e33-e73ef49a8d2f}"); }
        }
    }
}
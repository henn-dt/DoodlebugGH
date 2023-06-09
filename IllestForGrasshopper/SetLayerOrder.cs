using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class SetLayerOrder : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SetLayerOrder class.
        /// </summary>
        public SetLayerOrder()
          : base("SetLayerOrder", "LayOrder",
              "Set the desired layer order for a document",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The document", GH_ParamAccess.item);
            pManager.AddTextParameter("Layer Order", "L", "A list of layer names, in the desired order (from bottom to top)", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Success", "S", "True on success", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Document doc = null;
            if (!DA.GetData<Ill_Document>("Document", ref doc)) return;
            List<string> desiredLayerOrder = new List<string>();
            if (!DA.GetDataList<string>("Layer Order", desiredLayerOrder)) return;

            doc.SetLayerOrder(desiredLayerOrder);
            DA.SetData("Success", true);
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
                return Properties.Resources.SetLayerOrder;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{ac8aa377-6f09-4690-ac57-acf3091ee7df}"); }
        }
    }
}
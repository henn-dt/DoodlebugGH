using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class Documents : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ActiveDocument class.
        /// </summary>
        public Documents()
          : base("Documents", "Docs",
              "Get Open Illustrator Documents",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Application", "A", "The running instance of Illustrator", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Documents", "D", "All Open Documents", GH_ParamAccess.list);
            pManager.AddGenericParameter("Active Document", "AD", "The active document", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Application app = null;
            if (!DA.GetData<Ill_Application>("Application", ref app))
            {
                return;
            }

            DA.SetDataList("Documents", app.Documents);
            DA.SetData("Active Document", app.ActiveDocument);

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
                return Properties.Resources.Documents;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{16f85b29-5275-4671-a83a-18bd88a928d8}"); }
        }
    }
}
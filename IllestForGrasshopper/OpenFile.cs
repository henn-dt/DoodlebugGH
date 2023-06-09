using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class OpenFile : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the OpenFile class.
        /// </summary>
        public OpenFile()
          : base("OpenFile", "OpenFile",
              "Open an Illustrator Document",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Application", "A", "The running instance of Illustrator", GH_ParamAccess.item);
            pManager.AddTextParameter("FilePath", "F", "The file to open", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "W", "The optional width (in inches) of the file to open", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Height", "H", "The optional height (in inches) of the file to open", GH_ParamAccess.item);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The newly opened document", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Application app = null;
            if (!DA.GetData<Ill_Application>("Application", ref app)) return;

            string filePath = "";
            if (!DA.GetData<string>("FilePath", ref filePath)) return;

            double width = -1;
            double height = -1;
            bool hasSize = DA.GetData<double>("Width", ref width) && !DA.GetData<double>("Height", ref height);
            if (hasSize)
            {
                DA.SetData("Document", app.OpenWithSize(filePath, width, height));
            }
            else
            {
                DA.SetData("Document", app.Open(filePath));

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
                return Properties.Resources.OpenFile;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{7f7987ab-267f-4ff8-b02f-90c59c6831b1}"); }
        }
    }
}
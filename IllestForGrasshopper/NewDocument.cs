using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class NewDocument : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the NewDocument class.
        /// </summary>
        public NewDocument()
          : base("NewDocument", "NewDoc",
             "Create a new Illustrator Document",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Application", "A", "The running instance of Illustrator", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "The document name", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "W", "The optional width (in inches) of the file to create", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "The optional height (in inches) of the file to create", GH_ParamAccess.item);
  
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The created document", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Application app = null;
            if (!DA.GetData<Ill_Application>("Application", ref app)) return;

            string name = "";
            if (!DA.GetData<string>("Name", ref name)) return;

            double width = -1;
            double height = -1;
            bool hasSize = DA.GetData<double>("Width", ref width) && DA.GetData<double>("Height", ref height);
            if (!hasSize) return;


            DA.SetData("Document", app.AddDocumentInches("Preset", name, width, height));
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
                return Properties.Resources.New_Document;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{d1799c09-1b56-43a0-af46-ea90886c28f0}"); }
        }
    }
}
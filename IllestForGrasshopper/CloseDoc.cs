using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class CloseDoc : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CloseDoc class.
        /// </summary>
        public CloseDoc()
          : base("Close Document", "CloseDoc",
              "Close a specified document",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The document", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Save", "S", "Optional boolean to tell the document to save or not", GH_ParamAccess.item);
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
            bool save = false;
            bool hasSave = DA.GetData<bool>("Save", ref save);

            if (hasSave)
            {
                doc.Close(save);
            } else
            {
                doc.Close();
            }
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
                return Properties.Resources.CloseDocument;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{a5acd9c3-31aa-4dd7-a83f-4d340f54a83a}"); }
        }
    }
}
using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Illest;

namespace IllestForGrasshopper
{
    public class ExportDocument : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ExportDocument class.
        /// </summary>
        public ExportDocument()
          : base("Export Document", "Export",
              "Export a document to a specified location",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The document to export", GH_ParamAccess.item);
            pManager.AddIntegerParameter("File Type", "T", "The file type to export", GH_ParamAccess.item, 0);
            Param_Integer pi = pManager[1] as Param_Integer;
            pi.AddNamedValue("JPG", 0);
            pi.AddNamedValue("PNG", 1);
            pi.AddNamedValue("TIF", 2);
            pi.AddNamedValue("DWG", 3);
            pi.AddNamedValue("SVG", 4);
            pManager.AddTextParameter("File Path", "FP", "The location to save the file", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "The path of the saved file", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Document doc = null;
            DA.GetData("Document", ref doc);
            int fileType = -1;
            DA.GetData("File Type", ref fileType);
            string fileTypeName = "Not Found";
            string[] fileTypeNames = new string[] { "JPG", "PNG", "TIF", "DWG", "SVG" };
            fileTypeName = fileTypeNames[fileType];
            string filePath = "";
            DA.GetData("File Path", ref filePath);
            doc.Export(fileTypeName, filePath);
            DA.SetData("Path", filePath);

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
                return Properties.Resources.ExportDocument;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{006f1234-5717-4d33-97e6-0765b09fe315}"); }
        }
    }
}
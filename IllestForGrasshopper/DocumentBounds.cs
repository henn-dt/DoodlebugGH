using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class DocumentBounds : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DocumentBounds class.
        /// </summary>
        public DocumentBounds()
          : base("Document Bounds", "DocBounds",
              "Get the rectangle representing the document bounds",
              "Doodlebug","Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The document", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddRectangleParameter("Bounds", "B", "The bounds of the document", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Document doc = null;
            if (!DA.GetData<Ill_Document>("Document", ref doc)) return;
            var corners = doc.Corners();
            Rectangle3d r = new Rectangle3d(Plane.WorldXY, corners[0].ToPoint3d(), corners[1].ToPoint3d());

            DA.SetData("Bounds", r);
           
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
                return Properties.Resources.docBounds;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{ff372992-ac5a-4f9a-9272-3f4a248ee9f3}"); }
        }
    }
}
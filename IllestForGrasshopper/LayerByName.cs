using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class LayerByName : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the LayerByName class.
        /// </summary>
        public LayerByName()
          : base("Layer By Name", "LayByName",
              "Get a layer from a document by name",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The document", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "The name of the layer to retrieve", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "The layer if found", GH_ParamAccess.item);
            pManager.AddGenericParameter("Blend Mode", "BM", "The blend mode of the layer", GH_ParamAccess.item);
            pManager.AddGenericParameter("Opacity", "O", "The opacity of the layer", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Document doc = null;
            if (!DA.GetData<Ill_Document>("Document", ref doc)) return;
            string name = "";
            if (!DA.GetData<string>("Name", ref name)) return;

            Ill_Layer lay = doc.GetLayerByName(name);
            DA.SetData("Layer", lay );
            DA.SetData("Blend Mode", lay.BlendMode);
            DA.SetData("Opacity", lay.Opacity);
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
                return Properties.Resources.LayerByName;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{c0f18380-fc2e-4bdf-8965-ca1e4beb1a8f}"); }
        }
    }
}
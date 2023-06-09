using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class Layers : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Layers class.
        /// </summary>
        public Layers()
          : base("Layers", "Layers",
              "Get/Create Document Layers",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Document", "D", "The document", GH_ParamAccess.item);
            pManager.AddTextParameter("Layers To Add", "L", "An optional list of layers to add to the document", GH_ParamAccess.list);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layers", "L", "The document Layers", GH_ParamAccess.list);
            pManager.AddGenericParameter("Blend Modes", "BM", "The blend modes of the layers", GH_ParamAccess.list);
            pManager.AddNumberParameter("Opacities", "O", "The opacities of the layers", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> layersToAdd = new List<string>();
            Ill_Document doc = null;
            if (!DA.GetData<Ill_Document>("Document", ref doc)) return;
            bool layersSupplied = DA.GetDataList("Layers To Add", layersToAdd);
            if (layersSupplied)
            {
                doc.AddLayers(layersToAdd);
            }
            DA.SetDataList("Layers", doc.Layers);
            DA.SetDataList("Blend Modes", doc.Layers.Select(l => l.BlendMode));
            DA.SetDataList("Opacities", doc.Layers.Select(l => l.Opacity));

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
                return Properties.Resources.Layers;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{d04c9faa-d453-4632-9c44-35988cf43cb7}"); }
        }
    }
}
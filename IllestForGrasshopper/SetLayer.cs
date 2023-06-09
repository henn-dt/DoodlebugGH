using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Illest;

namespace IllestForGrasshopper
{
    public class SetLayer : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SetLayer class.
        /// </summary>
        public SetLayer()
          : base("Set Layer Properties", "SetLay",
              "Sets the opacity and blend mode of a layer",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "The layer to modify", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Blend Mode", "BM", "The blend mode for the layer", GH_ParamAccess.item);
            pManager[1].Optional = true;
            Param_Integer blendMode = pManager[1] as Param_Integer;
            foreach (BlendMode bm in Enum.GetValues(typeof(BlendMode)))
            {
                blendMode.AddNamedValue(bm.ToString(), (int)bm);
            }
            pManager.AddNumberParameter("Opacity", "O", "The opacity for the layer", GH_ParamAccess.item);
            pManager[2].Optional = true;
            
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
            Ill_Layer lay = null;
            if (!DA.GetData<Ill_Layer>("Layer", ref lay)) return;
            int blendMode = -1;
            
            bool hasBlendMode = DA.GetData<int>("Blend Mode", ref blendMode);
           
            double opacity = 0;
            bool hasOpacity = DA.GetData<double>("Opacity", ref opacity);

            if (hasBlendMode) lay.BlendMode = (BlendMode)blendMode;
            if (hasOpacity) lay.Opacity = opacity;


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
                return Properties.Resources.SetLayerProperties;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{67cba15f-06a2-46d2-baed-466869408479}"); }
        }
    }
}
using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Illest;

namespace IllestForGrasshopper
{
    public class RunScript : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RunScript class.
        /// </summary>
        public RunScript()
          : base("Run Script", "Script",
              "Run script (written in javascript) in Illustrator",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Application", "A", "The running instance of Illustrator", GH_ParamAccess.item);
            pManager.AddTextParameter("Script", "S", "The script to execute", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Run", "R", "Set to true to execute the script", GH_ParamAccess.item,false);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Done", "D", "True on script completion.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Ill_Application app = null;
            if (!DA.GetData<Ill_Application>("Application", ref app)) return;
            string script = "";
            bool run = false;
            if (!DA.GetData<string>("Script", ref script)) return;
            DA.GetData<bool>("Run", ref run);
            if (!run) return;
            app.runScript(script);
            

            DA.SetData("Done", true);
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
                return Properties.Resources.RunScript;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{8a6dc830-027d-4b05-a611-6eac1b8983d8}"); }
        }
    }
}
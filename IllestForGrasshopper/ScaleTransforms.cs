using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Illest;

namespace IllestForGrasshopper
{
    public class ScaleTransforms : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ScaleTransforms class.
        /// </summary>
        public ScaleTransforms()
          : base("ScaleTransforms", "ScaleXForms",
              "Map to and from Illustrator document scale. All Doodlebug components take / output points, so this lets you smartly map to and from other units.",
              "Doodlebug", "Doodlebug")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "Geometry to transform", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddNumberParameter("Rhino Scale", "RS", "Scale in Rhino units", GH_ParamAccess.item,1);
            pManager.AddNumberParameter("Illustrator Scale", "IS", "Scale in Illustrator Units", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("Illustrator Units", "IU", "The units in the Illustrator document", GH_ParamAccess.item, (int)Units.Inches);
            Param_Integer unitsParam = pManager[3] as Param_Integer;
            unitsParam.AddNamedValue("Pixels", 0);
            unitsParam.AddNamedValue("Inches", 1);
            unitsParam.AddNamedValue("Points", 2);
            unitsParam.AddNamedValue("Centimeters", 3);
            unitsParam.AddNamedValue("Millimeters", 4);
            unitsParam.AddNamedValue("Picas", 5);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Transformed Geometry", "G", "The geometry after a transform to illustrator units", GH_ParamAccess.item);
            pManager.AddTransformParameter("Transform From Rhino to Illustrator", "RIX", "The transform from Rhino to illustrator pts", GH_ParamAccess.item);
            pManager.AddTransformParameter("Transform From Illustrator to Rhino", "IRX", "The transform from Illustrator pts to Rhino", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GeometryBase g = null;
            double rhinoScale = 1;
            double illScale = 1;
            int IllUnitsInt = -1;
            Units IllUnits = Units.Points;


            bool hasGeometry = DA.GetData("Geometry", ref g);
            if (!DA.GetData("Rhino Scale", ref rhinoScale)) return;
            if (!DA.GetData("Illustrator Scale", ref illScale)) return;
            if(DA.GetData<int>("Illustrator Units", ref IllUnitsInt))
            {
                IllUnits = (Units)IllUnitsInt;
            }


            double unitScaleFactor = 1.0;
            switch (IllUnits)
            {
                case Units.Inches:
                    unitScaleFactor = 72;
                    break;
                case Units.Picas:
                    unitScaleFactor = 1 / 12.0;
                    break;
                case Units.Centimeters:
                    unitScaleFactor = 28.3465;
                    break;
                case Units.Millimeters:
                    unitScaleFactor = 2.83465;
                    break;
                case Units.Points:
                case Units.Pixels:
                default:
                    break;
            }

            double scaleFromRhinoToIllustrator =  (unitScaleFactor * illScale) / rhinoScale;

            string msg = string.Format("{0} {1} = {2} {3}", rhinoScale, Rhino.RhinoDoc.ActiveDoc.ModelUnitSystem.ToString(), illScale, IllUnits.ToString());
            Message = msg;
            Transform FromRhinoToIllustrator = Transform.Scale(Point3d.Origin, scaleFromRhinoToIllustrator);
            Transform FromIllustratorToRhino = Transform.Scale(Point3d.Origin, 1 / scaleFromRhinoToIllustrator);

            if(hasGeometry)
            {
                g.Transform(FromRhinoToIllustrator);
                DA.SetData("Transformed Geometry", g);

            }
            DA.SetData("Transform From Rhino to Illustrator", FromRhinoToIllustrator);
            DA.SetData("Transform From Illustrator to Rhino", FromIllustratorToRhino);





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
                return Properties.Resources.ScaleTransform;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{4837a5f5-c8e5-44b0-af9f-4bba0c33df44}"); }
        }
    }
}
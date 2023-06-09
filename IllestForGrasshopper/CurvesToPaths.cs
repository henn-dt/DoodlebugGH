using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Illest;

namespace IllestForGrasshopper
{
    public class CurvesToPaths : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GeometryToPath class.
        /// </summary>
        public CurvesToPaths()
          : base("Curves To Paths", "CrvsToPaths",
              "Convert Rhino Curves to bezier curve paths in Illustrator",
              "Doodlebug", "Doodlebug")
        {
            app = null;
            AggregateScript = "";
        }

      
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "The layer to which to add the path", GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve", "C", "Curve to convert", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Stroked", "S", "True if the path has a stroke", GH_ParamAccess.item);
            pManager.AddGenericParameter("Stroke Color", "SC", "Color of the stroke", GH_ParamAccess.item);
            pManager.AddNumberParameter("Stroke Weight", "SW", "Weight of the stroke", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Filled", "F", "True if the path has a fill", GH_ParamAccess.item);
            pManager.AddGenericParameter("Fill Color", "FC", "Color of the fill", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Delete On Layer", "D", "Before adding elements, delete everything on the layer", GH_ParamAccess.item, false);
            for (int i = 2; i < pManager.ParamCount - 1; i++)
            {
                pManager[i].Optional = true;
            }


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
       //     pManager.AddGenericParameter("Path", "P", "The created path item", GH_ParamAccess.item);
        }

        Ill_Application app;
        string AggregateScript;

        protected override void AfterSolveInstance()
        {
            if(!(app == null))
            {
                app.runScript(AggregateScript);
            }
            base.AfterSolveInstance();
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            
            Curve C = null;
            Ill_Layer l = null;
            bool stroked = false;
            bool filled = false;
            double strokeweight = 0;
            Color fillCol = Color.Transparent;
            Color strokeCol = Color.Transparent;
            bool delete = false;

            if (!DA.GetData<Curve>("Curve", ref C)) return;
            if (!DA.GetData<Ill_Layer>("Layer", ref l)) return;
            DA.GetData<bool>("Delete On Layer", ref delete);


            if (delete && DA.Iteration == 0)
            {
                l.ClearPaths();

            }

            if(DA.Iteration == 0)
            {
                AggregateScript = "";
                app = l.Application;
            }

            Ill_PathItem p = null;
            if (C.IsPolyline())
            {
                Polyline PL = null;
                if (C.TryGetPolyline(out PL))
                {
                    List<object[]> polylineCoords = PL.ToPolylineCoords();

                    string script = ScriptComposer.AddPolylinePathItem(l.Document().ToString(),l.LayerName,polylineCoords);
                    AggregateScript += script;
                }

            }
            else {
                List<List<double[]>> points = C.ToProtoPathPoints();
                AggregateScript += ScriptComposer.AddBezierPathItem(l.Document().ToString(), l.LayerName, points);
               // p = l.AddPathItem(points);
            }

            if (p == null)
            {
               // AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Something went wrong in conversion.");
             //   return;
            }

            if (DA.GetData<bool>("Stroked", ref stroked))
            {
                AggregateScript += ScriptComposer.setPathProperty("stroked", stroked.ToString().ToLower());
               // p.hasStroke = stroked;
            }
            if (DA.GetData<bool>("Filled", ref filled))
            {
                AggregateScript += ScriptComposer.setPathProperty("filled", filled.ToString().ToLower());
               // p.hasFill = filled;
            }
            if (DA.GetData<double>("Stroke Weight", ref strokeweight))
            {
                AggregateScript += ScriptComposer.setPathProperty("strokeWidth", strokeweight);
             //   p.strokeWidth = strokeweight;

            }
            if (DA.GetData<Color>("Stroke Color", ref strokeCol))
            {
                AggregateScript += ScriptComposer.defineColor(strokeCol, "strokeCol");
                AggregateScript += ScriptComposer.setPathProperty("strokeColor", "strokeCol");

                //   p.strokeColor = strokeCol;
            }

            if (DA.GetData<Color>("Fill Color", ref fillCol))
            {
                var opacity = fillCol.A / 255.0 * 100;
                AggregateScript += ScriptComposer.defineColor(fillCol, "fillCol");
                AggregateScript += ScriptComposer.setPathProperty("fillColor", "fillCol");
                AggregateScript += ScriptComposer.setPathProperty("opacity", opacity);

                //  p.fillColor = fillCol;
            }
            //   DA.SetData("Path", p);
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
                return Properties.Resources.CurveToPath;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{6D297BFE-F377-4C1A-8D9D-D9292D013A42}"); }
        }
    }
}
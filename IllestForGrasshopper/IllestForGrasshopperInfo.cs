using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace IllestForGrasshopper
{
    public class IllestForGrasshopperInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "IllestForGrasshopper";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("2e476bc1-c420-4668-8fdf-06bacdb38ab0");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Woods Bagot";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}

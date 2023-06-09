using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illest;

namespace IllustratorInteropTester
{
    class Program
    {
        static void Main(string[] args)
        {
           var app = IllustratorInterop.GetIllustrator();
            var layers = IllustratorInterop.GetDocumentLayerNames(app.ActiveDocument);

            foreach (string layer in layers)
            {
                Console.WriteLine(layer);
            }


            Console.Read();
        }



    }
}

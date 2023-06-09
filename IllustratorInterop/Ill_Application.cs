using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Illustrator;

namespace Illest
{
    public class Ill_Application
    {
        public override string ToString()
        {
            return "Illustrator Application Instance";
        }
        public Ill_Document ActiveDocument
        {
            get
            {
                try
                {
                    return new Ill_Document(illApp.ActiveDocument);
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<string> Fonts
        {
            get
            {
                var fonts = illApp.TextFonts.GetEnumerator().IllToList().Cast<TextFont>();
                var fontNames = fonts.Select(f => f.Name).ToList();
                return fontNames;
            }
        }

        public TextFont FindFont(string fontName)
        {
            string font = Fonts.FindClosestString(fontName);
            return illApp.TextFonts.GetFontByName(font);
        }

        public Ill_Document Open(string filePath)
        {
            return new Ill_Document(illApp.Open(filePath));
        }


        public void runScript(string script)
        {
            illApp.DoJavaScript(script);
        }

        public List<Ill_Document> Documents
        {
            get
            {
                List<Ill_Document> docs = new List<Ill_Document>();
                foreach (var document in illApp.Documents)
                {
                    docs.Add(new Ill_Document(document as Document));
                }
                return docs;
            }
        }
        Application illApp;
        public Ill_Application(Application app)
        {
            illApp = app;
        }

        public Ill_Application()
        {
            illApp = new Application();
        }

        public Ill_Document AddDocumentInches(string presetName, string docName, double widthInInches, double heightInInches)
        {
            var docPreset = new DocumentPreset();
            docPreset.DocumentUnits = AiRulerUnits.aiUnitsInches;
           
            docPreset.Width = widthInInches * 72;
            docPreset.Height = heightInInches * 72;
            docPreset.DocumentTitle = docName;
            var doc = illApp.Documents.AddDocument(presetName, docPreset);
         
            return new Ill_Document(doc);
        }

        public Ill_Document OpenWithSize(string filePath, double widthInInches, double heightInInches)
        {
            var tempDoc = AddDocumentInches("Temp", "Temp", widthInInches, heightInInches);
            string thing = tempDoc.ToString();
            tempDoc.Close();
            return Open(filePath);
        }


    }
}

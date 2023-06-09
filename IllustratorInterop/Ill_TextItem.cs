using System.Drawing;
using Illustrator;

namespace Illest
{

    public enum Ill_Justification
    {
        Left = 0,
        Right = 1,
        Center = 2,
        FullJustify = 6
    }

    public class Ill_TextItem
    {
        private TextFrame textFrame;
        private Ill_Application app;
        public Ill_TextItem(TextFrame frame)
        {
            this.textFrame = frame;
            app = new Ill_Application(frame.Application);
        }

        public double Size
        {
            get
            {
                return textFrame.TextRange.CharacterAttributes.Size;
            }
            set
            {
                textFrame.TextRange.CharacterAttributes.Size = value;
            }
        }


        public string Font
        {
            get
            {
                return textFrame.TextRange.CharacterAttributes.TextFont.Name;
            }
            set
            {
                TextFont closestMatch = app.FindFont(value);
                textFrame.TextRange.CharacterAttributes.TextFont = closestMatch;
            }
        }

        public Ill_Justification Justification
        {
            get
            {

                return (Ill_Justification)textFrame.TextRange.ParagraphAttributes.Justification;
            }

            set
            {
                textFrame.TextRange.ParagraphAttributes.Justification = (AiJustification)value;
            }
        }

        public string Content
        {
            get
            {
                return textFrame.Contents;
            }
            set
            {
                textFrame.Contents = value;
            }
        }

        public Color Color
        {
            get
            {
                return textFrame.TextRange.CharacterAttributes.FillColor.ToSysColor();

            }
            set
            {
                textFrame.TextRange.CharacterAttributes.FillColor = value.ToIllColor();
            }
        }
    }
}
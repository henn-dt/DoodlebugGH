using Illustrator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Illest
{
    public static class ExtensionMethods
    {
        public static List<object> IllToList(this IEnumerator enumerator)
        {
            List<object> vals = new List<object>();
            while (enumerator.MoveNext())
            {
                vals.Add(enumerator.Current);
            }

            return vals;
        }

        public static string FindClosestString(this List<string> set, string search)
        {
            int dist = int.MaxValue;
            int indexToReturn = -1;
            for(int i = 0; i < set.Count; i++)
            {
                string item = set[i];
                int itemDist = item.LevenshteinDistanceTo(search);
                if (itemDist < dist)
                {
                    dist = itemDist;
                    indexToReturn = i;
                }
            }

            try
            {
                return set[indexToReturn];
            } catch
            {
                return null;
            }
        }

        public static int LevenshteinDistanceTo(this string A, string B)
        {
            return LevenshteinDistance(A, B);
        }

        public static int LevenshteinDistance(string A, string B)
        {
            int[,] D = new int[A.Length + 1, B.Length + 1];
            int arg_1F_0 = 0;
            int length = A.Length;
            for (int i = arg_1F_0; i <= length; i++)
            {
                D[i, 0] = i;
            }
            int arg_3D_0 = 0;
            int length2 = B.Length;
            for (int j = arg_3D_0; j <= length2; j++)
            {
                D[0, j] = j;
            }
            int arg_5B_0 = 1;
            int length3 = B.Length;
            for (int k = arg_5B_0; k <= length3; k++)
            {
                int arg_6B_0 = 1;
                int length4 = A.Length;
                for (int l = arg_6B_0; l <= length4; l++)
                {
                    int cost = 0;
                    if (A[l - 1] != B[k - 1])
                    {
                        cost = 1;
                    }
                    int ins = D[l - 1, k] + 1;
                    int del = D[l, k - 1] + 1;
                    int sbs = D[l - 1, k - 1] + cost;
                    D[l, k] = System.Math.Min(System.Math.Min(ins, del), sbs);
                }
            }
            return D[A.Length, B.Length];
        }


        public static RGBColor ToIllColor(this System.Drawing.Color value)
        {

            var RGBCol = new RGBColor();
            RGBCol.Red = value.R;
            RGBCol.Green = value.G;
            RGBCol.Blue = value.B;
            return RGBCol;
        }

        public static Color ToSysColor(this object illColor,double ItemOpacity = 100)
        {
            try
            {
                byte opacity = 255;
                opacity = Convert.ToByte(Math.Min(ItemOpacity / 100.0 * 255, 255));

                RGBColor rgbCol = illColor as RGBColor;
                if (rgbCol != null)
                {
                    System.Drawing.Color c = System.Drawing.Color.FromArgb(opacity, (int)rgbCol.Red, (int)rgbCol.Green, (int)rgbCol.Blue);
                    return c;
                }
                CMYKColor cmykColor = illColor as CMYKColor;
                if (cmykColor != null)
                {
                    object[] componentsIn = new object[] { cmykColor.Cyan, cmykColor.Magenta, cmykColor.Yellow, cmykColor.Black };
                    object[] components = cmykColor.Application.ConvertSampleColor(AiImageColorSpace.aiImageCMYK, componentsIn, AiImageColorSpace.aiImageRGB, AiColorConvertPurpose.aiDefaultPurpose);
                    System.Drawing.Color c = System.Drawing.Color.FromArgb(opacity, Convert.ToInt32(components[0]), Convert.ToInt32(components[1]), Convert.ToInt32(components[2]));
                    return c;

                }
            }
            catch
            {

            }

            return System.Drawing.Color.Transparent;
        }
    }
}

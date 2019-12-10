using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DosScreen
{
  static class RGBToGray
    {

        public static int[,] RGB2Gray(Bitmap bmp)
        {
            int[,] Gray = new int[bmp.Width, bmp.Height];
            Color curColor;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    curColor = bmp.GetPixel(i, j);
                    Gray[i, j] = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                }
            }
            return Gray;
        }
    }
}

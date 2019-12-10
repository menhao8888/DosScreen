using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosScreen
{
    static class ImageToASCII
    {

         static readonly string charset = "MNHQ&OC?7>!:-;.";
        //算分块大小
        static int RowSize = 360 / 100 + 1;
        static int ColSize = RowSize / 2;
       static string res = null;
        //转换字符
        public static void GenerateString(Bitmap IMG/*,BackgroundWorker worker, DoWorkEventArgs e*/)
        {
            Bitmap img = IMG;

            int[,] GrayImg =RGBToGray.RGB2Gray(img);
            //分块大小
            //int RowSize = (int)numericUpDown1.Value;
            //int ColSize = (int)numericUpDown2.Value;
            
            int RowSize = 6;
            int ColSize = 5;
            //遍历各分块
            for (int h = 0; h < img.Height / RowSize; h++)
            {
                int Hoffset = h * RowSize;
                for (int w = 0; w < img.Width / ColSize; w++)
                {
                    int Woffset = w * ColSize;
                    int AvgGray = 0;
                    for (int x = 0; x < RowSize; x++)
                    {
                        for (int y = 0; y < ColSize; y++)
                        {
                            AvgGray += GrayImg[Woffset + y, Hoffset + x];
                        }
                    }
                    AvgGray /= RowSize * ColSize;
                    //计算灰度处在字符集的哪一个灰度等级，来选择合适的字符
                    if (AvgGray / 17 < charset.Length)
                    {
                        res += charset[AvgGray / 17];
                      //  Console.Write();
                    }
                    else
                    {
                           res += " ";
                    }
                    //报告完成进度
                  //  int percentComplete = (int)((float)(h * img.Width / ColSize + w) / (float)((img.Height / RowSize) * (img.Width / ColSize)) * 100);
                }
                 res += "\n";
                Console.Write(res);
                res = null;
              //  Console.Write("\n");

            }

        }
    }
}

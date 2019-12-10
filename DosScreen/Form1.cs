using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace DosScreen
{


    public partial class Form1 : Form
    {
        
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest
            , int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc,
            System.Int32 dwRop  // 光栅的处理数值
              );

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //    //获得当前屏幕的大小
            //    Rectangle rect = new Rectangle();
            //    rect = Screen.GetWorkingArea(this);
            //    //创建一个以当前屏幕为模板的图象
            //    Graphics g1 = this.CreateGraphics();
            //    //创建以屏幕大小为标准的位图
            //    Image MyImage = new Bitmap(rect.Width, rect.Height, g1);
            //    Graphics g2 = Graphics.FromImage(MyImage);
            //    //得到屏幕的DC
            //    IntPtr dc1 = g1.GetHdc();
            //    //得到Bitmap的DC
            //    IntPtr dc2 = g2.GetHdc();
            //    //调用此API函数，实现屏幕捕获
            ////    BitBlt(dc2, 0, 0, rect.Width, rect.Height, dc1, 0, 0, 13369376);
            //BitBlt(dc2, 0, 0, 1024, 768, dc1, 0, 0, 13369376);
            ////释放掉屏幕的DC
            //g1.ReleaseHdc(dc1);
            //    //释放掉Bitmap的DC
            //    g2.ReleaseHdc(dc2);
            //    //以JPG文件格式来保存
            //    MyImage.Save(@"E:\Capture.jpg", ImageFormat.Jpeg);
            //    MessageBox.Show("当前屏幕已经保存为C盘的capture.jpg文件！");

            //80 X 25
            //string[] Char= new string[2000];
   


            //for (int i = 0; i < Char.Length; i++)
            //{
                
            //    Console.ForegroundColor = (System.ConsoleColor)10;
            //    Char[i] = "@";
            //    Console.Write(Char[i]);
            

            //}
            

          //  Console.Write("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
       //     AdjustTobMosaic(CUTScreen.CutScreen(), 10).Save(@"E:\123.bmp", ImageFormat.Bmp);
       while (true)
            { 
            //AdjustTobMosaic(CUTScreen.CutScreen(), 10);
                //     Thread.Sleep();
                //    Console.Clear();
                CUTScreen.BitmapCutScreen();


            }

        }





       static public Bitmap AdjustTobMosaic(Bitmap bitmap, int effectWidth)
        {
            // 差异最多的就是以照一定范围取样 之后直接去下一个范围
            for (int heightOfffset = 0; heightOfffset < bitmap.Height; heightOfffset += effectWidth)
            {
                for (int widthOffset = 0; widthOffset < bitmap.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 计算范围平均
                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // 所有范围内都设定此值
                    for (int x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (int y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
            }
         
            return bitmap;
        }

        static void GetCost(string state)
        {
            Console.Write("当前状态：" + state + ";  占用内存:");
            using (var p1 = new PerformanceCounter("Process", "Working Set - Private", "GCtest.vshost"))
            {
                Console.WriteLine((p1.NextValue() / 1024 / 1024).ToString("0.0") + "MB");
            }
        }

    }

    static class CUTScreen
    {
       static int width = 360;  //主屏幕宽度
       static int height = 250;    //主屏幕高度
        public static void BitmapCutScreen()
        {
            Bitmap bmp = new Bitmap(width, height);     //新建一个 Bitmap 位图 
            Graphics g = Graphics.FromImage(bmp);   //从 Bitmap 位图创建 Graphics
         //   Stream BitmapStream =null;

            g.CopyFromScreen(new Point(100, 100), new Point(0, 0), new Size(width, height));
            g.ReleaseHdc(g.GetHdc());
            // string FileName = Path + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + new Random().Next(999) + ".jpg";
          //  bmp.Save(@"E:\Capture.jpg", ImageFormat.Jpeg);

        //    bmp.Save(BitmapStream, ImageFormat.Bmp);
           
       //     System.GC.Collect();
         

            ImageToASCII.GenerateString(bmp);
          //  return bmp;
        }

    }

}

using System;
using System.Drawing;
using System.IO;

namespace Sader_Ragnarok_Login_Background
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args[0];

            if (Path.GetExtension(file) != ".jpg" && Path.GetExtension(file) != ".bmp" && Path.GetExtension(file) != ".png")
            {
                Console.WriteLine("the image extension must be .jpg , .bmp or .png");
                Console.ReadLine();
                Environment.Exit(0);
            }
            string path = Path.GetDirectoryName(file);

            Image img = Image.FromFile(file);
            Console.WriteLine("What is the number of the login image ? (normally it's Nothing or 2)");
            Console.WriteLine("For the default login background just press Enter.");
            string n = Console.ReadLine();
            Bitmap src = new Bitmap(img, new Size(256 * 4, 256 * 3));

            string[] name = {
                    "t" + n + "_배경1-1",
                    "t" + n + "_배경1-2",
                    "t" + n + "_배경1-3",
                    "t" + n + "_배경1-4",

                    "t" + n + "_배경2-1",
                    "t" + n + "_배경2-2",
                    "t" + n + "_배경2-3",
                    "t" + n + "_배경2-4",

                    "t" + n + "_배경3-1",
                    "t" + n + "_배경3-2",
                    "t" + n + "_배경3-3",
                    "t" + n + "_배경3-4"
            };

            int[] dst = {
                0,0,
                256,0,
                512,0,
                768,0,

                0,256,
                256,256,
                512,256,
                768,256,

                0,512,
                256,512,
                512,512,
                768,512,
            };

            if (!Directory.Exists(@"data"))
            {
                Directory.CreateDirectory(@"data");
            }
            if (!Directory.Exists(@"data\texture"))
            {
                Directory.CreateDirectory(@"data\texture");
            }
            if (!Directory.Exists(@"data\texture\유저인터페이스"))
            {
                Directory.CreateDirectory(@"data\texture\유저인터페이스");
            }

            int d = 0;
            for (int i = 0; i < 12; i++)
            {
                Rectangle cropRect = new Rectangle(dst[d++], dst[d++], 256, 256);
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                using (Graphics g = Graphics.FromImage(target))
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
                target.Save(@"data\texture\유저인터페이스\" + name[i] + ".bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            }
            Console.WriteLine("Done, next to the image you will find a data folder , add it to your Grf.");
            Console.ReadLine();
        }
    }
}

using System;
using System.Drawing;
using System.IO;

namespace Sader_Ragnarok_Login_Background
{
    class Program
    {
        static void Main(string[] args)
        {
            //Check if the user has provided an image file as an argument
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("Please drag and drop the image you want on the application!");
                Console.WriteLine("When you drag and drop the image onto the application the folder will be created in the same path.");
                Console.WriteLine("also you will get this error if the file doesn't exist.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                return;
            }

            string file = args[0];

            //Check if the file extension is valid
            if (Path.GetExtension(file) != ".jpg" && Path.GetExtension(file) != ".bmp" && Path.GetExtension(file) != ".png")
            {
                Console.WriteLine("the image extension must be .jpg , .bmp or .png");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                return;
            }

            Image img = Image.FromFile(file);
            Console.WriteLine("What is the number of the login image ? (normally it's Nothing or 2)");
            Console.WriteLine("For the default login background just press Enter.");
            string n = Console.ReadLine();
            Bitmap src = new Bitmap(img, new Size(256 * 4, 256 * 3));

            //Using a loop to generate the name array
            string[] name = new string[12];
            for (int i = 0; i < 12; i++)
            {
                int row = i / 4 + 1;
                int col = i % 4 + 1;
                name[i] = $"t{n}_배경{row}-{col}";
            }

            //Create a two-dimensional array to store the destination coordinates
            int[,] dst = {
                {0,0},
                {256,0},
                {512,0},
                {768,0},

                {0,256},
                {256,256},
                {512,256},
                {768,256},

                {0,512},
                {256,512},
                {512,512},
                {768,512},
            };

            //Create the directory and all its subdirectories if they do not exist
            try
            {
                // Try to create the directory and all its subdirectories
                Directory.CreateDirectory(@"data\texture\유저인터페이스");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle the case when the caller does not have the required permission
                Console.WriteLine("You do not have the permission to create the directory.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                // Handle the case when the directory cannot be created due to anything other than the permissions.
                Console.WriteLine("There was a problem creating the directory.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                return;
            }

            //Access the destination coordinates from the two-dimensional array
            for (int i = 0; i < 12; i++)
            {
                Rectangle cropRect = new Rectangle(dst[i, 0], dst[i, 1], 256, 256);
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                using (Graphics g = Graphics.FromImage(target))
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
                target.Save($@"data\texture\유저인터페이스\{name[i]}.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }

            Console.WriteLine("Done, next to the image you will find a data folder , add it to your '.grf'.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}

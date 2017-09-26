using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace BitmapToCSV
{
    class Program
    {
        public string csv;

        public void SetString(string input)
        {
            csv = csv + input + ",";

        }
        public void ResetString()
        {
            csv = "";
        }
        public Bitmap ConvertToGrayscale(string sourceFile) // Average method
        {
            Bitmap image = new Bitmap(sourceFile);
            Color color;
            int y;
            int x;
            int avg;


            // Loop through the images pixels to reset color.
            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    color = image.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = color.A;
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    //find average
                    avg = (r + g + b) / 3;


                    SetString(avg.ToString());
                    //set new pixel value
                    image.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }
            }
            return image;
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            // Plan of attack: 
            /*
             * Make bitmap from input file
             * Convert to grayscale (different weights?)
             * While converting the RGB values to gray, grab the resulting int and put it in a string or method that will do: 
             * int,int,int...int,int (classic csv format)
             * 
             *
             *
             *
             */
            string bmStr = args[0];
            Bitmap bm = p.ConvertToGrayscale(bmStr);
            string currentdir = Directory.GetCurrentDirectory();
            Console.WriteLine(currentdir);
            string outputFile = currentdir + "\\" + bmStr + ".csv";
            Console.WriteLine(outputFile);
            if (!File.Exists(outputFile))
            {
                // Create a file to write to.
                File.WriteAllText(outputFile, p.csv);
            }


        }
    }
}

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
        public Color c;
        public string FileNameString;

        public void AddCSVString(string input)
        {
            csv = csv + input + ";";

        }
        public void SetFileName(string fn)
        {
            FileNameString = fn;
        }

        public string GetFileName()
        {
            return FileNameString;
        }

        public void SetColor(Color color)
        {
            c = color;
        }

        // from http://www.nbdtech.com/Blog/archive/2008/04/27/Calculating-the-Perceived-Brightness-of-a-Color.aspx
        public int BrightnessInt(Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }

        public void WriteCSVToFileViaFileName(String filename)
        {
            string currentdir = Directory.GetCurrentDirectory();

            string outputFile = currentdir + "\\" + filename + ".csv";
            
            File.WriteAllText(outputFile, csv);

        }
        public void WriteStringToFile(String filename, String TextToWrite)
        {
            string currentdir = Directory.GetCurrentDirectory();

            string outputFile = currentdir + "\\" + filename + ".csv";

            File.WriteAllText(outputFile, TextToWrite);

        }

        public void ConvertToGrayscale(Bitmap image) // Average method
        {
            Color color;
            int y;
            int x;
            int avg;


            // Loop through the images pixels
            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    color = image.GetPixel(x, y);
                    SetColor(color);
                    

                    //extract pixel component ARGB
                    int a = color.A;
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    //find average
                    // avg = (r + g + b) / 3; //orginal

                    // REC 709 
                    avg = (int)(r * 0.2126 + g * 0.7152 + b * 0.0722);

                    

                    // get the average grayscale data, put it in the string. 
                    // This is done to have the data for a histogram aproach at a later date so images can be compared or other statistical attacks can be performed
                    //SetString(avg.ToString()); // Original
                    //int Brightness = (int)(color.GetBrightness() * 100); //HSB brightness
                     int Brightness  = BrightnessInt(color);

                    AddCSVString(Brightness.ToString());

                    //set new pixel value
                    //image.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }
            }
            
        }

       
        public void BitmapToGrayscaleCSV(Bitmap bm)
        {

            ConvertToGrayscale(bm);
            WriteCSVToFileViaFileName(GetFileName());


        }
        public Bitmap ResizeImageToSquare(String arg0, int size)
        {

            // resize the input image as a string
            Bitmap bm = new Bitmap(arg0);
            return bm;

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
             *
             */

            /*
             * Step 1: Make string with the filename
             * Step 2: Set global string with the filename
             * Step 3: Do the converstions with Bitmaps directly
             * Step 4: Use the global filename string to write the filenames out.
             * 
             * 
             * 
             */


            // if 0 arguments, show readme
            if (args == null || args.Length == 0 || args.Length > 3)
            {
                Console.WriteLine("Readme");
            }
            else if (args.Length == 1)
            {
                string bmStr = args[0];
                p.SetFileName(bmStr);
                Bitmap bm = new Bitmap(bmStr);
                p.BitmapToGrayscaleCSV(bm);

            }
            else if (args.Length == 2)
            // if 2 arguments, do resize to the specified value in square, then make CSV file
            {
                string bmStr = args[0];
                p.SetFileName(bmStr);

                Bitmap bitmap = new Bitmap(args[0]);
                int argInt = Int32.Parse(args[1]);
                
                
                if (argInt > 12)
                {
                    Console.WriteLine("The image needs to be smaller than 12x12 pixes");

                }
                else
                {
                    Bitmap resized = p.ResizeImageToSquare(args[0], argInt);
                    p.BitmapToGrayscaleCSV(resized);
                    
                    
                }


            }
            else if (args.Length == 3)
            // if 3 arguments, do resize to specified square, rename the input file to the pixel values (clamp to 12 pixels)
            {
                int argInt = Int32.Parse(args[1]);
                if (argInt>12)
                    {
                    Console.WriteLine("The image needs to be smaller than 12x12 pixes");

                }
                // if pixels more than 12, tell to fix
                else
                {
                    Bitmap resized = p.ResizeImageToSquare(args[0], argInt);
                    // p.RenameFileToCSVValues(resized); // To be implemented

                }
            }
            

            



        }
    }
}

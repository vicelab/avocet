using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimelapseBuilder
{
    class Program
    {

        static string filler = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";

        static void Main(string[] args)
        {
            /* Get Introduction */
            Console.WriteLine(filler + "\n~ Welcome to the Timelapse Builder\n" + filler);
            Console.WriteLine("~ Press ENTER to begin");///Enter file location and press ENTER.\n~ (HARDCODED TO ./INPUT/)");

            /* Read data */
            string folder = "/input";

            //while (true)
            {
                Console.Read();
                generateGIF(folder);
            }
        }

        /* Give range of values of a given set of data */
        static void generateGIF(string folder)
        {

            string searchFolder = Directory.GetCurrentDirectory() + "/ExampleData";
            Console.WriteLine(searchFolder);
            var filters = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var files = getImages(searchFolder, filters, false);

            using (MagickImageCollection collection = new MagickImageCollection())
            {
                int number_images = files.Length;
                                       //MagickImage[] images = new MagickImage[number_images];
                                       // Console.WriteLine(files.Length);
                for (int i = 0; i < number_images; i++)
                {

                    //image.Resize(300, 0);

                    Console.WriteLine(((i * 1.0) / number_images).ToString("p") + ": Adding " + files[i]);
                    collection.Add(files[i]);
                    collection[i].Resize(300, 0);
                    collection[i].AnimationDelay = 5;



                }
                Console.WriteLine("Added Images");
                // Optionally reduce colors
                //    QuantizeSettings settings = new QuantizeSettings();
                //   settings.Colors = 256;
                //  collection.Quantize(settings);

                // Optionally optimize the images (images should have the same size).
                //   collection.Optimize();
                collection.Optimize();
                Console.WriteLine("Optimized");
                // Save gif
                collection.Write("output.gif");
                Console.WriteLine("outputted");
            }
        }

        static string[] getImages(string searchFolder, string[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        

        static void combineImages()
        {
            using (MagickImageCollection images = new MagickImageCollection())
            {
                // Add the first image
                MagickImage first = new MagickImage("Snakeware.png");
                images.Add(first);

                // Add the second image
                MagickImage second = new MagickImage("Snakeware.png");
                images.Add(second);

                // Create a mosaic from both images
                using (IMagickImage result = images.Mosaic())
                {
                    // Save the result
                    result.Write("Mosaic.png");
                }
            }
        }
    }
}

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
                int image_index = 0;
                for (int i = 0; i < number_images; i++)
                {
                    MagickImageInfo info = new MagickImageInfo(files[i]);
                    DateTime written_time = new FileInfo(files[i]).LastWriteTime;
                   // Console.WriteLine( written_time.Hour);
                    if (written_time.Hour <= 5 || written_time.Hour >= 20) //remove 8pm to 5am
                    {
                        Console.WriteLine(((i * 1.0) / number_images).ToString("p") + ": Skipping " + written_time);
                        continue;
                    }
                    Console.WriteLine(((i * 1.0) / number_images).ToString("p") + ": Adding " + written_time);
                   
                    collection.Add(files[i]);
                    collection[image_index].Resize(600, 0);
                    collection[image_index].AnimationDelay = 2;
                    image_index++;
                }
                Console.WriteLine("Added Images");
                // Optionally reduce colors
                //    QuantizeSettings settings = new QuantizeSettings();
                //   settings.Colors = 256;
                //  collection.Quantize(settings);
            
                collection.Optimize();
                Console.WriteLine("Optimized");
                collection.Write("output.gif");
                //ffmpeg -i animated.gif -movflags faststart -pix_fmt yuv420p -vf "scale=trunc(iw/2)*2:trunc(ih/2)*2" video.mp4
                Console.WriteLine("Outputted");
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

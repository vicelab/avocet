/*
 *  Author: Brian Hungerman
 *  
 *  This application generates a GIF out of any set of images in a defined subfolder to the executable's current directory
 *  
 *  The width (in px) and frame rate (in ms) of the resulting GIF can be controlled
 *  
 *  Runtime note: At 600px Width, 900 images, takes 35~ minutes to process GIF, 35~ seconds to convert to MP4
 */ 


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

            /* Init Variables */
            string folder = "PlanetData";
            int speed = 2;
            int width = 600;
            
            Console.Read();
            
            /* Run */
            generateGIF(folder, speed, width);
            
        }

        /* Give range of values of a given set of data */
        static void generateGIF(string folder, int speed, int width)
        {

            string searchFolder = Directory.GetCurrentDirectory() + "/" + folder;
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
                    if (written_time.Hour <= 5 || written_time.Hour >= 20) //remove 8pm to 5am
                    {
                        Console.WriteLine(((i * 1.0) / number_images).ToString("p") + ": Skip " + written_time);
                        continue;
                    }
                    Console.WriteLine(((i * 1.0) / number_images).ToString("p") + ": Add  " + written_time);
                   
                    collection.Add(files[i]);
                    collection[image_index].Resize(width, 0);
                    collection[image_index].AnimationDelay = speed;
                    image_index++;
                }

                Console.Clear();
                Console.WriteLine("Added Images, Optimizing... \n (this make take some time)");           
                collection.Optimize();

                Console.Clear();
                Console.WriteLine("Optimized, Outputting... \n (this may take some time)");
                collection.Write("output.gif");

                Console.WriteLine("Outputted, Closing");
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

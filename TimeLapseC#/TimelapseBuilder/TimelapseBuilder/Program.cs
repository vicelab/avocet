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
           
            /* Init Variables */
            string folder = "TestingData";
            int speed = 4;//2;
            int width = 600;

            /* If there are any TIF images, converts to PNG first */
            //convertTIFtoPNG(folder);

            Console.WriteLine("~ Press ENTER to begin");///Enter file location and press ENTER.\n~ (HARDCODED TO ./INPUT/)");

            combineImages();
            Console.Read();


            /* Run */
            generateGIF(folder, speed, width);

            
        }

        static void convertTIFtoPNG(string folder)
        {
            DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\" + folder);
            foreach (var file in d.GetFiles("*.tif"))
            {
                string newName = file.FullName.Substring(0, file.FullName.Length - 3) + "png";
                Console.WriteLine(newName);
                System.Drawing.Bitmap.
                FromFile(file.FullName).
                Save(newName,
                System.Drawing.Imaging.ImageFormat.Png);
            }            
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
                //collection.Optimize();
                /*
                 * foreach (MagickImage image in collection)
    {
        image.Resize(200, 0);
    }*/
                Console.Clear();
                Console.WriteLine("Optimized, Outputting... \n (this may take some time)");
                string outputName = "";

                Random rnd = new Random();
                for (int i = 0; i < 10; i++)
                {
                    outputName += (char)(rnd.Next(97, 122));
                }
                outputName += ".gif";
                collection.Write(outputName);

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
                MagickImage first = new MagickImage("test.jpg");
                images.Add(first);
       
                using (MagickImage image = new MagickImage(new MagickColor("#ff00ff"), 512, 128))
                {
                    new Drawables()
                      // Draw text on the image
                      .FontPointSize(72)
                      .Font("Comic Sans")
                      .StrokeColor(new MagickColor("yellow"))
                      .FillColor(MagickColors.Orange)
                      .TextAlignment(TextAlignment.Center)
                      .Text(256, 64, "Magick.NET")
                      // Add an ellipse
                      .StrokeColor(new MagickColor(0, Quantum.Max, 0))
                      .FillColor(MagickColors.SaddleBrown)
                      .Ellipse(256, 96, 192, 8, 0, 360)
                      .Draw(image);

                    images.Add(image);

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
}

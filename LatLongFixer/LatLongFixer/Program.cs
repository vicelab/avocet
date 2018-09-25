using System;
using System.IO;

namespace LatLongFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            intro();
        }

        static void intro()
        {
            Console.Clear();
            string filler = "";
            for (int i = 0; i < 50 ; i++)
            {
                filler += "~";
            }
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.ReadLine();
            string[] lines = File.ReadAllLines(@"../../../cameraLoc.txt");
            for (int i = 0; i < lines.Length; i+=50)
            {
                Console.WriteLine(lines[i]);
            }
            Console.Clear();

            Console.WriteLine(filler);
            Console.WriteLine("~  Thank you for inputting a file, Parsing Now!  ~");
            Console.WriteLine(filler);
            Console.ReadLine();
            string[] line;
            for (int i = 0; i < lines.Length; i += 50)
            {
                line = lines[i].Split('	');
                Console.WriteLine("Pos: " + line[1] + "," + line[2]);
            }
            Console.Clear();
            string[] prev_line;
            double[] running_average = new double[] { 0, 0 };
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i-1].Split('	');
                line = lines[i].Split('	');
                //Give some haptic feedback
                running_average[0] += double.Parse(line[1]) - double.Parse(prev_line[1]);
                running_average[1] += double.Parse(line[2]) - double.Parse(prev_line[2]);
                if (i%50 == 0) Console.WriteLine("ΔPos: " + (double.Parse(line[1])- double.Parse(prev_line[1])) + "," + (double.Parse(line[2]) - double.Parse(prev_line[2])));
            }
            Console.Clear();
            running_average[0] = running_average[0] / lines.Length;
            running_average[1] = running_average[1] / lines.Length;
            Console.WriteLine(running_average[0] / lines.Length + "," + running_average[1] / lines.Length);
            Console.ReadLine();



        }
    }
}

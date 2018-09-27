using System;
using System.Collections.Generic;
using System.IO;

namespace LatLongFixer
{
    class Program
    {
        static int response = 0;
        static void Main(string[] args)
        {
            while(true) intro();
        }

        static void intro()
        {
            Console.Clear();
            string filler = "";
            for (int i = 0; i < 50; i++)
            {
                filler += "~";
            }
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Enter file location and press ENTER.\n~ (e.g. C:/data/log.txt )");

            string file_location = Console.ReadLine();

            //  Console.ReadLine();
            string[] lines = File.ReadAllLines(file_location);//@"input.txt");

            for (int i = 0; i < lines.Length; i += 100)
            {
                Console.WriteLine(lines[i]);
            }
            Console.Clear();

            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Reading from: " + file_location);
            Console.WriteLine(filler);
            Console.WriteLine("~ Enter distance threshold and press ENTER.\n~ (e.g.  0.0000000001)");
            Console.WriteLine(filler);
            double threshold = double.Parse(Console.ReadLine());

            string[] prev_line;
            string[] line;

            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                if (Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1])) < 0.0000000001)
                {
                    Console.WriteLine("Line[" + i + "] is messed up?");
                }
            }


            string output = "";
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                if (Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1])) > 0.0000000001)
                {
                    output += lines[i] + "\n";

                }
                else
                {
                    line = lines[i].Split('	');
                    output += line[0] + "	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0\n";
                }
                if (i % 100 == 0)
                {
                    Console.Clear();
                    Console.WriteLine(filler);
                    Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
                    Console.WriteLine(filler);
                    Console.WriteLine("~ Reading from: " + file_location);
                    Console.WriteLine(filler);
                    Console.WriteLine("~ Using threshold: " + threshold);
                    Console.WriteLine(filler);
                    Console.WriteLine("~ Rewriting .TXT with removed points.");
                    Console.WriteLine(filler);
                    Console.WriteLine("~ " + ((double)i / lines.Length).ToString("p") + " the way there!");
                }
            }
            Console.Clear();
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Reading from: " + file_location);
            Console.WriteLine(filler);
            Console.WriteLine("~ Using threshold: " + threshold);
            Console.WriteLine(filler);
            Console.WriteLine("~ Rewrote .TXT with removed points.");
            Console.WriteLine(filler);
            Console.WriteLine("~ Overriding file");
            Console.WriteLine(filler);

            File.WriteAllText(file_location, output);

            Console.Clear();
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Reading from: " + file_location);
            Console.WriteLine(filler);
            Console.WriteLine("~ Using threshold: " + threshold);
            Console.WriteLine(filler);
            Console.WriteLine("~ Rewrote .TXT with removed points.");
            Console.WriteLine(filler);
            Console.WriteLine("~ Overriding file");
            Console.WriteLine(filler);
            Console.WriteLine("~ Enter 1 to rerun process. Press 0 to close.");

            response = int.Parse(Console.ReadLine());

            if (response == 0)
            {
                Environment.Exit(0);
            }
        }
    }
}

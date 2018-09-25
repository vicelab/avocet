using System;
using System.Collections.Generic;
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
            for (int i = 0; i < 50; i++)
            {
                filler += "~";
            }
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
          //  Console.ReadLine();
            string[] lines = File.ReadAllLines(@"input.txt");
            for (int i = 0; i < lines.Length; i += 50)
            {
                Console.WriteLine(lines[i]);
            }
            Console.Clear();

            Console.WriteLine(filler);
            Console.WriteLine("~  Thank you for inputting a file, Parsing Now!  ~");
            Console.WriteLine(filler);
          //  Console.ReadLine();
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
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                //Give some haptic feedback
                running_average[0] += Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1]));
                running_average[1] += Math.Abs(double.Parse(line[2]) - double.Parse(prev_line[2]));

                if (i % 50 == 0) Console.WriteLine("ΔPos: " + running_average[0] + "," + running_average[1]);
            }
            Console.Clear();
            running_average[0] = running_average[0] / lines.Length;
            running_average[1] = running_average[1] / lines.Length;
            Console.WriteLine(filler);
            Console.WriteLine("~  Average Distance Found, Identifying problems  ~");
            Console.WriteLine(filler);
            Console.WriteLine(running_average[0] / lines.Length + "," + running_average[1] / lines.Length);
         //   Console.ReadLine();

            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                //if (double.Parse(line[1]) - double.Parse(prev_line[1]) > running_average[0]*2 || double.Parse(line[2]) - double.Parse(prev_line[2]) > running_average[1]*2)
                if (Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1])) < 0.0000000001)
                {
                    Console.WriteLine("Line[" + i + "] is messed up?");
                }
            }
            Console.Clear();
            Console.WriteLine(filler);
            Console.WriteLine("~  Rewriting .TXT with removed points. . . . . . ~");
            Console.WriteLine(filler);
            string output = "";
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                if (Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1])) > 0.0000000001)
                {
                    output += lines[i];

                }
                if (i % 100 == 0)
                {
                    Console.Clear();
                    Console.WriteLine(filler);
                    Console.WriteLine("~  Rewriting .TXT with removed points. . . . . . ~");
                    Console.WriteLine(filler);
                    Console.WriteLine(((double)i / lines.Length).ToString("p") + " the way there!");
                }
            }
            Console.Clear();
            Console.WriteLine(filler);
            Console.WriteLine("~  Saving File, Program will Close when Done     ~");
            Console.WriteLine(filler);

            File.WriteAllText(@"output.txt", output);

        }
    }
}

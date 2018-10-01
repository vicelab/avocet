using System;
using System.Collections.Generic;
using System.IO;

namespace LatLongFixer
{
    class Program
    {
        static int response = 0;

        static double high, low, average;
        static double x, y, dist;

        static void Main(string[] args)
        {
            while(true) intro();
        }

        static void getHighLowAverage(string[] lines)
        {
            string[] prev_line, line;
            high = double.MinValue;
            low = double.MaxValue;
            average = 0;
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                if (prev_line[1] == "0.0" || prev_line[2] == "0.0")
                {
                    continue;
                }
                if (line[1] == "0.0" || line[2] == "0.0")
                {
                    continue;
                }
                double x = Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1]));
                double y = Math.Abs(double.Parse(line[2]) - double.Parse(prev_line[2]));
                double dist = Math.Sqrt((x * x) + (y * y));

                if (dist > high)
                {
                    high = dist;
                }
                if (dist < low)
                {
                    low = dist;
                }
                average += dist;
            }
            average /= lines.Length;
        }

        static void intro()
        {
            List<int> points_to_be_zeroed = new List<int>();
            Console.Clear();
            string filler = "";
            for (int i = 0; i < 50; i++)
            {
                filler += "~";
            }
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Enter file location and press ENTER.\n~ (HARDCODED TO @INPUT.TXT)");

            string file_location = "input.txt";//Console.ReadLine();

              Console.ReadLine();
            string[] lines = File.ReadAllLines(@"input.txt");////file_location);//@"input.txt");

            for (int i = 0; i < lines.Length; i +=100)
            {
                Console.WriteLine(lines[i]);
            }
            double old_high, old_average, old_low;

            getHighLowAverage(lines);

            old_high = high; old_average = average; old_low = low;
            Console.Clear();


            string[] prev_line;
            string[] line;

            double sum = 0;
            
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                double x = Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1]));
                double y = Math.Abs(double.Parse(line[2]) - double.Parse(prev_line[2]));
                double dist = Math.Sqrt((x * x) + (y * y));

                if (dist > .001) //.0002 seems common --> e-9
                {
                    //Extreme outlyers
                    Console.WriteLine("Line[" + i + "] distance = " + Math.Sqrt((x * x) + (y * y)));
                    points_to_be_zeroed.Add(i-1);
                }
                else if (dist < .000000001)
                {
                    Console.WriteLine("Line[" + i + "] distance = " + Math.Sqrt((x * x) + (y * y)));
                    points_to_be_zeroed.Add(i - 1);
                }
                else
                {
                    sum += dist;
                } 
            }
            for (int i = 0; i < points_to_be_zeroed.Count; i++)
            {
                line = lines[points_to_be_zeroed[i]].Split('	');
                lines[points_to_be_zeroed[i]] = line[0] + "	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0";
            }
            

            File.WriteAllText(@"output.txt", "");
            using (var dest = File.AppendText(@"output.txt"))
            {
                for (int i = 1; i < lines.Length; i += 1)
                {
                    dest.WriteLine(lines[i] + "\n");

                    if (i % 100 == 0)
                    {
                        Console.WriteLine("~ " + ((double)i / lines.Length).ToString("p") + " the way there!");
                    }
                }
            }
            
            Console.Clear();
            getHighLowAverage(lines);
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Reading from: " + file_location);
            Console.WriteLine("~ Writing to: " + "output.txt");
            Console.WriteLine(filler);
            Console.WriteLine("~ Rewrote .TXT with removed points.");
            Console.WriteLine(filler);
            Console.WriteLine("~ Results: ");
            Console.WriteLine("~ Removed " + points_to_be_zeroed.Count + " points out of " + lines.Length);
            Console.WriteLine("~ Old Range ");
            Console.WriteLine("~ High: " + old_high + ", Average: " + old_average + ", Low: " + old_low);
            Console.WriteLine("~ New Range ");
            Console.WriteLine("~ High: " + high + ", Average: " + average + ", Low: " + low);
            Console.WriteLine(filler);
            Console.WriteLine("~ Enter 1 to rerun process. Enter 0 to close.");
            Console.WriteLine(filler);

            response = int.Parse(Console.ReadLine());

            if (response == 0)
            {
                Environment.Exit(0);
            }
        }
    }
}

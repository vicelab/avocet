using System;
using System.Collections.Generic;
using System.IO;

namespace LatLongFixer
{
    class Program
    {
        static double high, low, average;
        static double x, y, dist;
        static string[] prev_line, line;

        static void Main(string[] args)
        {
            while(true) intro();
        }

        /* Give range of values of a given set of data */
        static void getHighLowAverage(string[] lines)
        {
            high = double.MinValue;
            low = double.MaxValue;
            average = 0;
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');
                /* Skip point if it has been zeroed out already */
                if (prev_line[1] == "0.0" || prev_line[2] == "0.0") continue;
                if (line[1] == "0.0" || line[2] == "0.0") continue;
                /* Calculate distance between two points */
                x = Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1]));
                y = Math.Abs(double.Parse(line[2]) - double.Parse(prev_line[2]));
                dist = Math.Sqrt((x * x) + (y * y));
                /* Check if distance is "significant" */
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
            Console.Clear();
            string filler = "";
            for (int i = 0; i < 50; i++) filler += "~";

            /* Get Introduction */
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Enter file location and press ENTER.\n~ (HARDCODED TO @INPUT.TXT)");

            /* Read data */
            string file_location = "input.txt";
            string[] lines = File.ReadAllLines(@"input.txt");

            /* See data being "read" */
            for (int i = 0; i < lines.Length; i += 100)
            {
                Console.WriteLine(lines[i]);
            }

            /* Get a snapshot of how "bad" data is before processing */
            getHighLowAverage(lines);
            double old_high, old_average, old_low;
            old_high = high; old_average = average; old_low = low;

            /* Remove points outside of acceptable range */
            List<int> points_to_be_zeroed = new List<int>();            
            for (int i = 1; i < lines.Length; i += 1)
            {
                prev_line = lines[i - 1].Split('	');
                line = lines[i].Split('	');

                /* Calculate distance between two points */
                x = Math.Abs(double.Parse(line[1]) - double.Parse(prev_line[1]));
                y = Math.Abs(double.Parse(line[2]) - double.Parse(prev_line[2]));
                dist = Math.Sqrt((x * x) + (y * y));

                /* Check if distance is out of range */
                if (dist > .001 || dist < .000000001)
                {
                    Console.WriteLine("Line[" + i + "] distance = " + Math.Sqrt((x * x) + (y * y)));
                    points_to_be_zeroed.Add(i - 1);
                }
            }

            /* Zero out points that are out of range */
            for (int i = 0; i < points_to_be_zeroed.Count; i++)
            {
                line = lines[points_to_be_zeroed[i]].Split('	');
                lines[points_to_be_zeroed[i]] = line[0] + "	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0";
            }
            
            /* Prime output file for data */
            File.WriteAllText(@"output.txt", "");
            using (var dest = File.AppendText(@"output.txt"))
            {
                /* Write data to output file */
                for (int i = 1; i < lines.Length; i += 1)
                {
                    dest.WriteLine(lines[i] + "\n");
                    if (i % 100 == 0) Console.WriteLine("~ " + ((double)i / lines.Length).ToString("p") + " the way there!");
                }
            }
            /* Display Results */
            Console.Clear();
            getHighLowAverage(lines);
            Console.WriteLine(filler);
            Console.WriteLine("~ Welcome to the LatLongFixer, Patented by Brian ~");
            Console.WriteLine(filler);
            Console.WriteLine("~ Reading from: " + file_location);
            Console.WriteLine("~ Writing to: " + "output.txt");
            Console.WriteLine(filler);
            Console.WriteLine("~ Results: ");
            Console.WriteLine("~ Removed " + points_to_be_zeroed.Count + " points out of " + lines.Length);
            Console.WriteLine("~ Old Range ");
            Console.WriteLine("~ High: " + old_high + ", Average: " + old_average + ", Low: " + old_low);
            Console.WriteLine("~ New Range ");
            Console.WriteLine("~ High: " + high + ", Average: " + average + ", Low: " + low);
            Console.WriteLine(filler);

            /* Exit */
            Console.ReadLine();
            Environment.Exit(0);
            
        }
    }
}

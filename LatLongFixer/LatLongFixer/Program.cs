using System;

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
        }
    }
}

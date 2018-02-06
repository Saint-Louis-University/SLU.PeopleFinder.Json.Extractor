using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLU.PeopleFinder.Json
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Extractor.GetEmail("schuelke", 2));
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VStepanov.Experiments.Vinyl.Imaging;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var vinyl = new Vinyl("../../../TEST_DATA/06.png");

            Console.WriteLine(vinyl.TrackWidth);
            Console.WriteLine(vinyl.GapWidth);
            Console.WriteLine(vinyl.SpinCount);
        }
    }
}

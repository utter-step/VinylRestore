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
            var image = Image.FromFile("../../../TEST_DATA/04.png");

            Console.WriteLine(image[900, 140]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;
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

            Console.WriteLine(vinyl.Duration);

            var res = vinyl.ExtractAudioBytes();

            WaveFormat format = new WaveFormat((int)(res.Length / vinyl.Duration.Seconds), 8, 1);

            using (var writer = new WaveFileWriter("out.wav", format))
            {
                writer.Write(res, 0, res.Length);
            }

            Console.WriteLine(vinyl.ComputeCenterX());
            Console.WriteLine(vinyl.ComputeCenterY());
        }
    }
}

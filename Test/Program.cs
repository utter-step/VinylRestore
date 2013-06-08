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

            Console.WriteLine("Center of this plate:\t{0}", vinyl.Center);

            Console.WriteLine("Average width of one track:\t{0}", vinyl.TrackWidth);
            Console.WriteLine("Average width of one gap:\t{0}", vinyl.GapWidth);
            Console.WriteLine("Approximate count of spins:\t{0}", vinyl.SpinCount);

            Console.WriteLine("Approximate duration:\t{0}", vinyl.Duration);

            var res = vinyl.ExtractAudioBytes();

            WaveFormat format = new WaveFormat((int)(res.Length / vinyl.Duration.Seconds), 8, 1);

            using (var writer = new WaveFileWriter("out.wav", format))
            {
                writer.Write(res, 0, res.Length);
            }

            var play = new Microsoft.VisualBasic.Devices.Audio();

            play.Play("out.wav", Microsoft.VisualBasic.AudioPlayMode.WaitToComplete);
        }
    }
}

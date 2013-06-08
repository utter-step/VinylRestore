using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;
using VStepanov.Experiments.Vinyl.Imaging;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var vinyl = new Vinyl(args[0]);

            byte[] res;

            if (args.Length < 2)
            {
                res = vinyl.ExtractAudioBytes(Vinyl.ExtractionOptions.None);
            }
            else
            {
                res = vinyl.ExtractAudioBytes(Vinyl.ExtractionOptions.SaveTrack, args[1]);
            }

            stopwatch.Stop();

            Console.WriteLine("Center of this plate:\t{0}", vinyl.Center);

            Console.WriteLine("Average width of one track:\t{0:f3}", vinyl.TrackWidth);
            Console.WriteLine("Average width of one gap:\t{0:f3}", vinyl.GapWidth);
            Console.WriteLine("Approximate count of spins:\t{0}", vinyl.SpinCount);

            Console.WriteLine("Approximate duration:\t{0}", vinyl.Duration);

            Console.WriteLine("\nAll computations took {0} ms.", stopwatch.ElapsedMilliseconds);

            var format = new WaveFormat((int)(res.Length / vinyl.Duration.Seconds), 8, 1);

            var outputFilename = String.Format("{0}.wav", args[0]);

            using (var writer = new WaveFileWriter(outputFilename, format))
            {
                writer.Write(res, 0, res.Length);
            }

            var play = new Microsoft.VisualBasic.Devices.Audio();

            play.Play(outputFilename, Microsoft.VisualBasic.AudioPlayMode.WaitToComplete);
        }
    }
}

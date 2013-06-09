using System;
using System.Diagnostics;

using VStepanov.Experiments.Vinyl.Audio;
using VStepanov.Experiments.Vinyl.Imaging;

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
                res = Vinyl.ExtractAudioBytes(vinyl, Vinyl.ExtractionOptions.None);
            }
            else
            {
                res = Vinyl.ExtractAudioBytes(vinyl, Vinyl.ExtractionOptions.SaveTrack);
            }

            stopwatch.Stop();

            Console.WriteLine("Center of this plate:\t{0}", vinyl.Center);

            Console.WriteLine("Average width of one track:\t{0:f3}", vinyl.TrackWidth);
            Console.WriteLine("Average width of one gap:\t{0:f3}", vinyl.GapWidth);
            Console.WriteLine("Approximate count of spins:\t{0}", vinyl.SpinCount);

            Console.WriteLine("Approximate duration:\t{0}", vinyl.Duration);

            Console.WriteLine("\nAll computations took {0} ms.", stopwatch.ElapsedMilliseconds);
            
            var outputFilename = String.Format("{0}.wav", args[0]);

            using (var writer = new WavPcmWriter(res.Length / vinyl.Duration.Seconds, 8, 1, outputFilename))
            {
                writer.Write(res, 0);
            }

            var play = new Microsoft.VisualBasic.Devices.Audio();

            play.Play(outputFilename, Microsoft.VisualBasic.AudioPlayMode.WaitToComplete);
        }
    }
}

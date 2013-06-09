using System;
using System.Diagnostics;
using VStepanov.Experiments.Vinyl.Audio;
using VStepanov.Experiments.Vinyl.Imaging;

namespace VStepanov.Experiments.Vinyl.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            Console.WriteLine("Recovering...");

            stopwatch.Start();
            var vinyl = new Imaging.Vinyl(args[0]);

            byte[] res;

            if (args.Length < 2)
            {
                res = vinyl.ExtractAudioBytes(Imaging.Vinyl.ExtractionOptions.None);
            }
            else
            {
                res = vinyl.ExtractAudioBytes(Imaging.Vinyl.ExtractionOptions.SaveTrack, args[1]);
            }
            stopwatch.Stop();

            Console.WriteLine("Recovered in {0} ms.", stopwatch.ElapsedMilliseconds);

            Console.WriteLine("\nCenter of this plate:\t{0}", vinyl.Center);

            Console.WriteLine("Average width of one track:\t{0:f3}", vinyl.TrackWidth);
            Console.WriteLine("Average width of one gap:\t{0:f3}", vinyl.GapWidth);
            Console.WriteLine("Approximate count of spins:\t{0}", vinyl.SpinCount);

            Console.WriteLine("Approximate duration:\t{0}", vinyl.Duration);

            Console.WriteLine("\nSaving...");

            stopwatch.Restart();
            var outputFilename = String.Format("{0}.wav", args[0]);

            using (var writer = new WavPcmWriter(res.Length / vinyl.Duration.Seconds, 8, 1))
            {
                writer.Write(res, outputFilename);
            }
            stopwatch.Stop();

            Console.WriteLine("\nSaved in {0} ms.", stopwatch.ElapsedMilliseconds);
        }
    }
}

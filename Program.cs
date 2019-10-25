using System;
using System.Drawing;
using CommandLine;

namespace TextureSynthesisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CommandLine.Parser.Default.ParseArguments<CLIOptions>(args)
            .WithParsed<CLIOptions>(o =>
            {
                Console.WriteLine("running with options");
                Bitmap sample = new Bitmap(o.Sample);
                int[] sampleArray = TextureSynthesis.GetPixels1D(sample);
                int width = sample.Width;
                int height = sample.Height;

                int[] outputArray = new int[o.Width * o.Height];
                switch (o.Method)
                {
                    case 0:
                        outputArray = TextureSynthesis.FullSynthesis(sampleArray, width, height, o.N, o.Width, o.Height, o.Temperature, o.Indexed);
                        break;
                    case 1:
                        outputArray = TextureSynthesis.CoherentSynthesis(sampleArray, width, height, o.K, o.N, o.Width, o.Height, o.Indexed, o.Temperature);
                        break;
                    case 2:
                        outputArray = TextureSynthesis.ReSynthesis(sampleArray, width, height, o.N, o.M, o.Polish, o.Indexed, o.Width, o.Height);
                        break;
                    default:
                        break;
                }

                Bitmap output = new Bitmap(o.Width, o.Height);

                for (int j = 0; j < o.Width * o.Height; j++)
                    output.SetPixel(j % o.Width, j / o.Width, Color.FromArgb(outputArray[j]));

                output.Save($"test.png");

            })
            ;

            //TextureSynthesis.RunAllExamples();
        }
    }
}

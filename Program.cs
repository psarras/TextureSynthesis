using System;
using System.Drawing;
using System.IO;
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
                string parameters = $"pass {o.Method} {o.Indexed} N={o.N} ";
                switch (o.Method)
                {
                    case 0:
                        outputArray = TextureSynthesis.FullSynthesis(sampleArray, width, height, o.N, o.Width, o.Height, o.Temperature, o.Indexed);
                        parameters += $"t={o.Temperature}";
                        break;
                    case 1:
                        outputArray = TextureSynthesis.CoherentSynthesis(sampleArray, width, height, o.K, o.N, o.Width, o.Height, o.Indexed, o.Temperature);
                        parameters += $"K={o.K} t={o.Temperature}";
                        break;
                    case 2:
                        outputArray = TextureSynthesis.ReSynthesis(sampleArray, width, height, o.N, o.M, o.Polish, o.Indexed, o.Width, o.Height);
                        parameters += $"M={o.M} polish={o.Polish}";
                        break;
                    default:
                        break;
                }

                Bitmap output = new Bitmap(o.Width, o.Height);

                for (int j = 0; j < o.Width * o.Height; j++)
                    output.SetPixel(j % o.Width, j / o.Width, Color.FromArgb(outputArray[j]));

                var filename = Path.GetFileNameWithoutExtension(o.Sample);
                var extention = Path.GetExtension(o.Sample);
                var outputFile = $"{filename} {parameters}{o.Suffix}{extention}";
                outputFile = Path.Combine(o.Output, outputFile);
                if (!o.Output.Equals("") && !Directory.Exists(o.Output))
                    Directory.CreateDirectory(o.Output);

                Console.WriteLine($"Writing to File: {outputFile}");
                output.Save(outputFile);
            })
            ;
        }
    }
}

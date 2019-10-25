using CommandLine;

namespace TextureSynthesisConsole
{
    public class CLIOptions
    {
        [Option('s', "sample", Required = true, HelpText = "Image file to use for synthesis")]
        public string Sample { get; set; }

        [Option('m', "method", Required = true, HelpText = "Method to use for synthesis synthesis, 0 = Full, 1 = Coherent, 2 = Harrison")]
        public int Method { get; set; }

        [Option('o', "Output", Required = false, Default = "", HelpText = "Output directory")]
        public string Output { get; set; }

        [Option('u', "Suffix", Required = false, Default = "", HelpText = "suffix to use in the end to differentiate between multiple samples")]
        public string Suffix { get; set; }

        [Option('W', "Width", Required = false, Default = 32, HelpText = "New Image width")]
        public int Width { get; set; }

        [Option('H', "Height", Required = false, Default = 32, HelpText = "New Image height")]
        public int Height { get; set; }

        [Option('N', "N_parameter", Required = false, Default = 1, HelpText = "Special parameter N")]
        public int N { get; set; }

        [Option('I', "Indexed", Required = false, Default = true, HelpText = "Index flag")]
        public bool Indexed { get; set; }

        [Option('t', "temperature", Required = false, Default = 1, HelpText = "Temp for Full and Coherent")]
        public float Temperature { get; set; }

        [Option('K', "K_parameter", Required = false, Default = 1, HelpText = "Special parameter K")]
        public int K { get; set; }

        [Option('M', "M_parameter", Required = false, Default = 20, HelpText = "Special parameter M")]
        public int M { get; set; }

        [Option('P', "Polish", Required = false, Default = 3, HelpText = "Special parameter Polish")]
        public int Polish { get; set; }
    }
}

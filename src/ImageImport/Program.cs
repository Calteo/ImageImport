using ImageImport.Options;
using Toolbox.CommandLine;

namespace ImageImport
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            var parser = new Parser(typeof(ImportOptions), typeof(RegisterOptions), typeof(UnregisterOptions));
            var rc = parser.Parse(args)
                .OnError(ShowError)
                .OnHelp(ShowHelp)
                .On<RegisterOptions>(RegisterAutoplay)
                .On<UnregisterOptions>(UnregisterAutoplay)
                .On<ImportOptions>(RunApplication)
                .Return;
            
            return rc;
        }

        private static int UnregisterAutoplay(UnregisterOptions arg)
        {
            AutoPlay.Unregister();
            return 0;
        }

        private static int RegisterAutoplay(RegisterOptions arg)
        {
            AutoPlay.Register();
            return 0;
        }

        private static int RunApplication(ImportOptions options)
        {
            Application.Run(new MainForm { Options = options });
            return 0;
        }

        private static int ShowHelp(ParseResult result)
        {
            var text = result.GetHelpText();
            MessageBox.Show(text, "Image Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 1;
        }

        private static int ShowError(ParseResult result)
        {
            MessageBox.Show(result.Text, "Image Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return 2;
        }
    }
}
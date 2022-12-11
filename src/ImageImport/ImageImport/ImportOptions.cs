using System.ComponentModel;
using Toolbox.CommandLine;

namespace ImageImport
{
    internal class ImportOptions
    {
        [Option("folder"), Position(0), DefaultValue("")]
        public string Folder { get; set; } = "";
    }
}

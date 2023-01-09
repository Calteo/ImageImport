using System.ComponentModel;
using Toolbox.CommandLine;

namespace ImageImport.Options
{
    [Description("import images")]
    internal class ImportOptions
    {
        [Option("folder"), Position(0), DefaultValue("")]
        [Description("Folder to scan for images")]
        public string Folder { get; set; } = "";
    }
}

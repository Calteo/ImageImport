using System.IO;
using Toolbox.Xml.Serialization;

namespace ImageImport
{
    internal class ImportToken
    {
        public DateTime LastImport { get; set; }        

        public static string FileName => $"ImageImport_{Environment.MachineName}_{Environment.UserDomainName}_{Environment.UserName}.token";

        internal static ImportToken Parse(Stream stream)
        {
            var formatter = new XmlFormatter<ImportToken>();
            return formatter.Deserialize(stream);
        }

        internal void Serialize(Stream stream)
        {
            var formatter = new XmlFormatter<ImportToken>();
            formatter.Serialize(this, stream);
        }
    }
}

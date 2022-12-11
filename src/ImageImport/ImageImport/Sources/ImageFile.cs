using System.IO;

namespace ImageImport.Sources
{
    internal class ImageFile
    {
        public ImageFile(string fullName)
        {
            FullName = fullName;
            Name = Path.GetFileName(fullName);
            Extension = Path.GetExtension(fullName);

            Parameters.Add("name", Name);
        }

        public string FullName { get; }
        public string Name { get; }
        public string Extension { get; }

        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
    }
}

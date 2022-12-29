using System.IO;

namespace ImageImport.Sources
{
    internal abstract class ImageFileBase
    {
        public ImageFileBase(ImageSource source, string fullName, DateTime created)
        {
            Source = source;

            FullName = fullName;
            Created = created;

            Name = Path.GetFileName(fullName);
            Extension = Path.GetExtension(fullName);

            Parameters.Add("name", Name);

            MetaDictionary.Add("Name", Name, "name of file");
            MetaDictionary.Add("Created", Created, "creation timestamp of file");
        }

        public ImageSource Source { get; }
        public string FullName { get; }
        public string Name { get; }
        public string Extension { get; }
        public DateTime Created { get; }

        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        public MetaDictionary MetaDictionary { get; } = new MetaDictionary();
        
        public void GetMetadata()
        {
            using var stream = GetStream();
            MetaDictionary.ParseFrom(stream);
        }

        public abstract Stream GetStream();
        public abstract void Copy(string target);
    }
}

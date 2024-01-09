using System.IO;

namespace ImageImport.Sources
{
    internal class DriveFile : ImageFile<DriveSource>
    {
        public DriveFile(DriveSource source, string fullName, DateTime created) : base(source, fullName, created)
        {
        }

        public override void Copy(string target)
        {
            File.Copy(FullName, target, true);
        }

        public override Stream GetStream()
        {
            return new FileStream(FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override void Delete()
        {
            File.Delete(FullName);
        }
    }
}

using System.IO;
using ImageImport.Icons;

namespace ImageImport.Sources
{
    internal class FtpSource : ImageSource
    {
        public override Icon GetIcon()
        {
            return IconStore.GetIcon("download", 32);
        }

        public override IEnumerable<ImageFileBase> EnumerateFiles()
        {
            throw new NotImplementedException();
        }

        protected override void Copy(ImageFileBase file, string target)
        {
            throw new NotImplementedException();
        }

        protected override Stream? OpenToken(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override void SaveToken(Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}

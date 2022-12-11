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

        public override IEnumerable<ImageFile> EnumerateFiles()
        {
            throw new NotImplementedException();
        }

        public override Stream GetStream(ImageFile file)
        {
            throw new NotImplementedException();
        }

        protected override void Copy(ImageFile file, string target)
        {
            throw new NotImplementedException();
        }
    }
}

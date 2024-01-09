using System.IO;

namespace ImageImport.Sources
{
    internal class FtpFile : ImageFile<FtpSource>
    {
        public FtpFile(FtpSource source, string fullName, DateTime created) : base(source, fullName, created)
        {
        }

        public override void Copy(string target)
        {
            var stream = GetStream();
            using var file = new FileStream(target, FileMode.Create, FileAccess.Write, FileShare.None);
            stream.CopyTo(file);
            file.Close();

            File.SetCreationTime(target, Created);
            File.SetLastWriteTime(target, Created);
        }

        public byte[]? Buffer { get; set; }

        public override Stream GetStream()
        {
            if (Buffer == null)
            {
                var stream = Source.GetStream(FullName)
                    ?? throw new FileNotFoundException(FullName);

                Buffer = new byte[stream.Length];
                var offset = 0;

                while (offset < Buffer.Length)
                {
                    var got = stream.Read(Buffer, offset, Buffer.Length - offset);
                    offset += got;
                }
            }

            return new MemoryStream(Buffer);
        }

        public override void Delete()
        {
            Source.Delete(FullName);
        }
    }
}

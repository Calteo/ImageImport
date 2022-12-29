using System.ComponentModel;
using System.IO;
using FluentFTP;
using ImageImport.Icons;
using Toolbox;

namespace ImageImport.Sources
{
    internal class FtpSource : ImageSource
    {
        private const string DefaultServer = "";
        [Description("Name of server"), DefaultValue(DefaultServer)]
        public string Server { get; set; } = DefaultServer;

        private const int DefaultPort = 25;
        [Description("Port of server"), DefaultValue(DefaultPort)]
        public int Port { get; set; } = DefaultPort;

        private const string DefaultFolder = "";
        [Description("Base folderr of files"), DefaultValue(DefaultFolder)]
        public string Folder { get; set; } = DefaultFolder;

        private const string DefaultUser = "";
        [Description("Username for logon"), DefaultValue(DefaultUser)]
        public string User { get; set; } = DefaultUser;

        private const string DefaultPassword = "";
        [Description("Password for logon"), DefaultValue(DefaultPassword), PasswordPropertyText(true)]
        public string Password { get; set; } = DefaultPassword;

        public override string Description => Server.NotEmpty() ? $"{Server}:{Port}" : "<unknown server>";

        protected override void Connect()
        {
            FtpClient = new FtpClient(Server, User, Password, Port);
            var profile = FtpClient.AutoConnect();

            Tracer.TraceVerbose($"connected {Server}:{Port} with user '{User}' profile={profile.DataConnection}/{profile.Protocols}");
        }

        protected override void Disconnect()
        {
            Tracer.TraceVerbose($"disconnect");

            FtpClient?.Disconnect();
            FtpClient = null;
        }

        public FtpClient? FtpClient { get; set; }

        public override Icon GetIcon()
        {
            return IconStore.GetIcon("download", 32);
        }        

        public override IEnumerable<ImageFileBase> EnumerateFiles()
        {
            if (FtpClient == null) yield break;

            var queue = new Queue<string>();
            queue.Enqueue(Folder);

            while (queue.Count > 0)
            {
                var folder = queue.Dequeue();

                foreach (var file in FtpClient.GetListing(folder))
                {
                    if (file.Type == FtpObjectType.File)
                    {
                        yield return new FtpFile(this, folder + file.Name, file.Modified);
                    }
                    else if (Recursive && file.Type == FtpObjectType.Directory)
                        queue.Enqueue(folder + file.Name + "/");
                }
            }
        }

        protected override void Copy(ImageFileBase file, string target)
        {
            throw new NotImplementedException();
        }

        protected override Stream? OpenToken(string fileName)
        {
            if (FtpClient==null || !FtpClient.FileExists(Folder + fileName)) return null;

            return FtpClient.OpenRead(fileName, FtpDataType.ASCII);            
        }

        protected override void SaveToken(Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }

        internal Stream? GetStream(string fullName)
        {
            return FtpClient?.OpenRead(fullName, FtpDataType.Binary);
        }
    }
}

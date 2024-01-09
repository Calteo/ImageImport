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
        [Category(CategorySource)]
        public string Server { get; set; } = DefaultServer;

        private const int DefaultPort = 25;
        [Description("Port of server"), DefaultValue(DefaultPort)]
        [Category(CategorySource)]
        public int Port { get; set; } = DefaultPort;

        private const string DefaultFolder = "";
        [Description("Base folderr of files"), DefaultValue(DefaultFolder)]
        [Category(CategorySource)]
        public string Folder { get; set; } = DefaultFolder;

        private const string DefaultUser = "";
        [Description("Username for logon"), DefaultValue(DefaultUser)]
        [Category(CategorySource)]
        public string User { get; set; } = DefaultUser;

        private const string DefaultPassword = "";
        [Description("Password for logon"), DefaultValue(DefaultPassword), PasswordPropertyText(true)]
        [Category(CategorySource)]
        public string Password { get; set; } = DefaultPassword;

        public override string Description => $"{base.Description} - {(Server.NotEmpty() ? $"{Server}:{Port}" : "<unknown server>")}";

        protected override bool Connect()
        {
            FtpClient = new FtpClient(Server, User, Password, Port);
            var profile = FtpClient.AutoConnect();
            
            if (profile == null)
            {
                Tracer.TraceError($"not connected {Server}:{Port} with user '{User}'.");
                return false;
            }

            Tracer.TraceVerbose($"connected {Server}:{Port} with user '{User}' profile={profile.DataConnection}/{profile.Protocols}");
            return FtpClient.IsConnected;
        }

        protected override void Disconnect()
        {
            Tracer.TraceVerbose($"disconnect");

            FtpClient?.Disconnect();
            FtpClient = null;
        }

        private FtpClient? FtpClient { get; set; }

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
                        var fullname = folder + (folder.EndsWith("/") ? "" : "/") + file.Name;
                        yield return new FtpFile(this, fullname, file.Modified);
                    }
                    else if (Recursive && file.Type == FtpObjectType.Directory)
                        queue.Enqueue(folder + file.Name + "/");
                }
            }
        }

        protected override Stream? OpenTokenRead()
        {
            if (FtpClient==null || !FtpClient.FileExists(Folder + ImportToken.FileName)) return null;

            return FtpClient.OpenRead(ImportToken.FileName, FtpDataType.ASCII);            
        }

        protected override Stream? OpenTokenWrite()
        {
            if (FtpClient == null || !FtpClient.FileExists(Folder + ImportToken.FileName)) return null;

            return FtpClient.OpenWrite(ImportToken.FileName, FtpDataType.ASCII);
        }

        internal Stream? GetStream(string fullName)
        {
            return FtpClient?.OpenRead(fullName, FtpDataType.Binary);
        }

        internal void Delete(string filename)
        {
            FtpClient?.DeleteFile(filename);
        }
    }
}

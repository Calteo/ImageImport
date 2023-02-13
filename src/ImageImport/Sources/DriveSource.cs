using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using ImageImport.Editors;
using ImageImport.Icons;
using Toolbox;

namespace ImageImport.Sources
{
    [DefaultProperty(nameof(Folder))]
    internal class DriveSource : ImageSource
    {        
        public override string Description => Folder.NotEmpty() ? Folder : "<unknown folder>";

        #region Folder
        private string folder = "";

        [Editor(typeof(FolderEditor), typeof(UITypeEditor))]
        // [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        [Description("Folder to scan for images"), DefaultValue("")]
        public string Folder 
        { 
            get => folder;
            set
            {
                if (folder == value) return;
                folder = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Description));
                ResetScan();
            }
        }
        #endregion

        public override Icon GetIcon()
        {
            return IconStore.GetIcon("folder", 32);
        }

        public override IEnumerable<ImageFileBase> EnumerateFiles()
        {
            var enumerable = Directory.EnumerateFiles(Folder, "*", Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return new DriveFile(this, enumerator.Current, File.GetCreationTime(enumerator.Current));
            }
        }

        protected override Stream? OpenTokenRead()
        {
            var fullFileName = Path.Combine(Folder, ImportToken.FileName);
            if (File.Exists(fullFileName))
                return new FileStream(fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            return null;
        }

        protected override Stream? OpenTokenWrite()
        {
            var fullFileName = Path.Combine(Folder, ImportToken.FileName);
            return new FileStream(fullFileName, FileMode.Create, FileAccess.Write, FileShare.None);            
        }

        protected override void Connect()
        {
        }

        protected override void Disconnect()
        {
        }
    }
}

using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using ImageImport.Editors;
using ImageImport.Icons;
using Toolbox;

namespace ImageImport.Sources
{
    [DefaultProperty(nameof(Folder))]
    internal class FileSource : ImageSource
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
            }
        }
        #endregion

        public override Icon GetIcon()
        {
            return IconStore.GetIcon("folder", 32);
        }

        public override IEnumerable<ImageFile> EnumerateFiles()
        {
            var enumerable = Directory.EnumerateFiles(Folder, "*", Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return new ImageFile(enumerator.Current);
            }
        }

        public override Stream GetStream(ImageFile file)
        {
            return new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        protected override void Copy(ImageFile file, string target)
        {
            File.Copy(file.FullName, target, true);
        }
    }
}

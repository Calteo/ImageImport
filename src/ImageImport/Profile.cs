using System.ComponentModel;
using System.Runtime.CompilerServices;
using ImageImport.Sources;

namespace ImageImport
{
    [DefaultProperty(nameof(Name))]
    internal class Profile : INotifyPropertyChanged
    {
        #region Name
        private const string NameDefaultValue = "New Profile";
        private string name = NameDefaultValue;
        [Description("Name of the profile"), DefaultValue(NameDefaultValue)]
        public string Name 
        { 
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Overwrite
        private const bool OverwriteDefault = false;
        private bool overwrite = OverwriteDefault;
        [DefaultValue(OverwriteDefault)]
        [Description("Overwrite existing files")]
        public bool Overwrite
        {
            get => overwrite;
            set
            {
                if (overwrite == value) return;
                overwrite = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region OnlyNewFiles
        private const bool OnlyNewFilesDefault = false;
        private bool onlyNewFiles = OnlyNewFilesDefault;
        [DefaultValue(OnlyNewFilesDefault), Description("Import only new files")]
        public bool OnlyNewFiles
        {
            get => onlyNewFiles;
            set
            {
                if (onlyNewFiles == value) return;
                onlyNewFiles = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public BindingList<ProfileFileType> FileTypes { get; set; } = new BindingList<ProfileFileType>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        internal ProfileFileType GetFileType(ImageFileBase file) => FileTypes.First(ft => ft.Match(file.Extension));
        internal bool CanImport(ImageFileBase file) => FileTypes.Any(ft => ft.Match(file.Extension));

    }
}

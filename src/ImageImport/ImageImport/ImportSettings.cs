using System.ComponentModel;
using ImageImport.Sources;
using Toolbox.Xml.Settings;

namespace ImageImport
{
    internal class ImportSettings : UserSetting
    {
        public BindingList<ImageSource> Sources { get; set; } = new BindingList<ImageSource>();
        public BindingList<Profile> Profiles { get; set; } = new BindingList<Profile>();
    }
}

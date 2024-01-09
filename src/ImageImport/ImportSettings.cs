using System.ComponentModel;
using System.Runtime.Serialization;
using ImageImport.Sources;
using Toolbox.Xml.Settings;

namespace ImageImport
{
    internal class ImportSettings : UserSetting
    {
        public BindingList<ImageSource> Sources { get; set; } = new BindingList<ImageSource>();
        public BindingList<Profile> Profiles { get; set; } = new BindingList<Profile>();        

        private BindingList<T> Sort<T>(BindingList<T> list, Comparison<T> comparison)
        {
            var sorted = list.ToList();
            sorted.Sort(comparison);
            return new BindingList<T>(sorted);
        }

        [OnDeserialized]
        void OnDeserialized(Dictionary<string, string> data)
        {
            Sources = Sort(Sources, (x, y) => x.Description.CompareTo(y.Description));
            Profiles = Sort(Profiles, (x, y) => x.Name.CompareTo(y.Name));
        }
    }
}

using System.ComponentModel;
using System.Data;
using Toolbox.Collection.Generics;

namespace ImageImport
{
    internal partial class EditProfileForm : Form
    {
        public EditProfileForm()
        {
            InitializeComponent();
        }

        public BindingList<Profile> Profiles { get; set; } = new BindingList<Profile>();
        public Profile? SelectedProfile { get; set; }

        private void EditProfileFormShown(object sender, EventArgs e)
        {
            Profiles.ListChanged += ProfilesListChanged;
            ProfilesListChanged(listView, new ListChangedEventArgs(ListChangedType.Reset, 0));
            if (SelectedProfile != null)
            {
                var item = listView.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == SelectedProfile);
                if (item != null)
                    item.Selected = true;
            }
        }

        private void ProfilesListChanged(object? sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                    {
                        listView.Items.Clear();
                        Profiles.ForEach(p => listView.Items.Add(CreateItem(p)));
                        break;
                    }
                case ListChangedType.ItemAdded:
                    {
                        listView.Items.Add(CreateItem(Profiles[e.NewIndex]));
                        listView.Sort();
                        break;
                    }
                case ListChangedType.ItemDeleted:
                    {
                        var removed = listView.Items.Cast<ListViewItem>().First(i => !Profiles.Contains((Profile)i.Tag));
                        listView.Items.Remove(removed);
                        break;
                    }
                case ListChangedType.ItemChanged:
                    {
                        if (e.PropertyDescriptor?.Name == nameof(Profile.Name))
                        {
                            var profile = Profiles[e.NewIndex];
                            var item = FindItem(profile);
                            item.Text = profile.Name;
                            listView.Sort();
                        }
                        break;
                    }
            }
        }

        private ListViewItem FindItem(Profile profile)
        {
            return listView.Items.Cast<ListViewItem>().First(i => i.Tag == profile);
        }

        private static ListViewItem CreateItem(Profile profile)
        {
            var item = new ListViewItem(profile.Name)
            {
                ImageKey = "profile",
                Tag = profile
            };
            return item;
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            var profile = new Profile();
            Profiles.Add(profile);
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            listView.SelectedItems.Cast<ListViewItem>()
                .Select(i => (Profile)i.Tag)
                .ToArray()
                .ForEach(s => Profiles.Remove(s));
        }

        private void ListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var selected = listView.SelectedItems.Cast<ListViewItem>().Select(i => i.Tag).ToArray();
            propertyGrid.SelectedObjects = selected;
            deleteButton.Enabled = propertyGrid.Enabled = selected.Length != 0;
        }
    }
}

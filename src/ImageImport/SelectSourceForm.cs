using System.ComponentModel;
using System.Data;
using System.Reflection;
using ImageImport.Sources;
using Toolbox.Collection.Generics;

namespace ImageImport
{
    internal partial class SelectSourceForm : Form
    {
        public SelectSourceForm()
        {
            InitializeComponent();
        }

        public BindingList<ImageSource> Sources { get; set; } = new BindingList<ImageSource>();
        public ImageSource? SelectedSource { get; set; }

        private void SelectSourceFormLoad(object sender, EventArgs e)
        {
            var sourceTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsAssignableTo(typeof(ImageSource)) && !t.IsAbstract)
                .OrderBy(t => t.Name);

            foreach (var sourceType in sourceTypes)
            {
                var button = new ToolStripMenuItem
                {
                    Text = sourceType.Name,                    
                    Tag = sourceType,
                    Image = imageListSources.Images[sourceType.Name]                    
                };
                button.Click += AddButtonClick;
                addButton.DropDownItems.Add(button);
            }
            
            Sources.ListChanged += SourcesListChanged;
            SourcesListChanged(listView, new ListChangedEventArgs(ListChangedType.Reset, 0));
            if (SelectedSource != null)
            {
                var item = listView.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == SelectedSource);
                if (item != null)
                    item.Selected = true;
            }
        }

        private void SourcesListChanged(object? sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                    {
                        listView.Items.Clear();
                        Sources.ForEach(s => listView.Items.Add(CreateItem(s)));
                        break;
                    }
                case ListChangedType.ItemAdded:
                    {
                        listView.Items.Add(CreateItem(Sources[e.NewIndex]));
                        listView.Sort();
                        break;
                    }
                case ListChangedType.ItemDeleted:
                    {
                        var removed = listView.Items.Cast<ListViewItem>().First(i => !Sources.Contains((ImageSource)i.Tag));
                        listView.Items.Remove(removed);
                        break;
                    }
                case ListChangedType.ItemChanged:
                    {
                        if (e.PropertyDescriptor?.Name == nameof(ImageSource.Description))
                        {
                            var source = Sources[e.NewIndex];
                            var item = FindItem(source);
                            item.Text = source.Description;
                            listView.Sort();
                        }
                        break;
                    }
            }
        }

        private ListViewItem FindItem(ImageSource source)
        {
            return listView.Items.Cast<ListViewItem>().First(i => i.Tag == source);
        }

        private ListViewItem CreateItem(ImageSource source)
        {
            var imageKey = source.GetType().Name;
            if (!imageListSources.Images.ContainsKey(imageKey))
            {
                imageListSources.Images.Add(imageKey, source.GetIcon());
            }
            var item = new ListViewItem(source.Description)
            {                
                ImageKey = imageKey,
                Tag = source
            };
            return item;
        }

        private void AddButtonClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem button)
            {
                if (button.Tag is Type sourceType)
                {
                    if (Activator.CreateInstance(sourceType) is ImageSource imageSource)
                    {
                        Sources.Add(imageSource);
                    }
                }
            }
        }

        private void ListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            /*
            var selected = listBox.SelectedItems.Cast<object>().ToArray();
            propertyGrid.SelectedObjects = selected;
            deleteButton.Enabled = propertyGrid.Enabled = selected.Length != 0;
            */
        }

        private void ListBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is ImageSource source)
            {
                e.Value = source.Description;
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            listView.SelectedItems.Cast<ListViewItem>()
                .Select(i => (ImageSource)i.Tag)
                .ToArray()
                .ForEach(s => Sources.Remove(s));            
        }

        private void ListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var selected = listView.SelectedItems.Cast<ListViewItem>().Select(i => i.Tag).ToArray();
            propertyGrid.SelectedObjects = selected;
            deleteButton.Enabled = propertyGrid.Enabled = selected.Length != 0;
        }
    }
}

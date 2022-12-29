using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using ImageImport.Icons;
using ImageImport.Sources;
using Toolbox;
using Toolbox.Collection.Generics;
using Toolbox.Xml.Settings;

namespace ImageImport
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Settings = UserSettings.Get<ImportSettings>();

            ProtocolListener = new ProtocolTraceListener(textBoxProtocol);

            noneProtocolButton.Tag = 0;
            informationProtocolButton.Tag = SourceLevels.Information;
            warningProtocolButton.Tag = SourceLevels.Warning;
            errorProtocolButton.Tag = SourceLevels.Error;
            criticalProtocolButton.Tag = SourceLevels.Critical;
            verboseProtocolButton.Tag = SourceLevels.Verbose;

            LevelButtons = new[]
            {
                noneProtocolButton,
                informationProtocolButton,
                warningProtocolButton,
                errorProtocolButton,
                criticalProtocolButton,
                verboseProtocolButton
            };

            dateTimeProtocolButton.Tag = TraceOptions.DateTime;
            contextProtocolButton.Tag = TraceOptions.LogicalOperationStack;
            threadProtocolButton.Tag = TraceOptions.ThreadId;

            Tracer.Listener.Add(ProtocolListener);
            Trace.AutoFlush = true;

            quitMenuItem.Image = IconStore.GetIcon("x_circle", 16).ToBitmap();
            versionMenuItem.Image = IconStore.GetIcon("info", 16).ToBitmap();
            licenceMenuItem.Image = IconStore.GetIcon("award", 16).ToBitmap();

            activityProtocolButton.PerformClick();
            warningProtocolButton.PerformClick();
            // errorProtocolButton.PerformClick();
            // verboseProtocolButton.PerformClick();

            dateTimeProtocolButton.PerformClick();
            contextProtocolButton.PerformClick();
        }

        public ProtocolTraceListener ProtocolListener { get; }

        public ImportSettings Settings { get; }
        public ImportOptions Options { get; set; } = new ImportOptions();

        private void MainFormLoad(object sender, EventArgs e)
        {
            layoutPanel.RowStyles[0].Height
                = layoutPanel.RowStyles[1].Height
                = comboBoxSource.Height + comboBoxSource.Margin.Vertical;

            comboBoxSource.DataSource = Settings.Sources;
            comboBoxSource.DisplayMember = nameof(ImageSource.Description);

            comboBoxProfile.DataSource = Settings.Profiles;
            comboBoxProfile.DisplayMember = nameof(Profile.Name);
        }

        private void MainFormShown(object sender, EventArgs e)
        {
            if (Options.Folder.NotEmpty())
            {
                var source = Settings.Sources.OfType<DriveSource>().FirstOrDefault(s => s.Folder == Options.Folder);
                if (source == null)
                {
                    source = new DriveSource { Folder = Options.Folder };
                    Settings.Sources.Add(source);
                    Settings.Save();
                }

                comboBoxSource.SelectedItem = source;
            }
        }

        private void ButtonSelectSourceClick(object sender, EventArgs e)
        {
            var selectForm = new SelectSourceForm
            {
                Sources = Settings.Sources,
                SelectedSource = Source
            };
            selectForm.ShowDialog(this);
            Settings.Save();
            Source?.DispatchScan();
        }

        private void ComboBoxSourceFormat(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is ImageSource source)
            {
                e.Value = source.Description;
            }
        }

        private void ComboBoxProfileFormat(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Profile profile)
            {
                e.Value = profile.Name;
            }
        }

        private void ButtonEditProfileClick(object sender, EventArgs e)
        {
            var editForm = new EditProfileForm
            {
                Profiles = Settings.Profiles,
                SelectedProfile = Profile
            };
            editForm.ShowDialog(this);
            Settings.Save();
            UpdateFiles();
        }

        private void QuitMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void VersionMenuItemClick(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();

            MessageBox.Show(this, $"Version - {assembly.GetName().Version}\r\nby {company?.Company ?? "unknown"}", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LicenceMenuItemClick(object sender, EventArgs e)
        {
            var form = new LicenceForm();
            form.ShowDialog(this);
        }

        private void ComboBoxSourceSelectedValueChanged(object sender, EventArgs e)
        {
            Source = (ImageSource)comboBoxSource.SelectedItem;
        }

        private void ComboBoxSourceSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ComboBoxProfileSelectedValueChanged(object sender, EventArgs e)
        {
            Profile = (Profile)comboBoxProfile.SelectedItem;

            if (Profile != null)
            {
                checkBoxOverwrite.Enabled = true;
                checkBoxOverwrite.Checked = Profile.Overwrite;
                if (Source?.Token != null)
                {
                    checkBoxOnlyNewFiles.Enabled = true;
                    checkBoxOnlyNewFiles.Checked = Profile.OnlyNewFiles;
                }
            }
            else
            {
                checkBoxOverwrite.Enabled = false;
                checkBoxOverwrite.Checked = false;
                checkBoxOnlyNewFiles.Enabled = false;
                checkBoxOnlyNewFiles.Checked = false;
            }
            UpdateFiles();
        }

        private ImageSource? source;
        private Profile? profile;

        private ImageSource? Source
        {
            get => source;
            set
            {
                if (source == value) return;

                if (source != null)
                {
                    source.Files.ListChanged -= SourceFilesListChanged;
                    source.PropertyChanged -= SourcePropertyChanged;
                }

                source = value;
                if (source != null)
                {
                    UpdateFiles();

                    source.PropertyChanged += SourcePropertyChanged;
                    source.Files.ListChanged += SourceFilesListChanged;
                    source.DispatchScan();
                }
            }
        }

        private void SourceFilesListChanged(object? sender, ListChangedEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke(() => SourceFilesListChanged(sender, e));
            else
            {
                UpdateFiles();
            }
        }

        private List<ImageFileBase> Files { get; } = new List<ImageFileBase>();

        private void UpdateFiles()
        {
            buttonImport.Enabled = false;

            if (Source != null)
            {
                labelFiles.Text = $"{Source.State} {Source.Files.Count:#,##0} files";
                if (Profile != null)
                {
                    lock (Source.Files)
                    {
                        Files.Clear();

                        Files.AddRange(Source.Files
                            .Where(f => !checkBoxOnlyNewFiles.Enabled || !checkBoxOnlyNewFiles.Checked || f.Created > ( Source.LastImport ?? DateTime.MinValue) )
                            .Where(f => Profile.CanImport(f)));

                        labelFiles.Text += $" -> {Files.Count:#,##0} to import";
                        buttonImport.Enabled = Source.State == SourceState.Scanned && Files.Count > 0;
                    }
                }
            }
            else
                labelFiles.Text = "";
        }

        private void SourcePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke(() => SourcePropertyChanged(sender, e));
            else
            {
                if (sender is ImageSource source)
                {
                    switch (e.PropertyName)
                    {
                        case nameof(Source.State):
                            UpdateFiles();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Profile? Profile
        {
            get => profile;
            set
            {
                if (profile == value) return;
                profile = value;
                UpdateFiles();
            }
        }

        private void ClearProtocolButtonClick(object sender, EventArgs e)
        {
            textBoxProtocol.Text = "";
        }

        private void ButtonImportClick(object sender, EventArgs e)
        {
            if (Source == null || Profile == null) return;

            CancelImport = false;
            ImportThread = new Thread(Import)
            {
                Name = "Import",
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal
            };

            comboBoxSource.Enabled =
                buttonSelectSource.Enabled =
                comboBoxProfile.Enabled =
                buttonEditProfile.Enabled =
                buttonImport.Enabled = false;

            statusProgressBar.Value = 0;
            statusProgressBar.Maximum = Files.Count;
            statusProgressBar.Style = ProgressBarStyle.Continuous;
            statusProgressBar.Visible = true;

            statusLabel.Text = "Importing";

            textBoxProtocol.Text = "";
            ImportThread.Start(new Tuple<ImageSource, Profile>(Source, Profile));
        }

        private bool CancelImport { get; set; }
        public Thread? ImportThread { get; set; }

        private void Import(object? obj)
        {
            if (obj == null) throw new NullReferenceException($"no source and profile given for input.");
            var parameters = (Tuple<ImageSource, Profile>)obj;
            var source = parameters.Item1;
            var profile = parameters.Item2;

            try
            {
                Tracer.StartOperation("Import");

                Tracer.TraceStart($"from {source.Description} with profile {profile.Name}");

                source.InitImport();

                foreach (var file in Files)
                {
                    if (CancelImport) break;

                    source.Import(file, profile);

                    Invoke(() => statusProgressBar.Value++);
                }
            }
            catch (Exception exception)
            {
                Tracer.TraceException(exception, 547);
            }
            finally
            {
                source.CompleteImport();

                ImportThread = null;
                Invoke(ImportCompleted, source);
                

                Tracer.TraceStop("import");
                Tracer.StopOperation();
            }
        }

        private void ImportCompleted(ImageSource source)
        {
            comboBoxSource.Enabled =
                buttonSelectSource.Enabled =
                comboBoxProfile.Enabled =
                buttonEditProfile.Enabled =
                buttonImport.Enabled = true;

            var files = new List<string>();

            if (source.Imported > 0)
                files.Add($"{source.Imported} imported");
            if (source.Skipped > 0)
                files.Add($"{source.Skipped} skipped");
            if (source.Failed > 0)
                files.Add($"{source.Failed} failed");

            labelFiles.Text += $" -> {string.Join(" / ", files)}";

            statusLabel.Text = "Ready";
            statusProgressBar.Visible = false;
        }

        private void CheckBoxOverwriteCheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnlyNewFiles.Enabled && Profile != null)
            {
                Profile.Overwrite = checkBoxOverwrite.Checked;
                UpdateFiles();
            }
        }

        private void CheckBoxOnlyNewFilesCheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnlyNewFiles.Enabled && Profile != null)
            {
                Profile.OnlyNewFiles = checkBoxOnlyNewFiles.Checked;
                UpdateFiles();
            }
        }

        private void ActivityProtocolButtonClick(object sender, EventArgs e)
        {
            activityProtocolButton.Checked = !activityProtocolButton.Checked;
            if (activityProtocolButton.Checked)
                Tracer.Switch.Level |= SourceLevels.ActivityTracing;
            else
                Tracer.Switch.Level &= ~SourceLevels.ActivityTracing;
        }

        public ToolStripMenuItem[] LevelButtons { get; }
        private void LevelProtocolButtonClick(object sender, EventArgs e)
        {
            var button = (ToolStripMenuItem)sender;
            var level = (SourceLevels)button.Tag;

            Tracer.Switch.Level &= ~SourceLevels.Verbose;
            Tracer.Switch.Level |= level;

            LevelButtons.ForEach(b => b.Checked = false);
            button.Checked = true;
        }

        private void ToogleProtocolOutputOption(object sender)
        {
            if (sender is ToolStripMenuItem button)
            {
                button.Checked = !button.Checked;
                var flag = (TraceOptions)button.Tag;

                if (button.Checked)
                    ProtocolListener.TraceOutputOptions |= flag;
                else
                    ProtocolListener.TraceOutputOptions &= ~flag;
            }

        }

        private void OutputProtocolButtonClick(object sender, EventArgs e)
        {
            ToogleProtocolOutputOption(sender);
        }
    }
}
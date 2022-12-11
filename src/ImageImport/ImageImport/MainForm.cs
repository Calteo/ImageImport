using System.ComponentModel;
using System.Reflection;
using ImageImport.Sources;
using Toolbox;
using Toolbox.Xml.Settings;

namespace ImageImport
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            AppendProtocolAction = new Action<string>(AppendProtocol);

            Settings = UserSettings.Get<ImportSettings>();

            FoundFiles.ListChanged += FoundFilesListChanged;
        }

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
                var source = Settings.Sources.OfType<FileSource>().FirstOrDefault(s => s.Folder == Options.Folder);
                if (source == null)
                { 
                    source = new FileSource { Folder = Options.Folder };
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
            DispatchScan();
        }

        private void ComboBoxSourceFormat(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is ImageSource source)
            {
                e.Value = source.Description;
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
            DispatchScan();
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
            DispatchScan();
        }

        private void ComboBoxProfileSelectedValueChanged(object sender, EventArgs e)
        {
            if (Profile == null)
            {
                checkBoxOverwrite.Checked = false;
                checkBoxOverwrite.Enabled = false;
            }
            else
            {
                checkBoxOverwrite.Enabled = true;
                checkBoxOverwrite.Checked = Profile.Overwrite;
            }
            DispatchScan();
        }

        private ImageSource Source => (ImageSource)comboBoxSource.SelectedItem;
        private Profile Profile => (Profile)comboBoxProfile.SelectedItem;

        public BindingList<ImageFile> FoundFiles { get; } = new BindingList<ImageFile>();
        public List<ImageFile> SelectedFiles { get; } = new List<ImageFile>();

        private void FoundFilesListChanged(object? sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                    FilterFiles();
                    break;
                case ListChangedType.ItemAdded:
                    AddFile(FoundFiles[e.NewIndex]);
                    break;
            }
        }

        private void AddFile(ImageFile file)
        {
            AppendProtocolVerbose($"found {file.FullName}");
            if (Profile.CanImport(file))
            {
                SelectedFiles.Add(file);
                EnableImport();
            }
        }

        private void FilterFiles()
        {
            SelectedFiles.Clear();
            SelectedFiles.AddRange(FoundFiles.Where(f => Profile.CanImport(f)));
            EnableImport();
        }

        private void EnableImport()
        {
            labelFiles.Text = $"{SelectedFiles.Count:#,##0} images found.";
            buttonImport.Enabled = ScanThread == null && SelectedFiles.Any();
        }
        private void DispatchScan()
        {
            if (Source == null || Profile == null) return;

            UseWaitCursor = true;

            if (ScanThread != null)
            {
                CancelScan = true;
                ScanThread.Join();
                ScanThread = null;
            }
            
            comboBoxSource.Enabled = false;
            buttonSelectSource.Enabled = false;
            
            statusProgressBar.Style = ProgressBarStyle.Marquee;
            statusProgressBar.Visible = true;
            statusLabel.Text = "Scanning...";

            FoundFiles.Clear();
                       
            CancelScan = false;
            ScanThread = new Thread(Scan)
            {
                Name = "Scan",
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal
            };
            ScanThread.Start(Source);

            UseWaitCursor = false;
        }
        private bool CancelScan { get; set; }
        private Thread? ScanThread { get; set; }

        private void Scan(object? obj)
        {
            if (obj == null) throw new NullReferenceException("No image source given for scan.");

            var source = (ImageSource)obj;

            try
            {
                source.InitScan();

                foreach (var file in source.EnumerateFiles())
                {
                    if (CancelScan) break;
                    Invoke(() => AddFile(file));
                }                               
            }
            catch (Exception exception)
            {
                AppendProtocol(exception.ToString());
            }
            finally
            {
                source.CompleteScan();
                if (!CancelScan)
                {
                    ScanThread = null;
                    Invoke(ScanCompleted);
                }
            }
        }

        private void ScanCompleted()
        {
            statusLabel.Text = "Ready";
            statusProgressBar.Visible = false;

            comboBoxSource.Enabled = true;
            buttonSelectSource.Enabled = true;
            comboBoxProfile.Enabled = true;
            buttonEditProfile.Enabled = true;

            EnableImport();
        }

        private bool VerboseProtocol { get; set; }

        private Action<string> AppendProtocolAction { get; }
        private void AppendProtocol(string text)
        {
            if (InvokeRequired)
                BeginInvoke(AppendProtocolAction, text);
            else
            {
                if (textBoxProtocol.Text.Any())
                    textBoxProtocol.AppendText(Environment.NewLine);
                textBoxProtocol.AppendText(text);
                textBoxProtocol.SelectionLength = 0;
                textBoxProtocol.SelectionStart = textBoxProtocol.TextLength;
                textBoxProtocol.ScrollToCaret();
            }
        }

        private void AppendProtocolVerbose(string text)
        {
            if (VerboseProtocol)
                AppendProtocol(text);
        }

        private void ClearProtocolButtonClick(object sender, EventArgs e)
        {
            textBoxProtocol.Text = "";
        }

        private void VerboseProtocolButtonClick(object sender, EventArgs e)
        {
            verboseProtocolButton.Checked = VerboseProtocol = !VerboseProtocol;
        }

        private void ButtonImportClick(object sender, EventArgs e)
        {
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
            statusProgressBar.Maximum = SelectedFiles.Count;
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
                source.InitImport();

                foreach (var file in SelectedFiles)
                {
                    if (CancelImport) break;

                    AppendProtocol($"import {file.FullName}");
                    try
                    {
                        source.Import(file, profile, AppendProtocol, AppendProtocolVerbose);
                    }
                    catch (Exception importException)
                    {
                        AppendProtocol(importException.ToString());
                    }
                    
                    Invoke(() => statusProgressBar.Value++);                    
                }
            }
            catch (Exception exception)
            {
                AppendProtocol(exception.ToString());
            }
            finally
            {
                source.CompleteImport();
                ImportThread = null;
                Invoke(ImportCompleted);
            }
        }

        private void ImportCompleted()
        {
            AppendProtocol("");
            AppendProtocol("finished results:");
            if (Source.Imported > 0) AppendProtocol($"- imported {Source.Imported:#,##0}");
            if (Source.Skipped > 0) AppendProtocol($"- skipped {Source.Skipped:#,##0}");
            if (Source.Failed > 0) AppendProtocol($"- failed {Source.Failed:#,##0}");

            comboBoxSource.Enabled =
                buttonSelectSource.Enabled =
                comboBoxProfile.Enabled =
                buttonEditProfile.Enabled =
                buttonImport.Enabled = true;

            statusLabel.Text = "Ready";
            statusProgressBar.Visible = false;
        }

        private void CheckBoxOverwriteCheckedChanged(object sender, EventArgs e)
        {
            if (Profile != null) Profile.Overwrite = checkBoxOverwrite.Checked;
        }
    }
}
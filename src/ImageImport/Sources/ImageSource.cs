using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace ImageImport.Sources
{
    [DefaultProperty(nameof(Name))]
    internal abstract class ImageSource : INotifyPropertyChanged
    {
        protected const string CategoryCommon = "Common";
        protected const string CategoryOptions = "Options";
        protected const string CategoryState = "State";
        protected const string CategorySource = "Source";

        #region Name
        private const string DefaultName = "Name";
        private string _name = DefaultName;
        [Description("Name of source"), DefaultValue(DefaultName)]
        [Category(CategoryCommon)]
        public string Name 
        {
            get => _name;
            set
            {
                if (SetField(ref _name, value))
                {
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        #endregion

        [Browsable(false)]
        public virtual string Description => Name;

        #region Recursive
        private bool _recursive = true;
        [Description("Include subfolders"), DefaultValue(true)]
        [Category(CategoryOptions)]
        public bool Recursive
        {
            get => _recursive;
            set
            {
                if (SetField(ref _recursive, value))
                {
                    ResetScan();
                }
            }
        }
        #endregion

        protected void ResetScan()
        {
            State = SourceState.Unknown;
            CancelScan = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName ?? throw new ArgumentException("Property name not set.", nameof(propertyName)));
            return true;
        }

        public abstract Icon GetIcon();

        #region LastScan
        private DateTime dateTime = DateTime.MinValue;
        [Browsable(false)]
        [Category(CategoryState)]
        public DateTime LastScan
        {
            get => dateTime;
            set => SetField(ref dateTime, value);
        }
        #endregion

        [Browsable(false)]
        public BindingList<ImageFileBase> Files { get; } = new BindingList<ImageFileBase>();

        [Description("State of source")]
        #region State
        private const SourceState StateDefault = SourceState.Unknown;
        private SourceState sourceState = StateDefault;
        [DefaultValue(StateDefault)]
        [Category(CategoryState)]
        public SourceState State
        {
            get => sourceState;
            private set => SetField(ref sourceState, value);
        }
        #endregion

        public void DispatchScan()
        {
            if (State == SourceState.Unknown)
            {
                CancelScan = true;
                ScanThread?.Join();

                CancelScan = false;
                State = SourceState.Scanning;
                Files.Clear();

                ScanThread = new Thread(Scan)
                {
                    Name = $"Scan<{Description}>",
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal
                };
                ScanThread.Start();
            }
        }

        private bool CancelScan { get; set; }
        private Thread? ScanThread { get; set; }

        private void Scan()
        {
            var counters = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

            try
            {
                if (!InitScan()) return;

                var tokenName = ImportToken.FileName;

                Files.RaiseListChangedEvents = false;

                foreach (var file in EnumerateFiles())
                {
                    if (CancelScan) break;
                    
                    if (file.Name != tokenName)
                    {
                        Tracer.TraceVerbose($"file '{file.FullName}'");

                        if (counters.ContainsKey(file.Extension))
                            counters[file.Extension]++;
                        else
                            counters[file.Extension] = 1;

                        lock (Files)
                        {
                            Files.Add(file);
                        }
                        if (Files.Count % 500 == 0) // trigger a list event every 500 files
                        {
                            Files.RaiseListChangedEvents = true;
                            Files[0] = Files[0];
                            Files.RaiseListChangedEvents = false;
                        }
                    }
                }

                State = CancelScan ? SourceState.Unknown : SourceState.Scanned;                
            }
            catch (Exception exception)
            {
                Tracer.TraceException(exception, 859);
                State = SourceState.Failed;
            }
            finally
            {
                if (Tracer.Switch.Level.HasFlag(SourceLevels.Information))
                {
                    var builder = new StringBuilder();
                    builder.Append("Files by type: ");
                    var types = counters.OrderBy(v => v.Key).Select(kvp => $"[{kvp.Key[1..]}]={kvp.Value:#,##0}");
                    builder.Append(string.Join(", ", types));

                    Tracer.TraceInformation(builder.ToString());
                }

                CompleteScan();

                Files.RaiseListChangedEvents = true;
                ScanThread = null;
            }
        }

        abstract protected bool Connect();
        abstract protected void Disconnect();
        
        public bool InitScan()
        {
            Tracer.StartOperation("scan");
            Tracer.TraceStart(Description);

            if (!Connect()) return false;

            using var stream = OpenTokenRead();
            if (stream == null) Tracer.TraceVerbose($"no token found {ImportToken.FileName}");
            else
            {
                try
                {
                    Tracer.TraceVerbose($"found token {ImportToken.FileName}");
                    Token = ImportToken.Parse(stream);
                }
                catch (Exception exception)
                {
                    Tracer.TraceException(exception, 1102);
                }
            }

            LastScan = DateTime.Now;
            return true;
        }

        public virtual void CompleteScan()
        {
            Tracer.TraceInformation($"found {Files.Count:#,##0} files");
            Tracer.TraceVerbose($"state={State} / canceled={CancelScan}");

            Disconnect();

            Tracer.TraceStop(Description);
            Tracer.StopOperation();
        }

        [Browsable(false)]
        public ImportToken? Token { get; set; }

        [Description("Time of last import.")]
        [Category(CategoryState)]
        public DateTime? LastImport => Token?.LastImport;

        protected abstract Stream? OpenTokenRead();
        protected abstract Stream? OpenTokenWrite();

        public abstract IEnumerable<ImageFileBase> EnumerateFiles();

        #region Imported
        private const int ImportedDefault = 0;
        private int imported = ImportedDefault;
        [Browsable(false), DefaultValue(ImportedDefault)]
        public int Imported
        {
            get => imported;
            set => SetField(ref imported, value);
        }
        #endregion
        #region Skipped
        private const int SkippedDefault = 0;
        private int skipped = SkippedDefault;
        [Browsable(false), DefaultValue(SkippedDefault)]
        public int Skipped
        {
            get => skipped;
            set => SetField(ref skipped, value);
        }
        #endregion
        #region Failed
        private const int FailedDefault = 0;
        private int failed = FailedDefault;        

        [Browsable(false), DefaultValue(FailedDefault)]
        public int Failed
        {
            get => failed;
            set => SetField(ref failed, value);
        }
        #endregion

        public virtual void InitImport()
        {
            Imported = Skipped = Failed = 0;

            Connect();
        }

        public virtual void CompleteImport()
        {
            try
            {
                Tracer.TraceInformation($"create token {ImportToken.FileName}");

                var token = new ImportToken { LastImport = LastScan };

                using var stream = OpenTokenWrite();
                if (stream != null)
                {
                    try
                    {
                        token.Serialize(stream);
                    }
                    catch (Exception tokenException)
                    {
                        Tracer.TraceException(tokenException, 8597);
                    }
                    Token = token;
                }
            }
            catch (Exception execption)
            {
                Tracer.TraceException(execption, 8596);
            }
            finally
            {
                Disconnect();
            }
        }        

        public void Import(ImageFileBase file, Profile profile)
        {
            Tracer.StartOperation(file.Name);
            Tracer.TraceInformation($"from {file.FullName}");

            try
            {                
                var fileType = profile.GetFileType(file);

                if (fileType.Parameters.Any(p => !file.MetaDictionary.Contains(p)))
                {
                    file.GetMetadata();
                }

                var target = fileType.GetTarget(file);

                if (File.Exists(target) && !profile.Overwrite)
                {
                    Skipped++;
                    Tracer.TraceInformation($"  skip {target}");
                }
                else
                {
                    var folder = Path.GetDirectoryName(target);
                    if (folder != null && !Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    file.Copy(target);
                    Imported++;
                    Tracer.TraceInformation($"   --> {target}");
                }
            }
            catch (Exception exception)
            {
                Failed++;
                Tracer.TraceException(exception, 125);
            }
            finally
            {                
                Tracer.StopOperation();
            }
        }
    }

    public enum SourceState
    {
        Unknown,
        Scanning,
        Scanned,
        Failed
    }
}
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace ImageImport.Sources
{
    internal abstract class ImageSource : INotifyPropertyChanged
    {
        [Browsable(false)]
        public virtual string Description => "Dummy";

        [Description("Include subfolders"), DefaultValue(true)]
        public bool Recursive
        {
            get => recursive;
            set
            {
                if (recursive == value) return;
                recursive = value;

                ResetScan();
            }
        }

        protected void ResetScan()
        {
            State = SourceState.Unknown;
            CancelScan = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public abstract Icon GetIcon();

        #region LastScan
        private DateTime dateTime = DateTime.MinValue;
        [Browsable(false)]
        public DateTime LastScan
        {
            get => dateTime;
            set
            {
                if (dateTime == value) return;
                dateTime = value;
                OnPropertyChanged();
            }
        }
        #endregion

        [Browsable(false)]
        public BindingList<ImageFileBase> Files { get; } = new BindingList<ImageFileBase>();

        [Description("State of source")]
        #region State
        private const SourceState StateDefault = SourceState.Unknown;
        private SourceState sourceState = StateDefault;
        [DefaultValue(StateDefault)]
        public SourceState State
        {
            get => sourceState;
            private set
            {
                if (sourceState == value) return;
                sourceState = value;
                OnPropertyChanged();
            }
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
                InitScan();

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

        abstract protected void Connect();
        abstract protected void Disconnect();
        
        public void InitScan()
        {
            Tracer.StartOperation("scan");
            Tracer.TraceStart(Description);

            Connect();

            var tokenName = ImportToken.FileName;

            using var stream = OpenToken(tokenName);
            if (stream == null) Tracer.TraceVerbose($"no token found {tokenName}");
            else
            {
                try
                {
                    Tracer.TraceVerbose($"found token {tokenName}");
                    Token = ImportToken.Parse(stream);
                }
                catch (Exception exception)
                {
                    Tracer.TraceException(exception, 1102);
                }
            }

            LastScan = DateTime.Now;
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
        public DateTime? LastImport => Token?.LastImport;

        protected abstract Stream? OpenToken(string fileName);
        protected abstract void SaveToken(Stream stream, string fileName);

        public abstract IEnumerable<ImageFileBase> EnumerateFiles();

        #region Imported
        private const int ImportedDefault = 0;
        private int imported = ImportedDefault;
        [Browsable(false), DefaultValue(ImportedDefault)]
        public int Imported
        {
            get => imported;
            set
            {
                if (imported == value) return;
                imported = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Skipped
        private const int SkippedDefault = 0;
        private int skipped = SkippedDefault;
        [Browsable(false), DefaultValue(SkippedDefault)]
        public int Skipped
        {
            get => skipped;
            set
            {
                if (skipped == value) return;
                skipped = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Failed
        private const int FailedDefault = 0;
        private int failed = FailedDefault;
        private bool recursive = true;

        [Browsable(false), DefaultValue(FailedDefault)]
        public int Failed
        {
            get => failed;
            set
            {
                if (failed == value) return;
                failed = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public virtual void InitImport()
        {
            Imported = Skipped = Failed = 0;

            Connect();
        }

        public virtual void CompleteImport()
        {
            var tokenName = ImportToken.FileName;

            try
            {
                Tracer.TraceInformation($"create token {tokenName}");

                var token = new ImportToken { LastImport = LastScan };
                using var stream = token.Serialize();
                SaveToken(stream, tokenName);

                Token = token;
            }
            catch (Exception excption)
            {
                Tracer.TraceException(excption, 8596);
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

        abstract protected void Copy(ImageFileBase file, string target);
    }

    public enum SourceState
    {
        Unknown,
        Scanning,
        Scanned,
        Failed
    }
}
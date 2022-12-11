using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Sharpen;
using Toolbox.Collection.Generics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Directory = MetadataExtractor.Directory;

namespace ImageImport.Sources
{
    internal abstract class ImageSource : INotifyPropertyChanged
    {
        [Browsable(false)]
        public virtual string Description => $"{GetType().Name}.{GetHashCode()}";

        [Description("Include subfolders"), DefaultValue(true)]
        public bool Recursive { get; set; } = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public abstract Icon GetIcon();

        public virtual void InitScan()
        {
        }

        public virtual void CompleteScan()
        {
        }

        public abstract IEnumerable<ImageFile> EnumerateFiles();

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
        }

        public virtual void CompleteImport()
        {
        }

        public abstract Stream GetStream(ImageFile file);

        private const string MetadataSeparator = "/";

        static Dictionary<string, Func<Tag, object, object>> Converters { get; } = new Dictionary<string, Func<Tag, object, object>>()
        {
            { $"Exif IFD0{MetadataSeparator}DateTime", ConvertDateTime },
            { $"Exif SubIFD{MetadataSeparator}DateTime Original", ConvertDateTime },
            { $"Exif SubIFD.Date{MetadataSeparator}Time Digitized", ConvertDateTime },
            { $"GPS{MetadataSeparator}GPS Date Stamp", ConvertDateTime },
            { $"GPS{MetadataSeparator}GPS Time-Stamp", ConvertGpsTimeStamp }
        };

        private static string[] DateTimeFormats { get; } =
        {
            "yyyy:MM:dd HH:mm:ss",
            "yyyy:MM:dd",
        };
        private static object ConvertDateTime(Tag tag, object value)
        {
            if (value is StringValue text)
            {
                if (DateTime.TryParseExact(text.ToString(), DateTimeFormats, null, System.Globalization.DateTimeStyles.None, out var parsed))
                    return parsed;
            }
            return value;
        }

        private static object ConvertGpsTimeStamp(Tag tag, object value)
        {
            return tag.Description ?? value;
        }

        public void Import(ImageFile file, Profile profile, Action<string> protocol, Action<string> protocolVerbose)
        {
            var fileType = profile.GetFileType(file);

            if (fileType.Parameters.Any(p => !file.Parameters.ContainsKey(p)))
            {
                GetMetadata(file, protocol, protocolVerbose);
            }

            try
            {
                var target = fileType.GetTarget(file);

                if (File.Exists(target) && !profile.Overwrite)
                {
                    Skipped++;
                    protocol($"  skip {target}");
                }
                else
                {
                    protocol($"   --> {target}");

                    var folder = Path.GetDirectoryName(target);
                    if (folder != null && !System.IO.Directory.Exists(folder))
                    {
                        System.IO.Directory.CreateDirectory(folder);
                    }

                    Copy(file, target);
                    Imported++;
                }
            }
            catch (Exception exception)
            {
                Failed++;
                protocol($"copy failed: {exception.Message}");
            }
        }

        abstract protected void Copy(ImageFile file, string target);

        private void GetMetadata(ImageFile file, Action<string> protocol, Action<string> protocolVerbose)
        {
            try
            {
                using var stream = GetStream(file);
                var metadatas = ImageMetadataReader.ReadMetadata(stream).ToHashSet();
                var stack = new Stack<Directory>();

                while (metadatas.Count > 0)
                {
                    metadatas.Where(m => m.Parent == null || !metadatas.Contains(m.Parent))
                        .ToArray()
                        .ForEach(m => 
                        {
                            stack.Push(m);
                            metadatas.Remove(m);
                        });
                }                

                foreach (var metadata in stack)
                {
                    foreach (var tag in metadata.Tags.Where(t => t.HasName))
                    {
                        var value = metadata.GetObject(tag.Type);

                        var name = tag.Name.Replace(MetadataSeparator, "");
                        var basename = metadata.Name + MetadataSeparator + name;
                        var fullname = basename;

                        for (var parent = metadata.Parent; parent != null; parent = parent.Parent)
                        {
                            fullname = parent.Name + MetadataSeparator + fullname;
                        }

                        if (value != null)
                        {
                            if (Converters.TryGetValue(basename, out var converter))
                            {
                                value = converter(tag, value);
                            }

                            var description = value.ToString() != tag.Description ? $" ({tag.Description})" : "";
                            protocolVerbose($"  {fullname} = {value.GetType().Name} [{value}]{description}");

                            file.Parameters[fullname] = value;
                            file.Parameters[name] = value;
                        }
                        else
                        {
                            protocolVerbose($"  {fullname} = <null> ignored");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                protocol($"failed to read metadata: {exception.Message}");
            }
        }
    }
}

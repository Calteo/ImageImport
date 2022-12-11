using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ImageImport.Editors;
using ImageImport.Sources;
using Toolbox;
using Toolbox.Collection.Generics;

namespace ImageImport
{
    internal class ProfileFileType : INotifyPropertyChanged
    {
        #region Extensions
        private const string ExtensionsDefaultValue = "";
        private string extensions = ExtensionsDefaultValue;
        [Description("File extensions to import"), DefaultValue(ExtensionsDefaultValue)]
        public string Extensions
        {
            get => extensions;
            set
            {
                if (extensions == value) return;
                var parts = (value ?? "").Split(",;. ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                extensions = string.Join(" ", parts);

                MatchExtensions.Clear();
                parts.ForEach(p => MatchExtensions.Add("."+p));

                OnPropertyChanged();
            }
        }
        private HashSet<string> MatchExtensions { get; } = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

        public bool Match(string extension) => MatchExtensions.Contains(extension);

        #endregion
        #region Folder
        private const string FolderDefaultValue = "";
        private string folder = FolderDefaultValue;
        [Editor(typeof(FolderEditor), typeof(UITypeEditor))]
        [Description("Folder to import to"), DefaultValue(FolderDefaultValue)]
        public string Folder
        {
            get => folder;
            set
            {
                if (folder == value) return;
                folder = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Pattern
        private const string PatternDefault = default;
        private string pattern = PatternDefault;
        [Description("Pattern to apply to import file"), DefaultValue(PatternDefault)]
        public string Pattern
        {
            get => pattern;
            set
            {
                if (pattern == value) return;
                pattern = value;

                Parameters.Clear();
                if (pattern.NotEmpty())
                {
                    ParameterExpression.Matches(pattern)
                        .ForEach(m => Parameters.Add(m.Groups["name"].Value));
                }

                OnPropertyChanged();
            }
        }
        internal HashSet<string> Parameters { get; } = new HashSet<string>();
        #endregion
        private Regex ParameterExpression { get; } = new Regex(@"(?<parameter>\$\((?<name>[ a-zA-Z/]+)(:(?<format>[^)]+))?)\)", RegexOptions.Compiled|RegexOptions.Singleline);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public override string ToString()
        {
            return $"[{extensions}]->";
        }

        internal string GetTarget(ImageFile file)
        {
            var target = ParameterExpression.Replace(Pattern, m => LookupParameter(file, m));
            if (Folder.NotEmpty())
                target = Path.Combine(Folder, target);

            return target;
        }

        private static string LookupParameter(ImageFile file, Match match)
        {
            var name = match.Groups["name"].Value;

            if (file.Parameters.TryGetValue(name, out var parameter))
            {
                if (match.Groups["format"].Success)
                {
                    var format = match.Groups["format"].Value;
                    return string.Format("{0:"+format+"}", parameter);
                }
                return parameter.ToString() ?? "(null)";
            }

            return match.Value;
        }
    }
}

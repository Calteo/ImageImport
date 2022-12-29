using System.Reflection;

namespace ImageImport.Icons
{
    internal static class IconStore
    {
        private static Dictionary<string, Dictionary<int, Icon>> Loaded { get; } = new Dictionary<string, Dictionary<int, Icon>>();

        public static Icon GetIcon(string name, int size)
        {
            if (!Loaded.TryGetValue(name, out var icons))
            {
                Loaded[name] = icons = new Dictionary<int, Icon>();
            }

            if (!icons.TryGetValue(size, out var icon))
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(IconStore), name + ".ico");
                if (stream == null)
                    throw new NullReferenceException($"missing icon for {name}.");

                icons[size] = icon = new Icon(stream, size, size);
            }

            return icon;
        }
    }
}

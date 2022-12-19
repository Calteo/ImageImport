using System.IO;
using MetadataExtractor;
using Toolbox.Collection.Generics;
using Directory = MetadataExtractor.Directory;

namespace ImageImport.Sources
{
    internal class MetaDictionary
    {
        public Dictionary<string, MetaValue> Values { get; } = new Dictionary<string, MetaValue>(StringComparer.InvariantCultureIgnoreCase);

        public void Add(string name, object value, string description)
        {
            var metaValue = new MetaValue(name, value, value, description);
            Values[name] = metaValue;
        }

        public bool Contains(string name) => Values.ContainsKey(name);
        public void ParseFrom(Stream stream)
        {
            try
            {
                Tracer.StartOperation("metadata");

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
                            var basevalue = value;

                            if (Converters.TryGetValue(basename, out var converter))
                            {
                                value = converter(tag, value);
                            }

                            Values[name] = new MetaValue(name, value, basevalue, tag.Description ?? "");
                            Values[fullname] = new MetaValue(fullname, value, basevalue, tag.Description ?? "");
                        }
                        else
                        {
                            // AppendProtocol($"  {fullname} = <null> ignored");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Tracer.TraceException(exception, 95);
            }
            finally
            {
                Tracer.StopOperation();
            }
        }

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
    }
}

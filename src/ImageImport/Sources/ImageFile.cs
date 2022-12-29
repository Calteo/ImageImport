namespace ImageImport.Sources
{
    internal abstract class ImageFile<T> : ImageFileBase where T : ImageSource
    {
        public ImageFile(T source, string fullName, DateTime created) : base(source, fullName, created)
        {
            Source = source;
        }

        public new T Source { get; }
    }
}

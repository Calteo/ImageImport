namespace ImageImport
{
    internal class ImportParameters
    {
        public bool Overwrite { get; set; }
        public bool OnlyNewFiles { get; set; }
        public CheckState DeleteAfterImport { get; set; }
    }
}

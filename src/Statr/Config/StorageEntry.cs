namespace Statr.Config
{
    public class StorageEntry
    {
        public StorageEntry() { }

        public StorageEntry(string name, string pattern, params Retention[] retentions)
        {
            Name = name;
            Pattern = pattern;
            Retentions = retentions;
        }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public Retention[] Retentions { get; set; }
    }
}
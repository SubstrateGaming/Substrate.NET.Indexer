using System.Text.Json.Serialization;

namespace Substrate.NET.Indexer.Model
{
    public class DbBlock
    {
        public uint BlockNumber { get; set; }
        public string BlockHash { get; set; }

        [JsonIgnore]
        public ICollection<DbEvent> Events { get; set; }
    }
}
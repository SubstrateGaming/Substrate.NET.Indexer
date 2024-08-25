
using Substrate.Bajun.NET.NetApiExt.Generated.Model.frame_system;
using System.Text.Json.Serialization;

namespace Substrate.NET.Indexer.Model
{
    public class DbEvent
    {
        public string EventId { get; set; }

        public uint BlockNumber { get; set; }

        public short Index { get; set; }

        public Phase Phase { get; set; }

        public uint? PhaseValue { get; set; }

        public string? ModuleName { get; set; }

        public string? EventName { get; set; }
        public string? ParameterType { get; set; }
        public byte[]? ParameterBytes { get; set; }

        [JsonIgnore]
        public DbBlock Block { get; set; }

    }
}
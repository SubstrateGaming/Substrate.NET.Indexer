using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.account;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped PlayStats
    /// </summary>
    public class PlayStatsSharp
    {
        /// <summary>
        /// PlayStatsSharp constructor
        /// </summary>
        /// <param name="playStats"></param>
        public PlayStatsSharp(PlayStats playStats)
        {
            First = playStats.First.Value;
            Last = playStats.Last.Value;
        }

        /// <summary>
        /// First
        /// </summary>
        public uint First { get; private set; }

        /// <summary>
        /// Last
        /// </summary>
        public uint Last { get; }
    }
}
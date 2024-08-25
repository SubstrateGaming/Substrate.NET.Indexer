using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.account;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Stats
    /// </summary>
    public class StatsSharp
    {
        /// <summary>
        /// Wrapped Stats constructor
        /// </summary>
        /// <param name="stats"></param>
        public StatsSharp(Stats stats)
        {
            Mint = new PlayStatsSharp(stats.Mint);
            Forge = new PlayStatsSharp(stats.Forge);
        }

        /// <summary>
        /// Mint
        /// </summary>
        public PlayStatsSharp Mint { get; private set; }

        /// <summary>
        /// Forge
        /// </summary>
        public PlayStatsSharp Forge { get; private set; }
    }
}
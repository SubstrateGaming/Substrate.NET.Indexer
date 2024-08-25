using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.account;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped PlayerSeasonConfig
    /// </summary>
    public class PlayerSeasonConfigsSharp
    {
        /// <summary>
        /// Wrapped PlayerSeasonConfig constructor
        /// </summary>
        /// <param name="playerSeasonConfig"></param>
        public PlayerSeasonConfigsSharp(PlayerSeasonConfig playerSeasonConfig)
        {
            StorageTier = playerSeasonConfig.StorageTier.Value;
            StatsSharp = new StatsSharp(playerSeasonConfig.Stats);
            LocksSharp = new LocksSharp(playerSeasonConfig.Locks);
        }

        /// <summary>
        /// Storage Tier
        /// </summary>
        public StorageTier StorageTier { get; }

        /// <summary>
        /// Stats
        /// </summary>
        public StatsSharp StatsSharp { get; }

        /// <summary>
        /// Locks
        /// </summary>
        public LocksSharp LocksSharp { get; }
    }
}
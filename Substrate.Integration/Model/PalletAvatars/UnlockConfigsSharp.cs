using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using System.Linq;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Unlock Configs
    /// </summary>
    public class UnlockConfigsSharp
    {
        /// <summary>
        /// Wrapped Season Unlock Configs constructor
        /// </summary>
        /// <param name="unlockConfigs"></param>
        public UnlockConfigsSharp(UnlockConfigs unlockConfigs)
        {
            SetPriceUnlock = unlockConfigs.SetPriceUnlock.Value.Value.Value.Select(p => p.Value).ToArray();
            AvatarTransferUnlock = unlockConfigs.AvatarTransferUnlock.Value.Value.Value.Select(p => p.Value).ToArray();
            AffiliateUnlock = unlockConfigs.AffiliateUnlock.Value.Value.Value.Select(p => p.Value).ToArray();
        }

        /// <summary>
        /// Set Price Unlock
        /// </summary>
        public byte[] SetPriceUnlock { get; }

        /// <summary>
        /// Avatar Transfer Unlock
        /// </summary>
        public byte[] AvatarTransferUnlock { get; }

        /// <summary>
        /// Affiliate Unlock
        /// </summary>
        public byte[] AffiliateUnlock { get; }
    }
}
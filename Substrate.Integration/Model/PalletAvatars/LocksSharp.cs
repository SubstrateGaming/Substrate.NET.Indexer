using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.account;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Locks
    /// </summary>
    public class LocksSharp
    {
        /// <summary>
        /// LocksSharp constructor
        /// </summary>
        /// <param name="locks"></param>
        public LocksSharp(Locks locks)
        {
            AvatarTransfer = locks.AvatarTransfer;
            SetPrice = locks.SetPrice;
            Affiliate = locks.Affiliate;
        }

        /// <summary>
        /// Avatar Transfer
        /// </summary>
        public Bool AvatarTransfer { get; }

        /// <summary>
        /// Set Price
        /// </summary>
        public Bool SetPrice { get; }

        /// <summary>
        /// Affiliate
        /// </summary>
        public Bool Affiliate { get; }
    }
}
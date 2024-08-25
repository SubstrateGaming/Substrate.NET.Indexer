using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Season Status
    /// </summary>
    public class SeasonStatusSharp
    {
        /// <summary>
        /// Wrapped Season Status constructor
        /// </summary>
        /// <param name="seasonStatus"></param>
        public SeasonStatusSharp(SeasonStatus seasonStatus)
        {
            SeasonId = seasonStatus.SeasonId.Value;
            Early = seasonStatus.Early.Value;
            Active = seasonStatus.Active.Value;
            EarlyEnded = seasonStatus.EarlyEnded.Value;
            MaxTierAvatars = seasonStatus.MaxTierAvatars.Value;
        }

        /// <summary>
        /// Season Id
        /// </summary>
        public ushort SeasonId { get; private set; }

        /// <summary>
        /// Early
        /// </summary>
        public bool Early { get; private set; }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// Early Ended
        /// </summary>
        public bool EarlyEnded { get; private set; }

        /// <summary>
        /// Max Tier Avatars
        /// </summary>
        public uint MaxTierAvatars { get; private set; }
    }
}
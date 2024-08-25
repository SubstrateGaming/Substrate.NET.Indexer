using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar;
using Substrate.Bajun.NET.NetApiExt.Helper;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Avatar
    /// </summary>
    public class AvatarSharp
    {
        /// <summary>
        /// Wrapped Avatar constructor
        /// </summary>
        /// <param name="avatar"></param>
        public AvatarSharp(Avatar avatar)
        {
            SeasonId = avatar.SeasonId.Value;
            DnaEncoding = avatar.Encoding.Value;
            Dna = avatar.Dna.Value.ToBytes();
            Souls = avatar.Souls.Value;
            MintedAt = avatar.MintedAt.Value;
        }

        /// <summary>
        /// Season Id
        /// </summary>
        public ushort SeasonId { get; }

        /// <summary>
        /// DNA Encoding
        /// </summary>
        public DnaEncoding DnaEncoding { get; }

        /// <summary>
        /// DNA byte array
        /// </summary>
        public byte[] Dna { get; }

        /// <summary>
        /// Souls
        /// </summary>
        public uint Souls { get; }

        /// <summary>
        /// Minted At
        /// </summary>
        public uint MintedAt { get; }
    }
}
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Mint Config C# Wrapper
    /// </summary>
    public class MintConfigSharp
    {
        /// <summary>
        /// Mint Config Constructor
        /// </summary>
        /// <param name="mint"></param>
        public MintConfigSharp(MintConfig mint)
        {
            Open = mint.Open.Value;
            Cooldown = mint.Cooldown.Value;
            FreeMintFeeMultiplier = mint.FreeMintFeeMultiplier.Value;
        }

        /// <summary>
        /// Mint Config Constructor
        /// </summary>
        /// <param name="open"></param>
        /// <param name="cooldown"></param>
        /// <param name="freeMintFeeMultiplier"></param>
        public MintConfigSharp(bool open, uint cooldown, ushort freeMintFeeMultiplier)
        {
            Open = open;
            Cooldown = cooldown;
            FreeMintFeeMultiplier = freeMintFeeMultiplier;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public MintConfig ToSubstrate()
        {
            return new MintConfig
            {
                Open = new Bool(Open),
                Cooldown = new U32(Cooldown),
                FreeMintFeeMultiplier = new U16(FreeMintFeeMultiplier)
            };
        }

        /// <summary>
        /// Open
        /// </summary>
        public bool Open { get; private set; }

        /// <summary>
        /// Cooldown
        /// </summary>
        public uint Cooldown { get; private set; }

        /// <summary>
        /// Free Mint Fee Multiplier
        /// </summary>
        public ushort FreeMintFeeMultiplier { get; private set; }
    }
}
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.fee;
using Substrate.NetApi.Model.Types.Primitive;
using System.Numerics;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapper Mint Fees
    /// </summary>
    public class MintFeesSharp
    {
        /// <summary>
        /// Wrapper Mint Fees constructor
        /// </summary>
        /// <param name="mint"></param>
        public MintFeesSharp(MintFees mint)
        {
            One = mint.One.Value;
            Three = mint.Three.Value;
            Six = mint.Six.Value;
        }

        /// <summary>
        /// Wrapper Mint Fees constructor
        /// </summary>
        /// <param name="one"></param>
        /// <param name="three"></param>
        /// <param name="six"></param>
        public MintFeesSharp(BigInteger one, BigInteger three, BigInteger six)
        {
            One = one;
            Three = three;
            Six = six;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public MintFees ToSubstrate()
        {
            return new MintFees
            {
                One = new U128(One),
                Three = new U128(Three),
                Six = new U128(Six)
            };
        }

        /// <summary>
        /// One
        /// </summary>
        public BigInteger One { get; private set; }

        /// <summary>
        /// Three
        /// </summary>
        public BigInteger Three { get; private set; }

        /// <summary>
        /// Six
        /// </summary>
        public BigInteger Six { get; private set; }
    }
}
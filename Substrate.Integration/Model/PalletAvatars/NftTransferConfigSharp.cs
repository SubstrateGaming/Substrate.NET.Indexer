using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Nft Transfer Config C# Wrapper
    /// </summary>
    public class NftTransferConfigSharp
    {
        /// <summary>
        /// Nft Transfer Config Constructor
        /// </summary>
        /// <param name="nftTransfer"></param>
        public NftTransferConfigSharp(NftTransferConfig nftTransfer)
        {
            Open = nftTransfer.Open.Value;
        }

        /// <summary>
        /// Nft Transfer Config Constructor
        /// </summary>
        /// <param name="open"></param>
        public NftTransferConfigSharp(bool open)
        {
            Open = open;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public NftTransferConfig ToSubstrate()
        {
            return new NftTransferConfig
            {
                Open = new Bool(Open)
            };
        }

        /// <summary>
        /// Open
        /// </summary>
        public bool Open { get; private set; }
    }
}
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Global Config C# Wrapper
    /// </summary>
    public class GlobalConfigSharp
    {
        /// <summary>
        /// Global Config Constructor
        /// </summary>
        /// <param name="globalConfig"></param>
        public GlobalConfigSharp(GlobalConfig globalConfig)
        {
            Mint = new MintConfigSharp(globalConfig.Mint);
            Forge = new ForgeConfigSharp(globalConfig.Forge);
            AvatarTransfer = new AvatarTransferConfigSharp(globalConfig.AvatarTransfer);
            FreemintTransfer = new FreemintTransferConfigSharp(globalConfig.FreemintTransfer);
            Trade = new TradeConfigSharp(globalConfig.Trade);
            NftTransfer = new NftTransferConfigSharp(globalConfig.NftTransfer);
            AffiliateConfig = new AffiliateConfigSharp(globalConfig.AffiliateConfig);
        }

        /// <summary>
        /// Global Config Constructor
        /// </summary>
        /// <param name="mint"></param>
        /// <param name="forge"></param>
        /// <param name="avatarTransfer"></param>
        /// <param name="freemintTransfer"></param>
        /// <param name="trade"></param>
        /// <param name="nftTransfer"></param>
        /// <param name="affiliateConfig"></param>
        public GlobalConfigSharp(MintConfigSharp mint, ForgeConfigSharp forge, AvatarTransferConfigSharp avatarTransfer, FreemintTransferConfigSharp freemintTransfer, TradeConfigSharp trade, NftTransferConfigSharp nftTransfer, AffiliateConfigSharp affiliateConfig)
        {
            Mint = mint;
            Forge = forge;
            AvatarTransfer = avatarTransfer;
            FreemintTransfer = freemintTransfer;
            Trade = trade;
            NftTransfer = nftTransfer;
            AffiliateConfig = affiliateConfig;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public GlobalConfig ToSubstrate()
        {
            return new GlobalConfig
            {
                Mint = Mint.ToSubstrate(),
                Forge = Forge.ToSubstrate(),
                AvatarTransfer = AvatarTransfer.ToSubstrate(),
                FreemintTransfer = FreemintTransfer.ToSubstrate(),
                Trade = Trade.ToSubstrate(),
                NftTransfer = NftTransfer.ToSubstrate(),
                AffiliateConfig = AffiliateConfig.ToSubstrate()
            };
        }

        /// <summary>
        /// Mint
        /// </summary>
        public MintConfigSharp Mint { get; }

        /// <summary>
        /// Forge
        /// </summary>
        public ForgeConfigSharp Forge { get; }

        /// <summary>
        /// Avatar Transfer
        /// </summary>
        public AvatarTransferConfigSharp AvatarTransfer { get; }

        /// <summary>
        /// Freemint Transfer
        /// </summary>
        public FreemintTransferConfigSharp FreemintTransfer { get; }

        /// <summary>
        /// Trade
        /// </summary>
        public TradeConfigSharp Trade { get; }

        /// <summary>
        /// NftTransfer
        /// </summary>
        public NftTransferConfigSharp NftTransfer { get; }

        /// <summary>
        /// Affiliate Config
        /// </summary>
        public AffiliateConfigSharp AffiliateConfig { get; }
    }
}
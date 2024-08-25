using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;
using System.Numerics;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Affiliate Config C# Wrapper
    /// </summary>
    public class AffiliateConfigSharp
    {
        /// <summary>
        /// Affiliate Config Constructor
        /// </summary>
        /// <param name="affiliateConfig"></param>
        public AffiliateConfigSharp(AffiliateConfig affiliateConfig)
        {
            Mode = affiliateConfig.Mode.Value;
            EnabledInMint = affiliateConfig.EnabledInMint.Value;
            EnabledInBuy = affiliateConfig.EnabledInBuy.Value;
            EnabledInUpgrade = affiliateConfig.EnabledInUpgrade.Value;
            AffiliatorEnableFee = affiliateConfig.AffiliatorEnableFee.Value;
        }

        /// <summary>
        /// Affiliate Config Constructor
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="enabledInMint"></param>
        /// <param name="enabledInBuy"></param>
        /// <param name="enabledInUpgrade"></param>
        /// <param name="affiliatorEnableFee"></param>
        public AffiliateConfigSharp(AffiliateMode mode, bool enabledInMint, bool enabledInBuy, bool enabledInUpgrade, BigInteger affiliatorEnableFee)
        {
            Mode = mode;
            EnabledInMint = enabledInMint;
            EnabledInBuy = enabledInBuy;
            EnabledInUpgrade = enabledInUpgrade;
            AffiliatorEnableFee = affiliatorEnableFee;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public AffiliateConfig ToSubstrate()
        {
            var mode = new EnumAffiliateMode();
            mode.Create(Mode);

            return new AffiliateConfig
            {
                Mode = mode,
                EnabledInMint = new Bool(EnabledInMint),
                EnabledInBuy = new Bool(EnabledInBuy),
                EnabledInUpgrade = new Bool(EnabledInUpgrade),
                AffiliatorEnableFee = new U128(AffiliatorEnableFee)
            };
        }

        /// <summary>
        /// Affiliate Mode
        /// </summary>
        public AffiliateMode Mode { get; }

        /// <summary>
        /// Enabled In Mint
        /// </summary>
        public bool EnabledInMint { get; }

        /// <summary>
        /// Enabled In Buy
        /// </summary>
        public bool EnabledInBuy { get; }

        /// <summary>
        /// Enabled In Upgrade
        /// </summary>
        public bool EnabledInUpgrade { get; }

        /// <summary>
        /// Affiliator Enable Fee
        /// </summary>
        public BigInteger AffiliatorEnableFee { get; }
    }
}
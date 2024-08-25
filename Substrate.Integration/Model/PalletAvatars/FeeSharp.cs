using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.fee;
using Substrate.NetApi.Model.Types.Primitive;
using System.Numerics;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapper Fee Sharp
    /// </summary>
    public class FeeSharp
    {
        /// <summary>
        /// Wrapper Fee Sharp constructor
        /// </summary>
        /// <param name="fee"></param>
        public FeeSharp(Fee fee)
        {
            MintFees = new MintFeesSharp(fee.Mint);
            TransferAvatar = fee.TransferAvatar.Value;
            BuyMinimum = fee.BuyMinimum.Value;
            BuyPercent = fee.BuyPercent.Value;
            UpgradeStorage = fee.UpgradeStorage.Value;
            PrepareAvatar = fee.PrepareAvatar.Value;
            SetPriceUnlock = fee.SetPriceUnlock.Value;
            AvatarTransferUnlock = fee.AvatarTransferUnlock.Value;
        }

        /// <summary>
        /// Wrapper Fee Sharp constructor
        /// </summary>
        /// <param name="mintFees"></param>
        /// <param name="transferAvatar"></param>
        /// <param name="buyMinimum"></param>
        /// <param name="buyPercent"></param>
        /// <param name="upgradeStorage"></param>
        /// <param name="prepareAvatar"></param>
        /// <param name="setPriceUnlock"></param>
        /// <param name="avatarTransferUnlock"></param>
        public FeeSharp(MintFeesSharp mintFees, BigInteger transferAvatar, BigInteger buyMinimum, byte buyPercent, BigInteger upgradeStorage, BigInteger prepareAvatar, BigInteger setPriceUnlock, BigInteger avatarTransferUnlock)
        {
            MintFees = mintFees;
            TransferAvatar = transferAvatar;
            BuyMinimum = buyMinimum;
            BuyPercent = buyPercent;
            UpgradeStorage = upgradeStorage;
            PrepareAvatar = prepareAvatar;
            SetPriceUnlock = setPriceUnlock;
            AvatarTransferUnlock = avatarTransferUnlock;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public Fee ToSubstrate()
        {
            var fee = new Fee
            {
                Mint = new MintFees(),
                TransferAvatar = new U128(TransferAvatar),
                BuyMinimum = new U128(BuyMinimum),
                BuyPercent = new U8(BuyPercent),
                UpgradeStorage = new U128(UpgradeStorage),
                PrepareAvatar = new U128(PrepareAvatar),
                SetPriceUnlock = new U128(SetPriceUnlock),
                AvatarTransferUnlock = new U128(AvatarTransferUnlock),
            };

            fee.Mint.One = new U128(MintFees.One);
            fee.Mint.Three = new U128(MintFees.Three);
            fee.Mint.Six = new U128(MintFees.Six);

            return fee;
        }

        /// <summary>
        /// Mint Fees
        /// </summary>
        public MintFeesSharp MintFees { get; }

        /// <summary>
        /// Transfer Avatar
        /// </summary>
        public BigInteger TransferAvatar { get; }

        /// <summary>
        /// Buy Minimum
        /// </summary>
        public BigInteger BuyMinimum { get; }

        /// <summary>
        /// Buy Percent
        /// </summary>
        public byte BuyPercent { get; }

        /// <summary>
        /// Upgrade Storage
        /// </summary>
        public BigInteger UpgradeStorage { get; }

        /// <summary>
        /// Prepare Avatar
        /// </summary>
        public BigInteger PrepareAvatar { get; }

        /// <summary>
        /// Set Price Unlock
        /// </summary>
        public BigInteger SetPriceUnlock { get; }

        /// <summary>
        /// Avatar Transfer Unlock
        /// </summary>
        public BigInteger AvatarTransferUnlock { get; }
    }
}
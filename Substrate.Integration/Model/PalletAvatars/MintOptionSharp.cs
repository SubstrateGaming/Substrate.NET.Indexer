using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Mint Option C# Wrapper
    /// </summary>
    public class MintOptionSharp
    {
        /// <summary>
        /// Mint Option Constructor
        /// </summary>
        /// <param name="mintOption"></param>
        public MintOptionSharp(MintOption mintOption)
        {
            Payment = mintOption.Payment;
            PackType = mintOption.PackType;
            PackSize = mintOption.PackSize;
        }

        /// <summary>
        /// Mint Option Constructor
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="packType"></param>
        /// <param name="packSize"></param>
        public MintOptionSharp(MintPayment payment, PackType packType, MintPackSize packSize)
        {
            Payment = payment;
            PackType = packType;
            PackSize = packSize;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public MintOption ToSubstrate()
        {
            var mintOption = new MintOption
            {
                Payment = new EnumMintPayment(),
                PackType = new EnumPackType(),
                PackSize = new EnumMintPackSize(),
            };

            mintOption.Payment.Create(Payment);
            mintOption.PackType.Create(PackType);
            mintOption.PackSize.Create(PackSize);

            return mintOption;
        }

        /// <summary>
        /// Mint Payment
        /// </summary>
        public MintPayment Payment { get; private set; }

        /// <summary>
        /// Pack Type
        /// </summary>
        public PackType PackType { get; private set; }

        /// <summary>
        /// Mint Pack Size
        /// </summary>
        public MintPackSize PackSize { get; private set; }
    }
}
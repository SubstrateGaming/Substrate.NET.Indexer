using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Freemint Transfer Config C# Wrapper
    /// </summary>
    public class FreemintTransferConfigSharp
    {
        /// <summary>
        /// Freemint Transfer Config Constructor
        /// </summary>
        /// <param name="freemintTransfer"></param>
        public FreemintTransferConfigSharp(FreemintTransferConfig freemintTransfer)
        {
            Mode = freemintTransfer.Mode.Value;
            FreeMintTransferFee = freemintTransfer.FreeMintTransferFee.Value;
            MinFreeMintTransfer = freemintTransfer.MinFreeMintTransfer.Value;
        }

        /// <summary>
        /// Freemint Transfer Config Constructor
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="freeMintTransferFee"></param>
        /// <param name="minFreeMintTransfer"></param>
        public FreemintTransferConfigSharp(FreeMintTransferMode mode, ushort freeMintTransferFee, ushort minFreeMintTransfer)
        {
            Mode = mode;
            FreeMintTransferFee = freeMintTransferFee;
            MinFreeMintTransfer = minFreeMintTransfer;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public FreemintTransferConfig ToSubstrate()
        {
            var mode = new EnumFreeMintTransferMode();
            mode.Create(Mode);

            return new FreemintTransferConfig
            {
                Mode = mode,
                FreeMintTransferFee = new U16(FreeMintTransferFee),
                MinFreeMintTransfer = new U16(MinFreeMintTransfer)
            };
        }

        /// <summary>
        /// Mode
        /// </summary>
        public FreeMintTransferMode Mode { get; }

        /// <summary>
        /// Free Mint Transfer Fee
        /// </summary>
        public ushort FreeMintTransferFee { get; }

        /// <summary>
        /// Min Free Mint Transfer
        /// </summary>
        public ushort MinFreeMintTransfer { get; }
    }
}
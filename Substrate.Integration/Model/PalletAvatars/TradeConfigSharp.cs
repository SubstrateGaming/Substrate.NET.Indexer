using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Transfer Config C# Wrapper
    /// </summary>
    public class TradeConfigSharp
    {
        /// <summary>
        /// Transfer Config Constructor
        /// </summary>
        /// <param name="trade"></param>
        public TradeConfigSharp(TradeConfig trade)
        {
            Open = trade.Open.Value;
        }

        /// <summary>
        /// Transfer Config Constructor
        /// </summary>
        /// <param name="open"></param>
        public TradeConfigSharp(bool open)
        {
            Open = open;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public TradeConfig ToSubstrate()
        {
            return new TradeConfig
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
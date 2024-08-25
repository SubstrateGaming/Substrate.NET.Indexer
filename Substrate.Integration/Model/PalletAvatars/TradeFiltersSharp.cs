using Substrate.Bajun.NET.NetApiExt.Generated.Model.bounded_collections.bounded_vec;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System.Linq;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Season Trade Filters
    /// </summary>
    public class TradeFiltersSharp
    {
        /// <summary>
        /// Wrapped Season Trade Filters constructor
        /// </summary>
        /// <param name="tradeFilters"></param>
        public TradeFiltersSharp(TradeFilters tradeFilters)
        {
            Values = tradeFilters.Value.Value.Value.Select(p => p.Value).ToArray();
        }

        /// <summary>
        /// Wrapped Season Trade Filters constructor
        /// </summary>
        /// <param name="values"></param>
        public TradeFiltersSharp(uint[] values)
        {
            Values = values;
        }

        public TradeFilters ToSubstrate()
        {
            var tradeFilters = new TradeFilters
            {
                Value = new BoundedVecT9(),
            };

            tradeFilters.Value.Value = new BaseVec<U32>(Values.Select(p => new U32(p)).ToArray());
            return tradeFilters;
        }

        /// <summary>
        /// Season Trade Filter
        /// </summary>
        public uint[] Values { get; }
    }
}
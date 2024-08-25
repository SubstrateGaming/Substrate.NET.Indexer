using Substrate.Bajun.NET.NetApiExt.Generated.Model.bounded_collections.bounded_vec;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;
using Substrate.Bajun.NET.NetApiExt.Helper;
using Substrate.NetApi.Extensions;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System.Linq;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped SeasonMeta
    /// </summary>
    public class SeasonMetaSharp
    {
        /// <summary>
        /// Wrapped SeasonMeta constructor
        /// </summary>
        /// <param name="seasonMeta"></param>
        public SeasonMetaSharp(SeasonMeta seasonMeta)
        {
            Name = seasonMeta.Name.Value.ToText();
            Description = seasonMeta.Description.Value.ToText();
        }

        /// <summary>
        /// Wrapped SeasonMeta constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public SeasonMetaSharp(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public SeasonMeta ToSubstrate()
        {
            var seasonMeta = new SeasonMeta
            {
                Name = new BoundedVecT7(),
                Description = new BoundedVecT8()
            };

            seasonMeta.Name.Value = new BaseVec<U8>(Name.ToBytes().Select(p => new U8(p)).ToArray());
            seasonMeta.Description.Value = new BaseVec<U8>(Description.ToBytes().Select(p => new U8(p)).ToArray());

            return seasonMeta;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; }
    }
}
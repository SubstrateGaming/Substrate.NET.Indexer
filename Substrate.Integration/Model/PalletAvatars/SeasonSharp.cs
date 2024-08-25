using Substrate.Bajun.NET.NetApiExt.Generated.Model.bounded_collections.bounded_vec;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar.rarity_tier;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;
using Substrate.Bajun.NET.NetApiExt.Helper;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System.Linq;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Season
    /// </summary>
    public class SeasonSharp
    {
        /// <summary>
        ///  Wrapped Season constructor
        /// </summary>
        /// <param name="season"></param>
        public SeasonSharp(Season season)
        {
            MaxTierForges = season.MaxTierForges.Value;
            MaxVariations = season.MaxVariations.Value;
            MaxComponents = season.MaxComponents.Value;
            MinSacrifices = season.MinSacrifices.Value;
            MaxSacrifices = season.MaxSacrifices.Value;
            Tiers = season.Tiers.Value.Value.Select(p => p.Value).ToArray();
            SingleMintProbs = season.SingleMintProbs.Value.ToBytes();
            BatchMintProbs = season.BatchMintProbs.Value.ToBytes();
            BaseProb = season.BaseProb.Value;
            PerPeriod = season.PerPeriod.Value;
            Periods = season.Periods.Value;
            Fee = new FeeSharp(season.Fee);
            MintLogic = season.MintLogic.Value;
            ForgeLogic = season.ForgeLogic.Value;
        }

        /// <summary>
        /// Wrapped Season constructor
        /// </summary>
        /// <param name="maxTierForges"></param>
        /// <param name="maxVariations"></param>
        /// <param name="maxComponents"></param>
        /// <param name="minSacrifices"></param>
        /// <param name="maxSacrifices"></param>
        /// <param name="tiers"></param>
        /// <param name="singleMintProbs"></param>
        /// <param name="batchMintProbs"></param>
        /// <param name="baseProb"></param>
        /// <param name="perPeriod"></param>
        /// <param name="periods"></param>
        /// <param name="fee"></param>
        /// <param name="mintLogic"></param>
        /// <param name="forgeLogic"></param>
        public SeasonSharp(uint maxTierForges, byte maxVariations, byte maxComponents, byte minSacrifices, byte maxSacrifices, RarityTier[] tiers, byte[] singleMintProbs, byte[] batchMintProbs, byte baseProb, uint perPeriod, ushort periods, FeeSharp fee, LogicGeneration mintLogic, LogicGeneration forgeLogic)
        {
            MaxTierForges = maxTierForges;
            MaxVariations = maxVariations;
            MaxComponents = maxComponents;
            MinSacrifices = minSacrifices;
            MaxSacrifices = maxSacrifices;
            Tiers = tiers;
            SingleMintProbs = singleMintProbs;
            BatchMintProbs = batchMintProbs;
            BaseProb = baseProb;
            PerPeriod = perPeriod;
            Periods = periods;
            Fee = fee;
            MintLogic = mintLogic;
            ForgeLogic = forgeLogic;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public Season ToSubstrate()
        {
            var season = new Season
            {
                MaxTierForges = new U32(MaxTierForges),
                MaxVariations = new U8(MaxVariations),
                MaxComponents = new U8(MaxComponents),
                MinSacrifices = new U8(MinSacrifices),
                MaxSacrifices = new U8(MaxSacrifices),
                Tiers = new BoundedVecT5(),
                SingleMintProbs = new BoundedVecT6(),
                BatchMintProbs = new BoundedVecT6(),
                BaseProb = new U8(BaseProb),
                PerPeriod = new U32(PerPeriod),
                Periods = new U16(Periods),
                Fee = Fee.ToSubstrate(),
                MintLogic = new EnumLogicGeneration(),
                ForgeLogic = new EnumLogicGeneration()
            };

            var tiersArray = Tiers.Select(p => { var tier = new EnumRarityTier(); tier.Create(p); return tier; }).ToArray();
            season.Tiers.Value = new BaseVec<EnumRarityTier>();
            season.Tiers.Value.Create(tiersArray);

            season.SingleMintProbs.Value = new BaseVec<U8>(SingleMintProbs.Select(p => new U8(p)).ToArray());
            season.BatchMintProbs.Value = new BaseVec<U8>(BatchMintProbs.Select(p => new U8(p)).ToArray());

            season.MintLogic.Create(MintLogic);
            season.ForgeLogic.Create(ForgeLogic);

            return season;
        }

        /// <summary>
        /// Max Tier Forges
        /// </summary>
        public uint MaxTierForges { get; }

        /// <summary>
        /// Max Variations
        /// </summary>
        public byte MaxVariations { get; }

        /// <summary>
        /// Max Components
        /// </summary>
        public byte MaxComponents { get; }

        /// <summary>
        /// Min Sacrifices
        /// </summary>
        public byte MinSacrifices { get; }

        /// <summary>
        /// Max Sacrifices
        /// </summary>
        public byte MaxSacrifices { get; }

        /// <summary>
        /// Tiers
        /// </summary>
        public RarityTier[] Tiers { get; }

        /// <summary>
        /// Single Mint Probs
        /// </summary>
        public byte[] SingleMintProbs { get; }

        /// <summary>
        /// Batch Mint Probs
        /// </summary>
        public byte[] BatchMintProbs { get; }

        /// <summary>
        /// Base Prob
        /// </summary>
        public byte BaseProb { get; }

        /// <summary>
        /// Per Period
        /// </summary>
        public uint PerPeriod { get; }

        /// <summary>
        /// Periods
        /// </summary>
        public ushort Periods { get; }

        /// <summary>
        /// Fee
        /// </summary>
        public FeeSharp Fee { get; }

        /// <summary>
        /// Mint Logic
        /// </summary>
        public LogicGeneration MintLogic { get; }

        /// <summary>
        /// Forge Logic
        /// </summary>
        public LogicGeneration ForgeLogic { get; }
    }
}
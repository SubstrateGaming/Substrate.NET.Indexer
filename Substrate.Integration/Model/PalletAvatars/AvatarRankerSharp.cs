using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar.force;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar.tournament;
using Substrate.Bajun.NET.NetApiExt.Generated.Types.Base;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model.PalletAvatars
{
    /// <summary>
    /// Wrapped AvatarRanker
    /// </summary>
    public class AvatarRankerSharp
    {
        /// <summary>
        /// Wrapped AvatarRanker constructor
        /// </summary>
        /// <param name="avatarRanker"></param>
        public AvatarRankerSharp(AvatarRanker avatarRanker)
        {
            AvatarRankingCategory = avatarRanker.Category.Value;
            switch (avatarRanker.Category.Value)
            {
                case AvatarRankingCategory.MinSoulPoints:
                case AvatarRankingCategory.MaxSoulPoints:
                case AvatarRankingCategory.DnaAscending:
                case AvatarRankingCategory.DnaDescending:
                    break;

                case AvatarRankingCategory.MinSoulPointsWithForce:
                case AvatarRankingCategory.MaxSoulPointsWithForce:
                    Force = ((EnumForce)avatarRanker.Category.Value2).Value;
                    break;

                case AvatarRankingCategory.MintedAtModulo:
                    Modulo = ((U32)avatarRanker.Category.Value2).Value;
                    break;
            }
        }

        public AvatarRankerSharp(AvatarRankingCategory avatarRankingCategory, Force? force, uint? modulo)
        {
            AvatarRankingCategory = avatarRankingCategory;
            Force = null;
            Modulo = null;
            switch (avatarRankingCategory)
            {
                case AvatarRankingCategory.MinSoulPoints:
                case AvatarRankingCategory.MaxSoulPoints:
                case AvatarRankingCategory.DnaAscending:
                case AvatarRankingCategory.DnaDescending:
                    break;

                case AvatarRankingCategory.MinSoulPointsWithForce:
                case AvatarRankingCategory.MaxSoulPointsWithForce:
                    if (force is null)
                    {
                        throw new System.ArgumentNullException(nameof(force));
                    }
                    Force = force;
                    break;

                case AvatarRankingCategory.MintedAtModulo:
                    if (modulo is null)
                    {
                        throw new System.ArgumentNullException(nameof(modulo));
                    }
                    Modulo = modulo;
                    break;
            }
        }

        /// <summary>
        /// To Substrate AvatarRanker
        /// </summary>
        /// <returns></returns>
        public AvatarRanker ToSubstrate()
        {
            var avatarRanker = new AvatarRanker
            {
                Category = new EnumAvatarRankingCategory()
            };

            switch (AvatarRankingCategory)
            {
                case AvatarRankingCategory.MinSoulPoints:
                case AvatarRankingCategory.MaxSoulPoints:
                case AvatarRankingCategory.DnaAscending:
                case AvatarRankingCategory.DnaDescending:
                    avatarRanker.Category.Create(AvatarRankingCategory, new BaseVoid());
                    break;

                case AvatarRankingCategory.MinSoulPointsWithForce:
                case AvatarRankingCategory.MaxSoulPointsWithForce:
                    var enumForce = new EnumForce();
                    enumForce.Create(Force.Value);
                    avatarRanker.Category.Create(AvatarRankingCategory, enumForce);
                    break;

                case AvatarRankingCategory.MintedAtModulo:
                    var nonZeroU32 = new NonZeroU32();
                    nonZeroU32.Value = new U32(Modulo.Value);
                    avatarRanker.Category.Create(AvatarRankingCategory, nonZeroU32);
                    break;
            }
            return avatarRanker;
        }

        /// <summary>
        /// AvatarRankingCategory
        /// </summary>
        public AvatarRankingCategory AvatarRankingCategory { get; set; }

        /// <summary>
        /// Force
        /// </summary>
        public Force? Force { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public uint? Modulo { get; set; }
    }
}
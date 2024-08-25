using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_identity.legacy;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_identity.types;
using Substrate.NetApi.Model.Types.Base;
using System.Linq;

namespace Substrate.Integration.Model
{
    public class IdentityInfoSharp
    {
        private IdentityInfo info;

        public IdentityInfoSharp(IdentityInfo identityInfo)
        {
            Additional = null; // TODO
            Display = new EnumDataSharp(identityInfo.Display);
            Legal = new EnumDataSharp(identityInfo.Legal);
            Web = new EnumDataSharp(identityInfo.Web);
            Riot = new EnumDataSharp(identityInfo.Riot);
            Email = new EnumDataSharp(identityInfo.Email);
            PgpFingerprint = identityInfo.PgpFingerprint.OptionFlag ? identityInfo.PgpFingerprint.Value.Value.Select(p => p.Value).ToArray() : null ;
            Image = new EnumDataSharp(identityInfo.Image);
            Twitter = new EnumDataSharp(identityInfo.Twitter);
        }

        /// <summary>
        /// Additional
        /// </summary>
        public BaseTuple<EnumData, EnumData>[] Additional { get; }

        /// <summary>
        /// Display
        /// </summary>
        public EnumDataSharp Display { get; }

        /// <summary>
        /// Legal
        /// </summary>
        public EnumDataSharp Legal { get; }

        /// <summary>
        /// Web
        /// </summary>
        public EnumDataSharp Web { get; }


        /// <summary>
        /// Riot
        /// </summary>
        public EnumDataSharp Riot { get; }

        /// <summary>
        /// Email
        /// </summary>
        public EnumDataSharp Email { get; }

        /// <summary>
        /// PgpFingerprint
        /// </summary>
        public byte[]? PgpFingerprint { get; }

        /// <summary>
        /// Image
        /// </summary>
        public EnumDataSharp Image { get; }

        /// <summary>
        /// Twitter
        /// </summary>
        public EnumDataSharp Twitter { get; }
    }
}
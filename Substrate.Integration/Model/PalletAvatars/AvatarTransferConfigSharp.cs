using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Avatar Transfer Config C# Wrapper
    /// </summary>
    public class AvatarTransferConfigSharp
    {
        /// <summary>
        /// Avatar Transfer Config Constructor
        /// </summary>
        /// <param name="avatarTransfer"></param>
        public AvatarTransferConfigSharp(AvatarTransferConfig avatarTransfer)
        {
            Open = avatarTransfer.Open.Value;
        }

        /// <summary>
        /// Avatar Transfer Config Constructor
        /// </summary>
        /// <param name="open"></param>
        public AvatarTransferConfigSharp(bool open)
        {
            Open = open;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public AvatarTransferConfig ToSubstrate()
        {
            return new AvatarTransferConfig
            {
                Open = new Bool(Open)
            };
        }

        /// <summary>
        /// Open
        /// </summary>
        public bool Open { get; }
    }
}
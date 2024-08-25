using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.account;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped PlayerConfig
    /// </summary>
    public class PlayerConfigsSharp
    {
        /// <summary>
        /// Wrapped PlayerConfig constructor
        /// </summary>
        /// <param name="playerConfig"></param>
        public PlayerConfigsSharp(PlayerConfig playerConfig)
        {
            FreeMints = playerConfig.FreeMints.Value;
        }

        /// <summary>
        /// Free Mints
        /// </summary>
        public ushort FreeMints { get; private set; }
    }
}
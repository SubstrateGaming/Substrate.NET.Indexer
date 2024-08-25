using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapper Forge Config
    /// </summary>
    public class ForgeConfigSharp
    {
        /// <summary>
        /// Forge Config Constructor
        /// </summary>
        /// <param name="forge"></param>
        public ForgeConfigSharp(ForgeConfig forge)
        {
            Open = forge.Open.Value;
        }

        /// <summary>
        /// Forge Config Constructor
        /// </summary>
        /// <param name="open"></param>
        public ForgeConfigSharp(bool open)
        {
            Open = open;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public ForgeConfig ToSubstrate()
        {
            return new ForgeConfig
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
using Serilog;
using Substrate.Bajun.NET.NetApiExt.Client;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.affiliates;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_core.crypto;
using Substrate.Bajun.NET.NetApiExt.Helper;
using Substrate.Integration.Model;
using Substrate.NetApi.Extensions;
using Substrate.NetApi.Model.Types.Primitive;
using System.Threading;
using System.Threading.Tasks;

namespace Substrate.Integration
{
    /// <summary>
    /// Substrate network
    /// </summary>
    public partial class SubstrateNetwork : BaseClient
    {
        #region Storage

        /// <summary>
        /// Get the affiliatees of an account
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AccountId32[]?> GetAffiliateesAsync(AccountId32 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AffiliatesAAAStorage.Affiliatees(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Affiliatees is null!");
                return null;
            }

            return result.Value.Value;
        }

        /// <summary>
        /// Get the affiliator of an account
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AffiliatorStateSharp?> GetAffiliatorsAsync(AccountId32 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AffiliatesAAAStorage.Affiliators(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Affiliators is null!");
                return null;
            }

            return new AffiliatorStateSharp(result);
        }

        /// <summary>
        /// Get the affiliate rules
        /// </summary>
        /// <param name="affiliateMethods"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<byte[]?> GetAffiliateRulesAsync(AffiliateMethods affiliateMethods, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var enumAffiliateMethods = new EnumAffiliateMethods();
            enumAffiliateMethods.Create(affiliateMethods);

            var result = await SubstrateClient.AffiliatesAAAStorage.AffiliateRules(enumAffiliateMethods, blockhash, token);

            if (result == null)
            {
                Log.Warning("AffiliateRules is null!");
                return null;
            }

            return result.Value.ToBytes();
        }

        /// <summary>
        /// Get the next affiliate id
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<uint?> GetNextAffiliateIdAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AffiliatesAAAStorage.NextAffiliateId(blockhash, token);

            if (result == null)
            {
                Log.Warning("Next affiliate id is null!");
                return null;
            }

            return result;
        }

        /// <summary>
        /// Get the affiliate id mapping
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AccountId32?> GetAffiliateIdMappingAsync(U32 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AffiliatesAAAStorage.AffiliateIdMapping(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("AffiliateIdMapping is null!");
                return null;
            }

            return result;
        }

        #endregion Storage
    }
}
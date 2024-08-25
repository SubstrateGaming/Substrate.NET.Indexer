using Serilog;
using Substrate.Bajun.NET.NetApiExt.Client;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.bajun_runtime;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.bounded_collections.bounded_vec;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.affiliates;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar.tournament;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_tournament.config;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.primitive_types;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_core.crypto;
using Substrate.Bajun.NET.NetApiExt.Generated.Storage;
using Substrate.Bajun.NET.NetApiExt.Helper;
using Substrate.Integration.Model;
using Substrate.Integration.Model.PalletAvatars;
using Substrate.NetApi;
using Substrate.NetApi.Extensions;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Substrate.Integration
{
    /// <summary>
    /// Substrate network
    /// </summary>
    public partial class SubstrateNetwork : BaseClient
    {
        /// <summary>
        /// Get all avatars from storage
        /// </summary>
        /// <param name="accountId32"></param>
        /// <param name="seasonId"></param>
        /// <param name="recordsPerBatch"></param>
        /// <param name="blockHash"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AvatarSharp>> GetAccountAvatarsAsync(AccountId32 accountId32, ushort seasonId = 1, int recordsPerBatch = 1000, string blockHash = null)
        {
            var avatars = new List<AvatarSharp>();

            var key = new BaseTuple<AccountId32, U16>(accountId32, seasonId.ToU16());

            var ownerAvatarIds = await SubstrateClient.AwesomeAvatarsStorage.Owners(key, blockHash, CancellationToken.None);
            if (ownerAvatarIds == null)
            {
                return avatars;
            }

            var batchs = Generic.BuildChunksOf(
                ownerAvatarIds.Value.Value.Select(x => Utils.HexToByteArray(AwesomeAvatarsStorage.AvatarsParams(x))), recordsPerBatch);

            Log.Debug("We have {count} batches of {records} to proccess.", batchs.Count(), recordsPerBatch);

            foreach (var batch in batchs)
            {
                var batchResult = await SubstrateClient.State.GetQueryStorageAtAsync(batch.ToList(), blockHash, CancellationToken.None);
                foreach (var storageChangeSet in batchResult.First().Changes)
                {
                    var storageKeyString = storageChangeSet[0];

                    var keyParam = new H256();
                    //keyParam.Create(storageKeyString[^64..]);
                    keyParam.Create(storageKeyString.Substring(storageKeyString.Length - 64, 64));

                    var valueParam = new BaseTuple<AccountId32, Avatar>();

                    if (storageChangeSet[1] == null)
                    {
                        continue;
                    }

                    valueParam.Create(storageChangeSet[1]);

                    var avatarStruct = (Avatar)valueParam.Value[1];
                    avatars.Add(new AvatarSharp(avatarStruct));
                }
            }

            return avatars;
        }

        #region storage

        /// <summary>
        /// Get the organizer address as string
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> GetOrganizerAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Organizer(blockhash, token);

            if (result == null)
            {
                Log.Warning("Organizer is null!");
                return null;
            }

            return result.ToAddress();
        }

        /// <summary>
        /// Get treasurer
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> GetTreasurerAsync(U16 seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Treasurer(seasonId, blockhash, token);

            if (result == null)
            {
                Log.Warning("Treasurer is null!");
                return null;
            }

            return result.ToAddress();
        }

        /// <summary>
        /// Get Whitelisted Accounts
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string[]?> GetWhitelistedAccountsAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.WhitelistedAccounts(blockhash, token);

            if (result == null)
            {
                Log.Warning("WhitelistedAccounts is null!");
                return null;
            }

            return result.Value.Value.Select(p => p.ToAddress()).ToArray();
        }

        /// <summary>
        /// Get the current season status
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<SeasonStatusSharp?> GetCurrentSeasonStatusAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.CurrentSeasonStatus(blockhash, token);

            if (result == null)
            {
                Log.Warning("Current Season Status is null!");
                return null;
            }

            return new SeasonStatusSharp(result);
        }

        /// <summary>
        /// Get the Season by id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<SeasonSharp?> GetSeasonsAsync(U16 seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Seasons(seasonId, blockhash, token);

            if (result == null)
            {
                Log.Warning("Seasons is null!");
                return null;
            }

            return new SeasonSharp(result);
        }

        /// <summary>
        /// Get the season metas
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<SeasonMetaSharp?> GetSeasonMetasAsync(U16 seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.SeasonMetas(seasonId, blockhash, token);

            if (result == null)
            {
                Log.Warning("SeasonMeta is null!");
                return null;
            }

            return new SeasonMetaSharp(result);
        }

        /// <summary>
        /// Get the season schedules
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<SeasonScheduleSharp?> GetSeasonSchedulesAsync(U16 seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.SeasonSchedules(seasonId, blockhash, token);

            if (result == null)
            {
                Log.Warning("SeasonSchedule is null!");
                return null;
            }

            return new SeasonScheduleSharp(result);
        }

        /// <summary>
        /// Get the season trade filters
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TradeFiltersSharp?> GetSeasonTradeFiltersAsync(U16 seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.SeasonTradeFilters(seasonId, blockhash, token);

            if (result == null)
            {
                Log.Warning("SeasonTradeFilters is null!");
                return null;
            }

            return new TradeFiltersSharp(result);
        }

        /// <summary>
        /// Get Season Unlock Configs
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UnlockConfigsSharp?> GetSeasonUnlocksAsync(U16 seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.SeasonUnlocks(seasonId, blockhash, token);

            if (result == null)
            {
                Log.Warning("SeasonUnlocks is null!");
                return null;
            }

            return new UnlockConfigsSharp(result);
        }

        /// <summary>
        /// Get the treasury
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BigInteger?> GetTreasuryAsync(U16 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Treasury(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Treasury is null!");
                return null;
            }

            return result.Value;
        }

        /// <summary>
        /// Get the global config
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GlobalConfigSharp?> GetGlobalConfigsAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.GlobalConfigs(blockhash, token);

            if (result == null)
            {
                Log.Warning("GlobalConfigs is null!");
                return null;
            }

            return new GlobalConfigSharp(result);
        }

        /// <summary>
        /// Get the avatars
        /// </summary>
        /// <param name="avatarId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<(string, AvatarSharp)?> GetAvatarsAsync(H256 avatarId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Avatars(avatarId, blockhash, token);

            if (result == null)
            {
                Log.Warning("Avatars is null!");
                return null;
            }

            return (((AccountId32)result.Value[0]).ToAddress(), new AvatarSharp((Avatar)result.Value[1]));
        }

        /// <summary>
        /// Get the owners
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string[]?> GetOwnersAsync(BaseTuple<AccountId32, U16> key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Owners(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Owners is null!");
                return null;
            }

            return result.Value.Value.Select(p => p.ToHexString()).ToArray();
        }

        /// <summary>
        /// Get locked avatars
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool?> GetLockedAvatarsAsync(H256 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.LockedAvatars(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("LockedAvatars is null!");
                return null;
            }

            return true;
        }

        /// <summary>
        /// Get collection id
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<uint?> GetCollectionIdAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.CollectionId(blockhash, token);

            if (result == null)
            {
                Log.Warning("CollectionId is null!");
                return null;
            }

            return result.Value;
        }

        /// <summary>
        /// Get player configs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PlayerConfigsSharp?> GetPlayerConfigsAsync(AccountId32 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.PlayerConfigs(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("PlayerConfigs is null!");
                return null;
            }

            return new PlayerConfigsSharp(result);
        }

        /// <summary>
        /// Get the player season configs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PlayerSeasonConfigsSharp?> GetPlayerSeasonConfigsAsync(BaseTuple<AccountId32, U16> key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.PlayerSeasonConfigs(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("PlayerSeasonConfigs is null!");
                return null;
            }

            return new PlayerSeasonConfigsSharp(result);
        }

        /// <summary>
        /// Get the season stats
        /// </summary>
        /// <param name="seasonAccount"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<SeasonInfoSharp?> GetSeasonStatsAsync(BaseTuple<U16, AccountId32> seasonAccount, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.SeasonStats(seasonAccount, blockhash, token);

            if (result == null)
            {
                Log.Warning("SeasonStats is null!");
                return null;
            }

            return new SeasonInfoSharp(result);
        }

        /// <summary>
        /// Get trade
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BigInteger?> GetTradeAsync(BaseTuple<U16, H256> key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Trade(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Trade is null!");
                return null;
            }

            return result.Value;
        }

        /// <summary>
        /// Get service account
        /// </summary>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> GetServiceAccountAsync(string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.ServiceAccount(blockhash, token);

            if (result == null)
            {
                Log.Warning("ServiceAccount is null!");
                return null;
            }

            return result.ToAddress();
        }

        /// <summary>
        /// Get preparation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<byte[]?> GetPreparationAsync(H256 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.Preparation(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Preparation is null!");
                return null;
            }

            return result.Value.Value.Select(p => p.Value).ToArray();
        }

        /// <summary>
        /// Get tournament rankers
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AvatarRankerSharp?> GetTournamentRankerAsync(BaseTuple<U16, U32> key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.AwesomeAvatarsStorage.TournamentRankers(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Preparation is null!");
                return null;
            }

            return new AvatarRankerSharp(result);
        }

        #endregion storage

        #region call

        /// <summary>
        /// Mint avatars
        /// </summary>
        /// <param name="account"></param>
        /// <param name="mintOption"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> MintAsync(Account account, MintOption mintOption, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.Mint";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.Mint(mintOption);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Forge avatars
        /// </summary>
        /// <param name="account"></param>
        /// <param name="leader"></param>
        /// <param name="sacrificies"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> ForgeAsync(Account account, H256 leader, BaseVec<H256> sacrificies, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.Forge";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.Forge(leader, sacrificies);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Transfer avatar
        /// </summary>
        /// <param name="account"></param>
        /// <param name="to"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> TransferAvatarAsync(Account account, AccountId32 to, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.TransferAvatar";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.TransferAvatar(to, avatarId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Transfer free mints
        /// </summary>
        /// <param name="account"></param>
        /// <param name="dest"></param>
        /// <param name="howMany"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> TransferFreeMintsAsync(Account account, AccountId32 dest, U16 howMany, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.TransferFreeMints";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.TransferFreeMints(dest, howMany);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set price
        /// </summary>
        /// <param name="account"></param>
        /// <param name="avatarId"></param>
        /// <param name="price"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetPriceAsync(Account account, H256 avatarId, BaseCom<U128> price, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetPrice";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetPrice(avatarId, price);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Remove price
        /// </summary>
        /// <param name="account"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> RemovePriceAsync(Account account, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.RemovePrice";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.RemovePrice(avatarId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Buy an avatar
        /// </summary>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> BuyAsync(H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.Buy";

            if (!IsConnected || Account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.Buy(avatarId);

            return await GenericExtrinsicAsync(Account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        private string UpgradeStorageKey => "AwesomeAvatarsCalls.UpgradeStorage";

        /// <summary>
        /// Check if the storage can be upgraded
        /// </summary>
        /// <param name="concurrentTasks"></param>
        /// <returns></returns>
        public bool CanUpgradeStorage(int concurrentTasks)
            => !HasToManyConcurentTaskRunning(UpgradeStorageKey, concurrentTasks);

        /// <summary>
        /// Upgrade the storage
        /// </summary>
        /// <param name="account"></param>
        /// <param name="accountId"></param>
        /// <param name="seasonId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> UpgradeStorageAsync(Account account, AccountId32 accountId, ushort? seasonId, int concurrentTasks, CancellationToken token)
        {
            if (!IsConnected || account == null)
            {
                return null;
            }

            var baseOptAccount = new BaseOpt<AccountId32>(accountId);

            var baseOptSeason = new BaseOpt<U16>();
            if (seasonId != null)
            {
                var u16 = new U16();
                u16.Create(seasonId.Value);
                baseOptSeason.Create(u16);
            }

            var extrinsic = AwesomeAvatarsCalls.UpgradeStorage(baseOptAccount, baseOptSeason);

            return await GenericExtrinsicAsync(account, UpgradeStorageKey, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set the organizer
        /// </summary>
        /// <param name="sudo"></param>
        /// <param name="organizer"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetOrganizerAsync(Account sudo, AccountId32 organizer, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetOrganizer";

            if (!IsConnected || sudo == null)
            {
                return null;
            }

            var enumCall = new EnumRuntimeCall();
            var palletCall = new Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet.EnumCall();
            palletCall.Create(Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet.Call.set_organizer, organizer);
            enumCall.Create(RuntimeCall.AwesomeAvatars, palletCall);
            var extrinsic = SudoCalls.Sudo(enumCall);

            return await GenericExtrinsicAsync(sudo, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set the treasurer
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="seasonId"></param>
        /// <param name="treasurer"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetTreasurerAsync(Account organizer, U16 seasonId, AccountId32 treasurer, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetTreasurer";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetTreasurer(seasonId, treasurer);

            return await GenericExtrinsicAsync(Alice, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Claim the treasury
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="seasonId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> ClaimTreasuryAsync(Account organizer, U16 seasonId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.ClaimTreasury";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.ClaimTreasury(seasonId);

            // TODO: Check Alice is the treasurer or if it needs to be a sudo call
            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set the season informations
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="seasonId"></param>
        /// <param name="season"></param>
        /// <param name="seasonMeta"></param>
        /// <param name="seasonSchedule"></param>
        /// <param name="tradeFilters"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetSeasonAsync(Account organizer, U16 seasonId, Season season, SeasonMeta seasonMeta, SeasonSchedule seasonSchedule, TradeFilters tradeFilters, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetSeason";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var seasonOpt = new BaseOpt<Season>(season);
            var seasonMetaOpt = new BaseOpt<SeasonMeta>(seasonMeta);
            var seasonScheduleOpt = new BaseOpt<SeasonSchedule>(seasonSchedule);
            var tradeFiltersOpt = new BaseOpt<TradeFilters>(tradeFilters);

            var extrinsic = AwesomeAvatarsCalls.SetSeason(seasonId, seasonOpt, seasonMetaOpt, seasonScheduleOpt, tradeFiltersOpt);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Update the global config
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="newGlobalConfig"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> UpdateGlobalConfigAsync(Account organizer, GlobalConfig newGlobalConfig, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.UpdateGlobalConfig";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.UpdateGlobalConfig(newGlobalConfig);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set Free Mints
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="dest"></param>
        /// <param name="howMany"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetFreeMintsAsync(Account organizer, AccountId32 dest, U16 howMany, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetFreeMints";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetFreeMints(dest, howMany);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set the collection id
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="collectionId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetCollectionIdAsync(Account organizer, uint collectionId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetCollectionId";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetCollectionId(new U32(collectionId));

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Lock an avatar
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> LockAvatarAsync(Account organizer, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.LockAvatar";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.LockAvatar(avatarId);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Unlock an avatar
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> UnlockAvatarAsync(Account organizer, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.UnlockAvatar";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.UnlockAvatar(avatarId);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set service account
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="serviceAccount"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetServiceAccountAsync(Account organizer, AccountId32 serviceAccount, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetServiceAccount";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetServiceAccount(serviceAccount);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Prepare an avatar
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> PrepareAvatarAsync(Account organizer, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.PrepareAvatar";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.PrepareAvatar(avatarId);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Unprepare an avatar
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> UnprepareAvatarAsync(Account organizer, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.UnprepareAvatar";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.UnprepareAvatar(avatarId);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Prepare an ipfs
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="avatarId"></param>
        /// <param name="url"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> PrepareIpfsAsync(Account organizer, H256 avatarId, string url, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.PrepareIpfs";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var urlBoundedVec = new BoundedVecT10();
            urlBoundedVec.Value = new BaseVec<U8>();
            urlBoundedVec.Value.Create(url.ToBytes());

            var extrinsic = AwesomeAvatarsCalls.PrepareIpfs(avatarId, urlBoundedVec);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Modify the freemint whitelist
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="account"></param>
        /// <param name="operation"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> ModifyFreemintWhitelistAsync(Account organizer, AccountId32 account, WhitelistOperation operation, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.ModifyFreemintWhitelist";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var whiteListOperation = new EnumWhitelistOperation();
            whiteListOperation.Create(operation);

            var extrinsic = AwesomeAvatarsCalls.ModifyFreemintWhitelist(account, whiteListOperation);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Add an affiliation
        /// </summary>
        /// <param name="account"></param>
        /// <param name="accountFor"></param>
        /// <param name="affiliateId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> AddAffiliationAsync(Account account, AccountId32? accountFor, U32 affiliateId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.AddAffiliation";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var optAccountId = new BaseOpt<AccountId32>();
            if (accountFor != null)
            {
                optAccountId.Create(accountFor);
            }

            var extrinsic = AwesomeAvatarsCalls.AddAffiliation(optAccountId, affiliateId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Enable the affiliator
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="target"></param>
        /// <param name="seasonId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> EnableAffiliatorAsync(Account organizer, EnumUnlockTarget target, U16 seasonId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.EnableAffiliator";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.EnableAffiliator(target, seasonId);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Remove an affiliation
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="accountId32"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> RemoveAffiliationAsync(Account organizer, AccountId32 accountId32, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.RemoveAffiliation";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.RemoveAffiliation(accountId32);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set the rule for
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="ruleId"></param>
        /// <param name="rule"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetRuleForAsync(Account organizer, EnumAffiliateMethods ruleId, BoundedVecT21 rule, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetRuleFor";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetRuleFor(ruleId, rule);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Clear the rule for
        /// </summary>
        /// <param name="organizer"></param>
        /// <param name="ruleId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> ClearRuleForAsync(Account organizer, EnumAffiliateMethods ruleId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.ClearRuleFor";

            if (!IsConnected || organizer == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.ClearRuleFor(ruleId);

            return await GenericExtrinsicAsync(organizer, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Enable set avatar price
        /// </summary>
        /// <param name="account"></param>
        /// <param name="target"></param>
        /// <param name="seasonId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> EnableSetAvatarPriceAsync(Account account, EnumUnlockTarget target, U16 seasonId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.EnableSetAvatarPrice";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.EnableSetAvatarPrice(target, seasonId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Enable avatar transfer
        /// </summary>
        /// <param name="account"></param>
        /// <param name="target"></param>
        /// <param name="seasonId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> EnableAvatarTransferAsync(Account account, EnumUnlockTarget target, U16 seasonId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.EnableAvatarTransfer";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.EnableAvatarTransfer(target, seasonId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Set the unlock config
        /// </summary>
        /// <param name="account"></param>
        /// <param name="seasonId"></param>
        /// <param name="unlockConfigs"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> SetUnlockConfigAsync(Account account, U16 seasonId, UnlockConfigs unlockConfigs, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.SetUnlockConfig";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.SetUnlockConfig(seasonId, unlockConfigs);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Create a tournament
        /// </summary>
        /// <param name="account"></param>
        /// <param name="seasonId"></param>
        /// <param name="tournamentConfig"></param>
        /// <param name="ranker"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> CreateTournamentAsync(Account account, U16 seasonId, TournamentConfig tournamentConfig, AvatarRanker ranker, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.CreateTournament";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.CreateTournament(seasonId, tournamentConfig, ranker);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Remove the latest tournament
        /// </summary>
        /// <param name="account"></param>
        /// <param name="seasonId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> RemoveLatestTournamentAsync(Account account, U16 seasonId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.RemoveLatestTournament";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.RemoveLatestTournament(seasonId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Claim the tournament reward
        /// </summary>
        /// <param name="account"></param>
        /// <param name="seasonId"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> ClaimTournamentRewardForAsync(Account account, U16 seasonId, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.ClaimTournamentRewardFor";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.ClaimTournamentRewardFor(seasonId, avatarId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        /// <summary>
        /// Claim the golden duck
        /// </summary>
        /// <param name="account"></param>
        /// <param name="seasonId"></param>
        /// <param name="avatarId"></param>
        /// <param name="concurrentTasks"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string?> ClaimGoldenDuckForAsync(Account account, U16 seasonId, H256 avatarId, int concurrentTasks, CancellationToken token)
        {
            var extrinsicType = "AwesomeAvatarsCalls.ClaimGoldenDuckFor";

            if (!IsConnected || account == null)
            {
                return null;
            }

            var extrinsic = AwesomeAvatarsCalls.ClaimGoldenDuckFor(seasonId, avatarId);

            return await GenericExtrinsicAsync(account, extrinsicType, extrinsic, concurrentTasks, token);
        }

        #endregion call
    }
}
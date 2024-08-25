using Serilog;
using Substrate.Bajun.NET.NetApiExt.Client;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.avatar;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.primitive_types;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_core.crypto;
using Substrate.Integration.Model;
using Substrate.Integration.Model.PalletTournament;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System.Collections.Generic;
using System.Linq;
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
        /// Get the tournament schedules
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TournamentScheduleActionSharp?> GetTournamentSchedulesAsync(U32 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.TournamentSchedules(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("TournamentSchedules is null!");
                return null;
            }

            return new TournamentScheduleActionSharp(result);
        }

        /// <summary>
        /// Get the treasury accounts cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AccountId32?> GetTreasuryAccountsCacheAsync(U16 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.TreasuryAccountsCache(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("TournamentSchedules is null!");
                return null;
            }

            return result;
        }

        /// <summary>
        /// Get the next tournament ids
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<uint?> GetNextTournamentIdsAsync(U16 key, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.NextTournamentIds(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("Next tournament id is null!");
                return null;
            }

            return result;
        }

        /// <summary>
        /// Get the tournaments config
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="tournamentId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TournamentConfigSharp?> GetTournamentsAsync(ushort seasonId, uint tournamentId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.Tournaments(new BaseTuple<U16, U32>(new U16(seasonId), new U32(tournamentId)), blockhash, token);

            if (result == null)
            {
                Log.Warning("Next tournament id is null!");
                return null;
            }

            return new TournamentConfigSharp(result);
        }

        /// <summary>
        /// Get the active tournaments
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TournamentStateSharp?> GetActiveTournamentsAsync(ushort seasonId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.ActiveTournaments(new U16(seasonId), blockhash, token);

            if (result == null)
            {
                Log.Warning("ActiveTournaments is null!");
                return null;
            }

            return new TournamentStateSharp(result);
        }

        /// <summary>
        /// Get the tournament rankings
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="tournamentId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<(H256 avatarId, AvatarSharp avatar)>?> GetTournamentRankingsAsync(ushort seasonId, uint tournamentId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.TournamentRankings(new BaseTuple<U16, U32>(new U16(seasonId), new U32(tournamentId)), blockhash, token);

            if (result == null)
            {
                Log.Warning("TournamentRankings is null!");
                return null;
            }

            return result.Value.Value.Select(p => ((H256)p.Value[0], new AvatarSharp((Avatar)p.Value[1]))).ToList();
        }

        /// <summary>
        /// Get the tournament reward claims
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="tournamentId"></param>
        /// <param name="rankId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<RewardClaimStateSharp?> GetTournamentRewardClaimsAsync(ushort seasonId, uint tournamentId, uint rankId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.TournamentRewardClaims(new BaseTuple<U16, U32, U32>(new U16(seasonId), new U32(tournamentId), new U32(rankId)), blockhash, token);

            if (result == null)
            {
                Log.Warning("RewardClaimStateSharp is null!");
                return null;
            }

            return new RewardClaimStateSharp(result);
        }

        /// <summary>
        /// Get the golden ducks
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<GoldenDuckStateSharp?> GetGoldenDucksAsync(ushort key1, uint key2, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var key = new BaseTuple<U16, U32>();
            key.Create(new U16(key1), new U32(key2));

            var result = await SubstrateClient.TournamentAAAStorage.GoldenDucks(key, blockhash, token);

            if (result == null)
            {
                Log.Warning("GoldenDucks is null!");
                return null;
            }

            return new GoldenDuckStateSharp(result);
        }

        /// <summary>
        /// Get the golden duck reward claims
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="tournamentId"></param>
        /// <param name="blockhash"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<RewardClaimStateSharp?> GetGoldenDuckRewardClaimsAsync(ushort seasonId, uint tournamentId, string? blockhash, CancellationToken token)
        {
            if (!IsConnected)
            {
                Log.Warning("Currently not connected to the network!");
                return null;
            }

            var result = await SubstrateClient.TournamentAAAStorage.GoldenDuckRewardClaims(new BaseTuple<U16, U32>(new U16(seasonId), new U32(tournamentId)), blockhash, token);

            if (result == null)
            {
                Log.Warning("RewardClaimStateSharp is null!");
                return null;
            }

            return new RewardClaimStateSharp(result);
        }

        #endregion Storage
    }
}
using Serilog;
using System.Net.WebSockets;
using System;
using Substrate.NET.Indexer.Data;
using Substrate.Integration;
using Substrate.Bajun.NET.NetApiExt.Client;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.primitive_types;
using Substrate.NetApi.Model.Types.Primitive;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.config;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;
using Substrate.Integration.Model;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Rpc;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.frame_system;
using System.Diagnostics;
using Substrate.Integration.Model.FrameSystem;
using Substrate.NET.Indexer.Model;

namespace Substrate.NET.Indexer
{
    public class IndexerUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IConfiguration _configuration;

        private uint _currentIndex = 5781869;

        public Dictionary<BlockNumber, Hash> BlocksDict { get; private set; }

        public Dictionary<BlockNumber, BaseVec<EventRecord>> EventsDict { get; private set; }

        public IndexerUpdateService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            var socketException = 0;

            var updateTimeoutMin = int.Parse(_configuration["base:update_timeout_min"]);
            var nodeUrl = _configuration["node:url"];

            // Task to handle regular updates
            var regularUpdateTask = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<IndexerContext>();

                        try
                        {
                            await PollNodeAsync(nodeUrl, token);
                            await UpdateDatabaseAsync(dbContext, token);
                        }
                        catch (WebSocketException ex)
                        {
                            socketException++;
                            Log.Error(ex, "Service Unavailable. Error {1}: {0}", ex.ErrorCode, socketException);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Error occurred while updating database");
                        }
                    }

                    // Wait for the regular update interval
                    await Task.Delay(TimeSpan.FromMinutes(updateTimeoutMin), token);
                }
            }, token);
        }

        private async Task PollNodeAsync(string nodeUrl, CancellationToken token)
        {
            var client = new SubstrateNetwork(BaseClient.Alice, nodeUrl);

            if (!await client.ConnectAsync(true, true, token))
            {
                Log.Warning($"Failed to connect to the node {nodeUrl}!");
                return;
            }

            var blockNumber = new BlockNumber(_currentIndex);
            var blockHash = await client.SubstrateClient.Chain.GetBlockHashAsync(blockNumber, token);

            Log.Debug($"Block Hash {blockHash?.Value}");

            if (blockHash == null)
            {
                Log.Warning($"Failed to retrieve the blockchash from the node {nodeUrl}!");
                await client.DisconnectAsync();
                return;
            }

            var block = await client.SubstrateClient.Chain.GetBlockAsync(blockHash, token);

            TimeSpan elapsed;

            BlocksDict = new Dictionary<BlockNumber, Hash>() { { blockNumber, blockHash } };

            Dictionary<BaseVoid, BaseVec<EventRecord>>? eventsDict = null;
            elapsed = await ActionTimeAsync(async () =>
              eventsDict = await client.GetAllStorageAsync<BaseVoid, BaseVec<EventRecord>>("System", "Events", blockHash.Value, token));

            await client.DisconnectAsync();

            EventsDict = [];
            if (eventsDict == null)
            {
                Log.Warning($"FailNo events found for blocknumber {blockNumber.Value}!");
            }
            else if (eventsDict.Count == 0) { }
            else if (eventsDict.Count == 1)
            {
                EventsDict.Add(blockNumber, eventsDict.First().Value);
            }
            else
            {
                Log.Warning($"We received more then one array of events for blocknumber {blockNumber.Value}!");
            }

            Log.Debug($"Time: {elapsed.TotalSeconds}s for Poll events. [{EventsDict?.Count}]");
        }

        private async Task UpdateDatabaseAsync(IndexerContext dbContext, CancellationToken token)
        {
            TimeSpan elapsed;

            // Add, Change & Delete avatars
            elapsed = ActionTime(() =>
                StoreBlocks(dbContext, BlocksDict));
            Log.Debug($"Time: {elapsed.TotalSeconds}s for Add/Change/Remove blocks.");

            // Save the changes to the database
            elapsed = await ActionTimeAsync(async () => 
                await dbContext.SaveChangesAsync(token));
            Log.Debug($"Time: {elapsed.TotalSeconds}s for Store blocks.");

            // Add, Change & Delete avatars
            elapsed = ActionTime(() =>
                StoreEvents(dbContext, EventsDict));
            Log.Debug($"Time: {elapsed.TotalSeconds}s for Add/Change/Remove events.");

            // Save the changes to the database
            elapsed = await ActionTimeAsync(async () => 
                await dbContext.SaveChangesAsync(token));
            Log.Debug($"Time: {elapsed.TotalSeconds}s for Store events.");
        }


    private void StoreBlocks(IndexerContext dbContext, Dictionary<BlockNumber, Hash> storageBlocks)
    {
        if (storageBlocks == null || storageBlocks.Count == 0)
        {
            Log.Warning("Skipping blocks as storageBlocks is null or empty.");
            return;
        }

        foreach (var kvp in storageBlocks)
        {
            uint blockNumber = kvp.Key.Value;
            string blockHash = kvp.Value.ToString();

            var dbBlock = new DbBlock
            {
                BlockNumber = blockNumber,
                BlockHash = blockHash,
                Events = []
            };

            dbContext.Blocks.Add(dbBlock);
        }
    }

    private void StoreEvents(IndexerContext dbContext, Dictionary<BlockNumber, BaseVec<EventRecord>> storageEvents)
    {
        if (storageEvents == null || storageEvents.Count == 0)
        {
            Log.Warning("Skipping events as storageEvents is null or empty.");
            return;
        }

        foreach (var kvp in storageEvents)
        {
            uint blockNumber = kvp.Key.Value;
            EventRecord[] eventRecords = kvp.Value.Value;

            var dbBlock = dbContext.Blocks.Find(blockNumber);

            if (dbBlock == null)
            {
                Log.Warning($"Block {blockNumber} not found in the database. Skipping events.");
                continue;
            }

            for (short i = 0; i < eventRecords.Length; i++)
            {
                EventRecordSharp eventRecordSharp = new(eventRecords[i]);

                var dbEvent = new DbEvent
                {
                    EventId = $"{blockNumber}-{i}",
                    BlockNumber = blockNumber,
                    Index = i,
                    Phase = eventRecordSharp.Phase.Phase,
                    PhaseValue = eventRecordSharp.Phase.Value,
                    ModuleName = eventRecordSharp.Event.Module.ToString(),
                    EventName = eventRecordSharp.Event.EventName,
                    ParameterType = eventRecordSharp.Event.ParameterType?.FullName,
                    ParameterBytes = eventRecordSharp.Event.ParameterBytes
                };

                dbBlock.Events.Add(dbEvent);
            }
        }
    }

    public async Task<TimeSpan> ActionTimeAsync(Func<Task> asyncAction)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        await asyncAction();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    public TimeSpan ActionTime(Action action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    public class EventKey
    {
        public uint BlockNumber { get; }
        public short EventIndex { get; }

        public EventKey(uint blockNumber, short eventIndex)
        {
            BlockNumber = blockNumber;
            EventIndex = eventIndex;
        }

        public override bool Equals(object obj)
        {
            if (obj is EventKey other)
            {
                return BlockNumber == other.BlockNumber && EventIndex == other.EventIndex;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BlockNumber, EventIndex);
        }

        public override string ToString()
        {
            return $"{BlockNumber}-{EventIndex}";
        }
    }
}
}
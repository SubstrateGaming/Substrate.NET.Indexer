using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.types.season;
using Substrate.NetApi.Model.Types.Primitive;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Wrapped Season Schedule
    /// </summary>
    public class SeasonScheduleSharp
    {
        /// <summary>
        /// Wrapped Season Schedule constructor
        /// </summary>
        /// <param name="seasonSchedule"></param>
        public SeasonScheduleSharp(SeasonSchedule seasonSchedule)
        {
            EarlyStart = seasonSchedule.EarlyStart.Value;
            Start = seasonSchedule.Start.Value;
            End = seasonSchedule.End.Value;
        }

        /// <summary>
        /// Wrapped Season Schedule constructor
        /// </summary>
        /// <param name="earlyStart"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public SeasonScheduleSharp(uint earlyStart, uint start, uint end)
        {
            EarlyStart = earlyStart;
            Start = start;
            End = end;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public SeasonSchedule ToSubstrate()
        {
            return new SeasonSchedule
            {
                EarlyStart = new U32(EarlyStart),
                Start = new U32(Start),
                End = new U32(End)
            };
        }

        /// <summary>
        /// Early Start
        /// </summary>
        public uint EarlyStart { get; }

        /// <summary>
        /// Start
        /// </summary>
        public uint Start { get; }

        /// <summary>
        /// End
        /// </summary>
        public uint End { get; }
    }
}
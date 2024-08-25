using Substrate.Bajun.NET.NetApiExt.Generated.Model.orml_vesting;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System.Numerics;

namespace Ajuna.ExtrinsicTools.Model.OrlmVesting
{
    /// <summary>
    /// Wrapper for VestingSchedule
    /// </summary>
    public class VestingScheduleSharp
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vestingSchedule"></param>
        public VestingScheduleSharp(VestingSchedule vestingSchedule)
        {
            Start = vestingSchedule.Start.Value;
            Period = vestingSchedule.Period.Value;
            PeriodCount = vestingSchedule.PeriodCount.Value;
            PerPeriod = vestingSchedule.PerPeriod.Value.Value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="start"></param>
        /// <param name="period"></param>
        /// <param name="periodCount"></param>
        /// <param name="perPeriod"></param>
        public VestingScheduleSharp(uint start, uint period, uint periodCount, BigInteger perPeriod)
        {
            Start = start;
            Period = period;
            PeriodCount = periodCount;
            PerPeriod = perPeriod;
        }

        /// <summary>
        /// Convert to Substrate
        /// </summary>
        /// <returns></returns>
        public VestingSchedule ToSubstrate()
        {
            var result = new VestingSchedule();
            result.Start = new U32(Start);
            result.Period = new U32(Period);
            result.PeriodCount = new U32(PeriodCount);
            result.PerPeriod = new BaseCom<U128>(PerPeriod);
            return result;
        }

        /// <summary>
        /// Start
        /// </summary>
        public uint Start { get; private set; }

        /// <summary>
        /// Period
        /// </summary>
        public uint Period { get; private set; }

        /// <summary>
        /// PeriodCount
        /// </summary>
        public uint PeriodCount { get; private set; }

        /// <summary>
        /// PerPeriod
        /// </summary>
        public BigInteger PerPeriod { get; private set; }
    }
}

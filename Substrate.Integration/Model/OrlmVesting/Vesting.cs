using Substrate.Bajun.NET.NetApiExt.Generated.Model.bajun_runtime;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.orml_vesting;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_core.crypto;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_runtime.multiaddress;
using Substrate.NetApi.Model.Types.Base;

namespace Ajuna.ExtrinsicTools.Model.OrlmVesting
{
    public class Vesting
    {
        /// <summary>
        /// Vesting transfer
        /// </summary>
        /// <param name="target"></param>
        /// <param name="vestingScheduleSharp"></param>
        /// <returns></returns>
        public static EnumRuntimeCall VestedTransfer(AccountId32 target, VestingScheduleSharp vestingScheduleSharp)
        {
            var multiAddress = new EnumMultiAddress();
            multiAddress.Create(MultiAddress.Id, target);

            var baseTuple = new BaseTuple<EnumMultiAddress, VestingSchedule>();
            baseTuple.Create(multiAddress, vestingScheduleSharp.ToSubstrate());

            var call = new Substrate.Bajun.NET.NetApiExt.Generated.Model.orml_vesting.module.EnumCall();
            call.Create(Substrate.Bajun.NET.NetApiExt.Generated.Model.orml_vesting.module.Call.vested_transfer, baseTuple);

            var enumCall = new EnumRuntimeCall();
            enumCall.Create(RuntimeCall.Vesting, call);

            return enumCall;
        }
    }
}
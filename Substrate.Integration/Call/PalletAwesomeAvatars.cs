using Substrate.Bajun.NET.NetApiExt.Generated.Model.bajun_runtime;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.primitive_types;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_core.crypto;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System.Collections.Generic;
using System.Linq;

namespace Substrate.Integration.Call
{
    /// <summary>
    /// Pallet AwesomeAvatars
    /// </summary>
    public static class PalletAwesomeAvatars
    {
        /// <summary>
        /// Transfer Free Mints call
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static EnumRuntimeCall TransferFreeMints(AccountId32 target, ushort amount)
        {
            var baseTubleParams = new BaseTuple<AccountId32, U16>();
            baseTubleParams.Create(target, new U16(amount));

            var enumPalletCall = new EnumCall();
            enumPalletCall.Create(Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet.Call.transfer_free_mints, baseTubleParams);

            var enumCall = new EnumRuntimeCall();
            enumCall.Create(RuntimeCall.AwesomeAvatars, enumPalletCall);

            return enumCall;
        }

        /// <summary>
        /// Add Affiliation call
        /// </summary>
        /// <param name="accountFor"></param>
        /// <param name="affiliateId"></param>
        /// <returns></returns>
        public static EnumRuntimeCall AddAffiliation(AccountId32? accountFor, uint affiliateId)
        {
            var baseTubleParams = new BaseTuple<BaseOpt<AccountId32>, U32>();
            var optAccountId = new BaseOpt<AccountId32>();
            if (accountFor != null)
            {
                optAccountId.Create(accountFor);
            }
            baseTubleParams.Create(optAccountId, new U32(affiliateId));

            var enumPalletCall = new EnumCall();
            enumPalletCall.Create(Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet.Call.add_affiliation, baseTubleParams);

            var enumCall = new EnumRuntimeCall();
            enumCall.Create(RuntimeCall.AwesomeAvatars, enumPalletCall);

            return enumCall;
        }

        /// <summary>
        /// Set Free Mints call
        /// </summary>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static EnumRuntimeCall SetFreeMints(AccountId32 target, ushort amount)
        {
            var baseTubleParams = new BaseTuple<AccountId32, U16>();
            baseTubleParams.Create(target, new U16(amount));

            var enumPalletCall = new EnumCall();
            enumPalletCall.Create(Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet.Call.set_free_mints, baseTubleParams);

            var enumCall = new EnumRuntimeCall();
            enumCall.Create(RuntimeCall.AwesomeAvatars, enumPalletCall);

            return enumCall;
        }

        /// <summary>
        /// Forge call
        /// </summary>
        /// <param name="leader"></param>
        /// <param name="sacrifices"></param>
        /// <returns></returns>
        public static EnumRuntimeCall Forge(string leader, List<string> sacrifices)
        {
            var h256 = new H256();
            h256.Create(leader);

            var h256Array = sacrifices.Select(x =>
            {
                var result = new H256();
                result.Create(x);
                return result;
            });

            var baseVec = new BaseVec<H256>();
            baseVec.Create(h256Array.ToArray());

            var forgeConfig = new BaseTuple<H256, BaseVec<H256>>();
            forgeConfig.Create(h256, baseVec);

            var enumCall = new EnumCall();
            enumCall.Create(Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_ajuna_awesome_avatars.pallet.Call.forge, forgeConfig);

            var enumRuntimeCall = new EnumRuntimeCall();
            enumRuntimeCall.Create(RuntimeCall.AwesomeAvatars, enumCall);

            return enumRuntimeCall;
        }
    }
}
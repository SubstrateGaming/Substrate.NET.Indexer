using Substrate.Bajun.NET.NetApiExt.Generated.Model.bajun_runtime;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.frame_support.traits.tokens.misc;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.sp_core.crypto;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System;

namespace Substrate.Integration.Model.FrameSystem
{
    /// <summary>
    /// Enum Runtime Event C# Wrapper
    /// </summary>
    public class EnumRuntimeEventSharp
    {
        /// <summary>
        /// Enum Runtime Event
        /// </summary>
        /// <param name="enumRuntimeEvent"></param>
        public EnumRuntimeEventSharp(EnumRuntimeEvent enumRuntimeEvent)
        {
            Module = enumRuntimeEvent.Value;
            switch (enumRuntimeEvent.Value)
            {
                case RuntimeEvent.System:
                case RuntimeEvent.ParachainSystem:
                case RuntimeEvent.Multisig:
                case RuntimeEvent.Utility:
                case RuntimeEvent.Identity:
                case RuntimeEvent.Proxy:
                case RuntimeEvent.Scheduler:
                case RuntimeEvent.Preimage:
                case RuntimeEvent.Migrations:
                case RuntimeEvent.Balances:
                case RuntimeEvent.TransactionPayment:
                case RuntimeEvent.Vesting:
                case RuntimeEvent.CollatorSelection:
                case RuntimeEvent.Session:
                case RuntimeEvent.XcmpQueue:
                case RuntimeEvent.PolkadotXcm:
                case RuntimeEvent.CumulusXcm:
                case RuntimeEvent.MessageQueue:
                case RuntimeEvent.XTokens:
                case RuntimeEvent.OrmlXcm:
                case RuntimeEvent.Sudo:
                case RuntimeEvent.Treasury:
                case RuntimeEvent.Council:
                case RuntimeEvent.TechnicalCommittee:
                case RuntimeEvent.Democracy:
                case RuntimeEvent.AwesomeAvatars:
                case RuntimeEvent.Nft:
                case RuntimeEvent.NftTransfer:
                case RuntimeEvent.AffiliatesAAA:
                case RuntimeEvent.TournamentAAA:
                case RuntimeEvent.Assets:
                case RuntimeEvent.AssetRegistry:
                case RuntimeEvent.CouncilMembership:
                case RuntimeEvent.TechnicalCommitteeMembership:
                    var eventXYZ = enumRuntimeEvent.Value2;
                    EventName = eventXYZ.GetType().GetProperty("Value")?.GetValue(eventXYZ, null)?.ToString();
                    ParameterType = eventXYZ.GetType().GetProperty("Value2")?.GetValue(eventXYZ, null)?.GetType();
                    ParameterBytes = (eventXYZ as BaseType)?.Bytes;
                    break;

                default:
                    throw new NotImplementedException($"RuntimeEvent '{enumRuntimeEvent.Value}' not implemented!");
            }
        }

        /// <summary>
        /// Module
        /// </summary>
        public RuntimeEvent Module { get; }

        /// <summary>
        /// Event Name
        /// </summary>
        public string? EventName { get; }

        /// <summary>
        /// Parameter Type
        /// </summary>
        public Type? ParameterType { get; }

        /// <summary>
        /// Parameter Bytes
        /// </summary>
        public byte[]? ParameterBytes { get; }
    }
}
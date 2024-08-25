using Substrate.Bajun.NET.NetApiExt.Generated.Model.frame_system;
using Substrate.NetApi.Model.Types.Primitive;
using System;

namespace Substrate.Integration.Model.FrameSystem
{
    /// <summary>
    /// Enum Phase C# Wrapper
    /// </summary>
    public class EnumPhaseSharp
    {
        /// <summary>
        /// Enum Phase
        /// </summary>
        /// <param name="enumPhase"></param>
        public EnumPhaseSharp(EnumPhase enumPhase)
        {
            Phase = enumPhase.Value;
            switch(enumPhase.Value)
            {
                case Phase.ApplyExtrinsic:
                    Value = ((U32)enumPhase.Value2).Value;
                    break;
                case Phase.Initialization:
                    Value = null;
                    break;
                case Phase.Finalization:
                    Value = null;
                    break;
                default:
                    throw new NotImplementedException($"Phase '{enumPhase.Value}' not implemented!");
            }
        }

        /// <summary>
        /// Phase
        /// </summary>
        public Phase Phase { get; }

        /// <summary>
        /// Value
        /// </summary>
        public uint? Value { get; }
    }
}
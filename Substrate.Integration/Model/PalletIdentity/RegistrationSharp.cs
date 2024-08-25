using Substrate.Bajun.NET.NetApiExt.Generated.Model.bounded_collections.bounded_vec;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_balances.types;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_identity.legacy;
using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_identity.types;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// Registration C# Wrapper
    /// </summary>
    public class RegistrationSharp
    {
        /// <summary>
        /// Registration Constructor
        /// </summary>
        /// <param name="registration"></param>
        public RegistrationSharp(Registration registration)
        {
            Judgements = null; // TODO
            Deposit = registration.Deposit.Value;
            IdentityInfo = new IdentityInfoSharp(registration.Info);
        }

        /// <summary>
        /// Judgements
        /// </summary>
        public BaseTuple<U32, EnumJudgement>[] Judgements { get; }

        /// <summary>
        /// Deposit
        /// </summary>
        public BigInteger Deposit { get; }

        /// <summary>
        /// Identity Info
        /// </summary>
        public IdentityInfoSharp IdentityInfo { get; }
    }
}
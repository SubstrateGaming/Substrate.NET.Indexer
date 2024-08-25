using Substrate.Bajun.NET.NetApiExt.Generated.Model.pallet_identity.types;
using Substrate.Bajun.NET.NetApiExt.Generated.Types.Base;
using System;
using System.Linq;
using System.Text;

namespace Substrate.Integration.Model
{
    /// <summary>
    /// EnumDataSharp C# Wrapper
    /// </summary>
    public class EnumDataSharp
    {
        /// <summary>
        /// EnumDataSharp Constructor
        /// </summary>
        /// <param name="enumData"></param>
        public EnumDataSharp(EnumData enumData)
        {
            switch (enumData.Value)
            {
                case Data.None:
                    Value = null;
                    break;

                case Data.Raw0:
                    Value = ((Arr0U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw1:
                    Value = ((Arr1U8)enumData.Value2).Value.Select(p => p.Value).ToArray();

                    break;

                case Data.Raw2:
                    Value = ((Arr2U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw3:
                    Value = ((Arr3U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw4:
                    Value = ((Arr4U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw5:
                    Value = ((Arr5U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw6:
                    Value = ((Arr6U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw7:
                    Value = ((Arr7U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw8:
                    Value = ((Arr8U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw9:
                    Value = ((Arr9U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw10:
                    Value = ((Arr10U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw11:
                    Value = ((Arr11U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw12:
                    Value = ((Arr12U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw13:
                    Value = ((Arr13U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw14:
                    Value = ((Arr14U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw15:
                    Value = ((Arr15U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw16:
                    Value = ((Arr16U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw17:
                    Value = ((Arr17U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw18:
                    Value = ((Arr18U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw19:
                    Value = ((Arr19U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw20:
                    Value = ((Arr20U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw21:
                    Value = ((Arr21U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw22:
                    Value = ((Arr22U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw23:
                    Value = ((Arr23U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw24:
                    Value = ((Arr24U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw25:
                    Value = ((Arr25U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw26:
                    Value = ((Arr26U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw27:
                    Value = ((Arr27U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw28:
                    Value = ((Arr28U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw29:
                    Value = ((Arr29U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw30:
                    Value = ((Arr30U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw31:
                    Value = ((Arr31U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Raw32:
                    Value = ((Arr32U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.BlakeTwo256:
                    Value = ((Arr32U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Sha256:
                    Value = ((Arr32U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.Keccak256:
                    Value = ((Arr32U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                case Data.ShaThree256:
                    Value = ((Arr32U8)enumData.Value2).Value.Select(p => p.Value).ToArray();
                    break;

                default:
                    throw new Exception("Invalid EnumData");
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        public byte[]? Value { get; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value != null ? Encoding.UTF8.GetString(Value) : "";
        }
    }
}
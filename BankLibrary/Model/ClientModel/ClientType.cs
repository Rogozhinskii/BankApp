using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BankLibrary.Model.ClientModel
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ClientType
    {
        [EnumMember(Value = "Regular")]
        Regular,
        [EnumMember(Value = "Special")]
        Special
    }
}

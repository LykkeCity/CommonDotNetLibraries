using System;
using System.Net;
using Newtonsoft.Json;

namespace Lykke.Common.Log.Converters
{
    /// <summary>
    /// Json converter for <see cref="IPAddress"/>.
    /// </summary>
    public class IPAddressConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IPAddress);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return IPAddress.Parse((string) reader.Value);
        }
    }
}

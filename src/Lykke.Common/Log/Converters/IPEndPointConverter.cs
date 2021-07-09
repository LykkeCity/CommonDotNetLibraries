using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lykke.Common.Log.Converters
{
    /// <summary>
    /// Json converter for <see cref="IPEndPoint"/>.
    /// </summary>
    public class IPEndPointConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IPEndPoint);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ipEndPoint = (IPEndPoint) value;

            var jObject = new JObject
            {
                {"Address", JToken.FromObject(ipEndPoint.Address, serializer)},
                {"Port", ipEndPoint.Port}
            };

            jObject.WriteTo(writer);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var address = jObject["Address"].ToObject<IPAddress>(serializer);

            var port = (int) jObject["Port"];

            return new IPEndPoint(address, port);
        }
    }
}

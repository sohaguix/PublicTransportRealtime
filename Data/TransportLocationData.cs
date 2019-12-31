﻿// <auto-generated />

namespace PublicTransportRealtime.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TransportLocationData
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("speed")]
        public long Speed { get; set; }

        [JsonProperty("direction")]
        public double Direction { get; set; }

        [JsonProperty("sat")]
        public long Sat { get; set; }

        [JsonProperty("board")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Board { get; set; }

        [JsonProperty("rtu_id")]
        public string RtuId { get; set; }

        [JsonProperty("route")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Route { get; set; }
    }

    public partial class TelemetryEventData
    {
        public static TelemetryEventData FromJson(string json) => JsonConvert.DeserializeObject<TelemetryEventData>(json.Split(new[] { "\n\n" }, StringSplitOptions.None).Last(), Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TelemetryEventData self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();

    }
}

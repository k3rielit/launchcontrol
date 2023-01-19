using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Drawing;
using LibLCV;

namespace LibLCV {
    // Deserialization and serialization
    internal static class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Formatting = Formatting.Indented,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    // zero/nonzero - false/true
    internal class IntToBoolConverter : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Null) return false;
            var value = serializer.Deserialize<string>(reader);
            if(int.TryParse(value, out int i)) return i > 0;
            return false;
        }
        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer) {
            if(untypedValue == null) serializer.Serialize(writer, false);
            else if(untypedValue is bool boolean) serializer.Serialize(writer, (boolean ? 1 : 0).ToString()); // serialization not tested
        }
        public static readonly IntToBoolConverter Singleton = new();
    }

    // Extract a number from any string by discarding non-digit characters
    internal class IntExtract : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Null) return 0;
            string raw = serializer.Deserialize<string>(reader) ?? string.Empty, extracted = "";
            foreach(char c in raw) extracted += char.IsDigit(c) ? c : string.Empty;
            if(int.TryParse(extracted, out int i)) return i;
            return 0;
        }
        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer) {
            if(untypedValue == null) serializer.Serialize(writer, false);
            else if(untypedValue is int integer) serializer.Serialize(writer, integer.ToString()); // serialization not tested
        }
        public static readonly IntExtract Singleton = new();
    }

    // For cases where a value can be a number or false
    // Keeping the number representing the boolean as 0
    internal class BoolOrIntToInt : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Null) return 0;
            var value = serializer.Deserialize<string>(reader) ?? string.Empty;
            if(value == "false") return 0;
            else if(int.TryParse(value, out int i)) return i;
            return 0;
        }
        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer) {
            if(untypedValue == null) serializer.Serialize(writer, false);
            else if(untypedValue is int integer) serializer.Serialize(writer, integer.ToString()); // serialization not tested
        }
        public static readonly BoolOrIntToInt Singleton = new();
    }

    // "minute:seconds.milliseconds" - TimeStamp
    internal class TimeStampParser : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(TimeSpan) || t == typeof(TimeSpan?);
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Null) return TimeSpan.Zero;
            var value = serializer.Deserialize<string>(reader) ?? string.Empty;
            if(TimeSpan.TryParseExact(value, @"mm\:ss\.fff", null, out TimeSpan ts)) return ts;
            return TimeSpan.Zero;
        }
        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer) {
            if(untypedValue == null) serializer.Serialize(writer, "00:00.000");
            else if(untypedValue is TimeSpan ts) serializer.Serialize(writer, ts.ToString(@"mm\:ss\.fff")); // serialization not tested
        }
        public static readonly TimeStampParser Singleton = new();
    }

    // "ffffff" - Color
    internal class MMHexColor : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(Color) || t == typeof(Color?);
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer) {
            Color result = Color.White;
            if(reader.TokenType == JsonToken.Null) return result;
            var value = serializer.Deserialize<string>(reader) ?? string.Empty;
            return value.MMColor();
        }
        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer) {
            if(untypedValue == null) serializer.Serialize(writer, "ffffff");
            else if(untypedValue is Color color) serializer.Serialize(writer, color.MMHexString());
        }
        public static readonly MMHexColor Singleton = new();
    }
}

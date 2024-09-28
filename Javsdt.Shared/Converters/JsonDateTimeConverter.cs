using System.Text.Json;
using System.Text.Json.Serialization;

namespace Javsdt.Shared.Converters
{
    public class JsonDateTimeConverter : JsonConverter<DateTime?>
    {
        private readonly string _format = "yyyy-MM-dd";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String 
                && DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(_format));
        }
    }
}

using System.Text.Json.Serialization;
using System.Text.Json;
using Javsdt.Shared.Enums;

namespace Javsdt.Shared.Converters
{
    public class JavTypeConverter : JsonConverter<JavType>
    {
        public override JavType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (Enum.TryParse(typeof(JavType), value, out var result))
            {
                return (JavType)result;
            }
            throw new JsonException($"Unable to convert \"{value}\" to {nameof(JavType)}");
        }

        public override void Write(Utf8JsonWriter writer, JavType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}

using Javsdt.Shared.Enums;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Javsdt.Shared.Converters
{
    public class ClassifyOperationTypeConverter : JsonConverter<ClassifyOperationType>
    {
        public override ClassifyOperationType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out int intValue))
            {
                return (ClassifyOperationType)intValue;
            }
            throw new JsonException("Invalid value for ClassifyOperationType");
        }

        public override void Write(Utf8JsonWriter writer, ClassifyOperationType value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)value);
        }
    }
}

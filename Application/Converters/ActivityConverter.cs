using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CfpService.Application.Convertors
{
    public class ActivityConverter : JsonConverter<Activity>
    {
        public override Activity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Unexpected token type: {reader.TokenType}");
            }
            string value = reader.GetString();
            if (Enum.TryParse<Activity>(value, true, out Activity result))
            {
                return result;
            }

            throw new JsonException($"Unable to parse '{value}' to {typeof(Activity).Name}");
        }

        public override void Write(Utf8JsonWriter writer, Activity value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}

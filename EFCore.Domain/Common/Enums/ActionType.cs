using System.Text.Json.Serialization;

namespace EFCore.Domain.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActionType
{
    Insert,
    Update,
    Delete
}

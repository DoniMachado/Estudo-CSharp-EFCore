using System.Text.Json.Serialization;

namespace EFCore.Domain.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusCommand
{
    Ok,
    Error,
    Forbidden,
    NotFound,
    Exception
}

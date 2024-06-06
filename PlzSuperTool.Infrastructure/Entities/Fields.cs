using System.Text.Json.Serialization;

namespace PlzSuperTool.Infrastructure.Entities;

public class Fields
{
    [JsonPropertyName("plz_code")]
    public string? PlzCode { get; set; }
}
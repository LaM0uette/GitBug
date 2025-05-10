using System.Text.Json.Serialization;

namespace GitBug.Shared;

public readonly record struct ContributionDTO
{
    [JsonPropertyName("date")]
    public required DateTime Date { get; init; }

    [JsonPropertyName("count")]
    public required int Count { get; init; }

    [JsonPropertyName("level")]
    public required int Level { get; init; }
}
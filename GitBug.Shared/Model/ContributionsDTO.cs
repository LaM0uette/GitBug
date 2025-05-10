using System.Text.Json.Serialization;

namespace GitBug.Shared;

public readonly record struct ContributionsDTO
{
    [JsonPropertyName("total")]
    public required IReadOnlyDictionary<int, int> TotalByYears { get; init; }

    [JsonPropertyName("contributions")]
    public required IReadOnlyList<ContributionDTO> Contributions { get; init; }
}
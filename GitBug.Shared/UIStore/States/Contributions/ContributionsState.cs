using MGA.UniFlux;

namespace GitBug.Shared;

public record ContributionsState(string UserName, IReadOnlyDictionary<int, int> TotalByYears, IReadOnlyList<ContributionDTO> Contributions) : ImmutableState
{
    public IReadOnlyList<int> AvailableYears()
    {
        List<int> years = TotalByYears.Keys.ToList();
        years.Sort();
        years.Reverse();
        
        return years;
    }
}
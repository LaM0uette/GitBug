using MGA.UniFlux;

namespace GitBug.Shared;

public record SetContributionsAction(string UserName, int Year, IReadOnlyDictionary<int, int> TotalByYears, IReadOnlyList<ContributionDTO> Contributions) : ImmutableAction<ContributionsState>
{
    public override ContributionsState Reduce(ContributionsState state)
    {
        return new ContributionsState(UserName, Year, TotalByYears, Contributions);
    }
}
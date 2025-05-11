using MGA.UniFlux;

namespace GitBug.Shared;

public record SetContributionsAction(string UserName, IReadOnlyDictionary<int, int> TotalByYears, IReadOnlyList<ContributionDTO> Contributions) : ImmutableAction<ContributionsState>
{
    public override ContributionsState Reduce(ContributionsState state)
    {
        return new ContributionsState(UserName, TotalByYears, Contributions);
    }
}
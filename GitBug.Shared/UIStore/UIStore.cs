using MGA.UniFlux;

namespace GitBug.Shared;

public class UIStore : Store
{
    public UIStore()
    {
        States.Add(new ContributionsState(string.Empty, new Dictionary<int, int>(), new List<ContributionDTO>()));
    }
}
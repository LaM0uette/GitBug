using MGA.UniFlux;

namespace GitBug.Shared;

public class UIStore : Store
{
    public UIStore()
    {
        States.Add(new ContributionsState(string.Empty, 0, new Dictionary<int, int>(), new List<ContributionDTO>()));
    }
}
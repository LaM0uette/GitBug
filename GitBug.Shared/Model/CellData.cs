namespace GitBug.Shared;

public class CellData
{
    public DateTime Date { get; init; }
    public int ContributionCount { get; init; }
    public int Level { get; init; }

    public CellData(DateTime date, int contributionCount, int level)
    {
        Date = date;
        ContributionCount = contributionCount;
        Level = level;
    }
}
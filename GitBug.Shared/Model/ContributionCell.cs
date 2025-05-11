namespace GitBug.Shared;

public class ContributionCell
{
    #region Statements

    public DateTime Date { get; init; }
    public int Count { get; init; }
    public int Level { get; init; }

    public ContributionCell(DateTime date, int count, int level)
    {
        Date = date;
        Count = count;
        Level = level;
    }

    #endregion

    #region Methods

    public bool IsBug => Level is 3 or 4;

    #endregion
}
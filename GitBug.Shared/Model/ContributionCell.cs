namespace GitBug.Shared;

public class ContributionCell
{
    #region Statements

    public DateTime Date { get; init; }
    public int Count { get; init; }
    public int Level { get; init; }
    public ContributionCellState State { get; private set; }
    public int BugCount { get; private set; } = 0;

    public ContributionCell(DateTime date, int count, int level)
    {
        Date = date;
        Count = count;
        Level = level;
        
        State = ContributionCellState.Empty;
    }

    #endregion

    #region Methods

    public void SetState(ContributionCellState state, int bugCount = 0)
    {
        State = state;
        BugCount = bugCount;
    }

    #endregion
}
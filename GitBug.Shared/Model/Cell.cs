namespace GitBug.Shared;

public class Cell
{
    public DateTime Date { get; init; }
    public int Level { get; init; }

    public Cell(DateTime date, int level)
    {
        Date = date;
        Level = level;
    }
}
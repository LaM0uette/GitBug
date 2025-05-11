using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class GameBase : ComponentBase
{
    #region Statements

    [Parameter] public string UserName { get; set; } = string.Empty;
    [Parameter] public int SelectedYear { get; set; }
    
    protected Cell[,] Grid = null!;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;
    [Inject] private UIStore _uiStore { get; set; } = null!;
    
    private IReadOnlyDictionary<int, int> _totalByYears = null!;
    private IReadOnlyList<ContributionDTO> _contributions = null!;
    
    protected override void OnInitialized()
    {
        var contributionsState = _uiStore.GetState<ContributionsState>();
        
        if (string.IsNullOrWhiteSpace(UserName))
            UserName = contributionsState.UserName;
        
        if (SelectedYear == 0)
            SelectedYear = contributionsState.Year;
        
        _totalByYears = contributionsState.TotalByYears;
        _contributions = contributionsState.Contributions;
        
        CreateNewGame();
    }

    #endregion

    #region Methods

    private void CreateNewGame()
    {
        List<ContributionDTO> filteredContributions = _contributions.Where(c => c.Date.Year == SelectedYear).ToList();
        if (filteredContributions.Count == 0)
            return;

        var startDate = new DateTime(SelectedYear, 1, 1);
        var endDate = new DateTime(SelectedYear, 12, 31);
        DateTime startSunday = startDate.AddDays(-(int)startDate.DayOfWeek);

        int totalDays = (endDate - startSunday).Days + 1;
        var totalWeeks = (int)Math.Ceiling(totalDays / 7f);

        Grid = new Cell[totalWeeks, 7];
        Dictionary<DateTime, ContributionDTO> contributions = filteredContributions.ToDictionary(c => c.Date.Date);

        for (var week = 0; week < totalWeeks; week++)
        {
            for (var day = 0; day < 7; day++)
            {
                DateTime date = startSunday.AddDays(week * 7 + day);
                contributions.TryGetValue(date, out ContributionDTO contribution);
                Grid[week, day] = new Cell(date, contribution.Level);
            }
        }
    }

    #endregion
}
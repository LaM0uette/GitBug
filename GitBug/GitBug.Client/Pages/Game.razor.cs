using System.Text.Json;
using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class GameBase : ComponentBase
{
    #region Statements

    [Parameter] public string UserName { get; set; } = string.Empty;
    [Parameter] public int SelectedYear { get; set; }
    
    protected ContributionCell[,]? Grid;
    protected int TotalBugCount;
    protected int BugResolved;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;
    [Inject] private UIStore _uiStore { get; set; } = null!;
    
    private IReadOnlyDictionary<int, int>? _totalByYears;
    private IReadOnlyList<ContributionDTO>? _contributions;
    
    protected override async Task OnInitializedAsync()
    {
        await FetchData();
        CreateNewGame();
    }

    #endregion

    #region Methods

    private void CreateNewGame()
    {
        if (_totalByYears == null || _contributions == null)
            return;
        
        List<ContributionDTO> filteredContributions = _contributions.Where(c => c.Date.Year == SelectedYear).ToList();
        if (filteredContributions.Count == 0)
            return;

        var startDate = new DateTime(SelectedYear, 1, 1);
        var endDate = new DateTime(SelectedYear, 12, 31);
        DateTime startSunday = startDate.AddDays(-(int)startDate.DayOfWeek);

        int totalDays = (endDate - startSunday).Days + 1;
        var totalWeeks = (int)Math.Ceiling(totalDays / 7f);

        Grid = new ContributionCell[totalWeeks, 7];
        Dictionary<DateTime, ContributionDTO> contributions = filteredContributions.ToDictionary(c => c.Date.Date);

        for (var week = 0; week < totalWeeks; week++)
        {
            for (var day = 0; day < 7; day++)
            {
                DateTime date = startSunday.AddDays(week * 7 + day);
                contributions.TryGetValue(date, out ContributionDTO contribution);
                Grid[week, day] = new ContributionCell(date, contribution.Count, contribution.Level);
            }
        }
        
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                ContributionCell cell = Grid[x, y];
                if (cell.Level >= 3)
                    cell.SetState(ContributionCellState.Bug);
            }
        }

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                ContributionCell cell = Grid[x, y];

                if (cell.State == ContributionCellState.Bug)
                    continue;

                var bugCount = 0;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx < 0 || nx >= width || ny < 0 || ny >= height) 
                            continue;
                        
                        if (Grid[nx, ny].State == ContributionCellState.Bug)
                            bugCount++;
                    }
                }

                if (bugCount == 0)
                    cell.SetState(ContributionCellState.Tested);
                else
                    cell.SetState(ContributionCellState.Resolved, bugCount);
            }
        }

        TotalBugCount = Grid.Cast<ContributionCell>().Count(cell => cell.State == ContributionCellState.Bug);
    }
    
    private async Task FetchData()
    {
        if (string.IsNullOrWhiteSpace(UserName))
            return;
        
        var url = $"https://github-contributions-api.jogruber.de/v4/{UserName}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.UserAgent.ParseAdd("GitBug");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var contributionsData = JsonSerializer.Deserialize<ContributionsDTO>(content);
            
            _uiStore.Dispatch(new SetContributionsAction(UserName, SelectedYear, contributionsData.TotalByYears, contributionsData.Contributions));
            var contributionsState = _uiStore.GetState<ContributionsState>();
            
            _totalByYears = contributionsState.TotalByYears;
            _contributions = contributionsState.Contributions;
        }
        else
        {
            _totalByYears = null;
            _contributions = null;
            
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    #endregion
}
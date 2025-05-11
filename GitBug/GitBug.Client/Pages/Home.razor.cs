using System.Text.Json;
using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class HomeBase : ComponentBase
{
    #region Statements
    
    [Parameter] public string UserName { get; set; } = string.Empty;

    protected IReadOnlyList<int>? AvailableYears;
    protected int SelectedYear;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private UIStore _uiStore { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(UserName))
            return;

        await FetchData();
    }

    #endregion

    #region Methods
    
    protected async Task OnValueChanged(string username)
    {
        UserName = username;
        await FetchData();
    }
    
    protected async Task OnValidated(string username)
    {
        UserName = username;
        await FetchData();
    }
    
    protected void OnYearChanged(int year)
    {
        SelectedYear = year;
        StateHasChanged();
    }

    protected void OnStartGameButtonClicked()
    {
        _navigationManager.NavigateTo($"/Game/{UserName}");
    }
    
    
    private async Task FetchData()
    {
        var url = $"https://github-contributions-api.jogruber.de/v4/{UserName}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.UserAgent.ParseAdd("GitBug");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var contributionsData = JsonSerializer.Deserialize<ContributionsDTO>(content);
            
            _uiStore.Dispatch(new SetContributionsAction(UserName, contributionsData.TotalByYears, contributionsData.Contributions));
            var contributionsState = _uiStore.GetState<ContributionsState>();
            
            AvailableYears = contributionsState.AvailableYears();
        }
        else
        {
            AvailableYears = [];
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    #endregion
}

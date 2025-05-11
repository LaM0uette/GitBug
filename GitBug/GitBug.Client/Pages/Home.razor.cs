using System.Text.Json;
using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class HomeBase : ComponentBase
{
    #region Statements
    
    [Parameter] public string UserName { get; set; } = string.Empty;

    protected List<int>? AvailableYears;
    protected int SelectedYear;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;

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
        Console.WriteLine($"Selected year: {SelectedYear}");
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
            var contributions = JsonSerializer.Deserialize<ContributionsDTO>(content);
            
            List<int> years = contributions.TotalByYears.Keys.ToList();
            years.Sort();
            AvailableYears = years;
        }
        else
        {
            AvailableYears = [];
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    #endregion
}

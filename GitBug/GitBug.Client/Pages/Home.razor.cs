using System.Text.Json;
using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class HomeBase : ComponentBase
{
    #region Statements

    protected List<int>? AvailableYears;
    protected int SelectedYear;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;

    #endregion

    #region Methods
    
    protected async Task OnValueChanged(string username)
    {
        await FetchData(username);
    }
    
    protected async Task OnValidated(string username)
    {
        await FetchData(username);
    }
    
    protected void OnYearChanged(int year)
    {
        SelectedYear = year;
        StateHasChanged();
        
        Console.WriteLine($"Selected year: {year}");
    }
    
    
    private async Task FetchData(string username)
    {
        var url = $"https://github-contributions-api.jogruber.de/v4/{username}";
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

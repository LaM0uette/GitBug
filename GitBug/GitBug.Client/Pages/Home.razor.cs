using System.Text.Json;
using GitBug.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GitBug.Client;

public class HomeBase : ComponentBase
{
    #region Statements

    [Parameter] public string Value { get; set; } = string.Empty;
    
    protected List<int>? AvailableYears;
    protected int SelectedYear;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;

    #endregion

    #region Methods
    
    protected void OnInputChanged(ChangeEventArgs eventArgs)
    {
        if (eventArgs.Value is string inputString)
            Value = inputString;
    }
    
    protected async Task HandleBlur()
    {
        if (string.IsNullOrWhiteSpace(Value))
            return;

        await FetchData();
    }
    
    protected async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            if (string.IsNullOrWhiteSpace(Value))
                return;

            await FetchData();
        }
    }

    
    private async Task FetchData()
    {
        string username = Value;
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

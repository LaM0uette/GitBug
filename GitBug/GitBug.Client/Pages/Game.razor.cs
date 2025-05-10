using System.Text.Json;
using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class GameBase : ComponentBase
{
    [Inject] private HttpClient _httpClient { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        var username = "LaM0uette";
        var url = $"https://github-contributions-api.jogruber.de/v4/{username}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.UserAgent.ParseAdd("GitBug");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ContributionsDTO>(content);
            
            foreach (KeyValuePair<int, int> entry in data.TotalByYears)
                Console.WriteLine($"Year {entry.Key}: {entry.Value}");
        
            Console.WriteLine($"\nFirst contribution - Date: {data.Contributions[0].Date:M/d/yyyy}, Count: {data.Contributions[0].Count}");
            Console.WriteLine($"\nFirst contribution - Date: {data.Contributions[1].Date:M/d/yyyy}, Count: {data.Contributions[1].Count}");
            Console.WriteLine($"\nFirst contribution - Date: {data.Contributions[2].Date:M/d/yyyy}, Count: {data.Contributions[2].Count}");
        }
    }
}
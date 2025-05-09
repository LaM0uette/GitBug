using Microsoft.AspNetCore.Components;

namespace GitBug.Client.Pages;

public class HomeBase : ComponentBase
{
    [Inject] private HttpClient _httpClient { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        var url = $"https://github-contributions-api.jogruber.de/v4/LaM0uette";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.UserAgent.ParseAdd("BlazorApp");

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
}
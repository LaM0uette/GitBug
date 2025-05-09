using System.Text.Json;
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
            
            ContributionData data = JsonSerializer.Deserialize<ContributionData>(content) 
                ?? throw new JsonException("Failed to deserialize JSON response");; 
        
            Console.WriteLine("Totaux par année :");
            foreach (KeyValuePair<string, int> entry in data.total)
            {
                Console.WriteLine($"Année {entry.Key}: {entry.Value}");
            }
        
            // Afficher les contributions
            Console.WriteLine("\nContributions :");
            foreach (Contribution contribution in data.contributions)
            {
                Console.WriteLine($"Date: {contribution.date}, Nombre: {contribution.count}, Niveau: {contribution.level}");
            }
        
            // Exemple d'accès à une contribution spécifique
            Console.WriteLine($"\nPremière contribution - Date: {data.contributions[0].date}, Nombre: {data.contributions[0].count}");
        }
    }
}

public class Contribution
{
    public string date { get; set; }
    public int count { get; set; }
    public int level { get; set; }
}

public class ContributionData
{
    public Dictionary<string, int> total { get; set; }
    public List<Contribution> contributions { get; set; }
}
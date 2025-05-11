using GitBug.Shared;
using Microsoft.AspNetCore.Components;

namespace GitBug.Client;

public class GameBase : ComponentBase
{
    #region Statements

    [Parameter] public string UserName { get; set; } = string.Empty;
    
    [Inject] private HttpClient _httpClient { get; set; } = null!;
    [Inject] private UIStore _uiStore { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            var contributionsState = _uiStore.GetState<ContributionsState>();
            UserName = contributionsState.UserName;
        }
    }

    #endregion
}
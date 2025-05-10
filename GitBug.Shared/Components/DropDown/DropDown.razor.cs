using Microsoft.AspNetCore.Components;

namespace GitBug.Shared;

public class DropDownBase : ComponentBase
{
    #region Statements

    [Parameter] public EventCallback<int> SelectedChanged { get; set; }
    
    [Parameter] public string Label { get; set; } = string.Empty;
    
    [Parameter] public List<int>? Options { get; set; }
    [Parameter] public int Selected { get; set; }
    [Parameter] public string Width { get; set; } = "auto";
    [Parameter] public string Height { get; set; } = "33px";

    #endregion

    #region Methods
    
    protected async Task OnSelectedChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Selected = selectedValue;
            await SelectedChanged.InvokeAsync(selectedValue);
        }
    }
    
    #endregion
}
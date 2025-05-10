using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GitBug.Shared;

public class InputFieldBase : ComponentBase
{
    #region Statements

    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public EventCallback<string> Validated { get; set; }
    
    [Parameter] public string Label { get; set; } = string.Empty;
    
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string Width { get; set; } = "auto";
    [Parameter] public string Height { get; set; } = "33px";

    #endregion

    #region Methods
    
    protected async Task HandleBlur()
    {
        if (string.IsNullOrWhiteSpace(Value))
            return;

        await Validated.InvokeAsync(Value);
    }
    
    protected async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            if (string.IsNullOrWhiteSpace(Value))
                return;

            await Validated.InvokeAsync(Value);
        }
    }

    protected async Task OnInputChanged(ChangeEventArgs eventArgs)
    {
        if (eventArgs.Value is string inputString)
        {
            Value = inputString;
            await ValueChanged.InvokeAsync(inputString);
        }
    }
    
    protected async Task ClearInput()
    {
        Value = string.Empty;
        await OnInputChanged(new ChangeEventArgs { Value = string.Empty });
    }
    
    #endregion
}
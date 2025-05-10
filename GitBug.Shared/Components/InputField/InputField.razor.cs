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
    
    protected void HandleBlur()
    {
        if (string.IsNullOrWhiteSpace(Value))
            return;

        Validated.InvokeAsync(Value);
    }
    
    protected void HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            if (string.IsNullOrWhiteSpace(Value))
                return;

            Validated.InvokeAsync(Value);
        }
    }

    protected void OnInputChanged(ChangeEventArgs eventArgs)
    {
        if (eventArgs.Value is string inputString)
        {
            Value = inputString;
            ValueChanged.InvokeAsync(inputString);
        }
    }
    
    protected void ClearInput()
    {
        Value = string.Empty;
        OnInputChanged(new ChangeEventArgs { Value = string.Empty });
    }
    
    #endregion
}
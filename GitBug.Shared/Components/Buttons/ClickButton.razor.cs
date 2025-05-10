using Microsoft.AspNetCore.Components;

namespace GitBug.Shared;

public class ClickButtonBase : ComponentBase
{
    #region Statements

    [Parameter] public EventCallback OnPointerUp { get; set; }
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [Parameter] public string Width { get; set; } = "auto";
    [Parameter] public string Height { get; set; } = "33px";
    [Parameter] public string FontSize { get; set; } = "1.5em";

    #endregion
}
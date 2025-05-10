using Microsoft.AspNetCore.Components;

namespace GitBug.Shared;

public class VerticalSpacerBase : ComponentBase
{
    #region Statements

    [Parameter] public double Value { get; set; }

    #endregion
}
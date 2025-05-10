using Microsoft.AspNetCore.Components;

namespace GitBug.Shared;

public class HorizontalSpacerBase : ComponentBase
{
    #region Statements

    [Parameter] public double Value { get; set; }

    #endregion
}
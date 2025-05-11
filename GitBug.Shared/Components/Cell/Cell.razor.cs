using Microsoft.AspNetCore.Components;

namespace GitBug.Shared;

public class CellBase : ComponentBase
{
    #region Statements

    [Parameter, EditorRequired] public required ContributionCell Data { get; set; }
    
    internal CellState State { get; private set; } = CellState.Clickable;

    #endregion
}
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GitBug.Shared;

public class CellBase : ComponentBase
{
    #region Statements

    [Parameter] public EventCallback<CellBase> Revealed { get; set; }
    
    [Parameter, EditorRequired] public required ContributionCell Data { get; set; }
    
    internal CellState State { get; private set; } = CellState.Clickable;

    #endregion

    #region Methods

    protected void OnPointerUp(PointerEventArgs e)
    {
        if (e.Button != 0)
            return;
        
        State = CellState.Revealed;
        Revealed.InvokeAsync(this);
    }

    #endregion
}
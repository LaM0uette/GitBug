namespace MGA.UniFlux;

public class StateBinder
{
    #region Statements

    private readonly Store _store;

    public StateBinder(Store store)
    {
        _store = store;
    }

    #endregion

    #region Methods

    public StateBinder Add<TState>(TState state) where TState : IState
    {
        _store.AddState(state);
        return this;
    }

    public StateBinder Add<TState>(params object[] args) where TState : IState
    {
        var state = (TState)Activator.CreateInstance(typeof(TState), args);
        _store.AddState(state);
        return this;
    }

    #endregion
}
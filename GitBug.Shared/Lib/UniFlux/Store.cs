﻿namespace MGA.UniFlux;

public class Store : IStateStore, ISignalStore
{
    #region Statements
        
    protected StateBinder States { get; }
    protected SignalBinder Signals { get; }

    private readonly Dictionary<Type, IState> _states = new();
    private readonly Dictionary<Type, List<Delegate>> _stateSubscribers = new();
    private readonly Dictionary<Type, List<Delegate>> _signalSubscribers = new();
        
    protected Store()
    {
        States = new StateBinder(this);
        Signals = new SignalBinder(this);
    }

    #endregion

    #region IStateStore
        
    public TState GetState<TState>() where TState : IState
    {
        return (TState)_states[typeof(TState)];
    }

    public void Dispatch<TState>(IAction<TState> action) where TState : IState
    {
        var oldState = GetState<TState>();
        var newState = action.Reduce(oldState);

        _states[typeof(TState)] = newState;

        var listeners = new List<Delegate>(_stateSubscribers[typeof(TState)]);
    
        foreach (var listener in listeners)
            ((Action<TState>)listener)(newState);
    }

    public IDisposable Subscribe<TState>(Action<TState> action) where TState : IState
    {
        _stateSubscribers[typeof(TState)].Add(action);
        return new Unsubscriber(() => _stateSubscribers[typeof(TState)].Remove(action));
    }

    public void Unsubscribe<TState>(Action<TState> action) where TState : IState
    {
        if (_stateSubscribers.ContainsKey(typeof(TState)))
            _stateSubscribers[typeof(TState)].Remove(action);
    }

    #endregion
        
    #region ISignalStore

    public void Emit<TSignal>(TSignal signal) where TSignal : ISignal
    {
        if (!_signalSubscribers.ContainsKey(typeof(TSignal))) return;

        var subscribers = new List<Delegate>(_signalSubscribers[typeof(TSignal)]);

        foreach (var listener in subscribers)
            ((Action<TSignal>)listener)(signal);
    }

    public IDisposable Listen<TSignal>(Action<TSignal> action) where TSignal : ISignal
    {
        _signalSubscribers[typeof(TSignal)].Add(action);
        return new Unsubscriber(() => _signalSubscribers[typeof(TSignal)].Remove(action));
    }

    public void Mute<TSignal>(Action<TSignal> action) where TSignal : ISignal
    {
        if (_signalSubscribers.ContainsKey(typeof(TSignal)))
            _signalSubscribers[typeof(TSignal)].Remove(action);
    }

    #endregion

    #region Methods
        
    internal void AddState<TState>(TState state) where TState : IState
    {
        _states[typeof(TState)] = state;
        
        if (!_stateSubscribers.ContainsKey(typeof(TState)))
            _stateSubscribers[typeof(TState)] = new List<Delegate>();
    }

    internal void AddSignal<TSignal>() where TSignal : ISignal
    {
        if (!_signalSubscribers.ContainsKey(typeof(TSignal)))
            _signalSubscribers[typeof(TSignal)] = new List<Delegate>();
    }

    #endregion
}
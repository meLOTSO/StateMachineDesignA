public delegate void StateChanger<TState>(TState state) where TState : notnull;

public class StateMachineContext<TState, TValue> where TState : notnull
{
    private readonly Dictionary<TState, TValue> _stateValues = new();
    private TState? _currentState;
    private TValue? _currentValue;
    private event EventHandler<StateChangedEventArgs<TState, TValue>>? _onStateChange;

    public IReadOnlyDictionary<TState, TValue> StateValues { get => _stateValues; }
    public TState? CurrentState { get => _currentState; }
    public TValue? CurrentValue { get => _currentValue; }
    public event EventHandler<StateChangedEventArgs<TState, TValue>>? OnStateChange
    {
        add => _onStateChange += value;
        remove => _onStateChange -= value;
    }

    public virtual void SetState(TState state)
    {
        if (_stateValues.TryGetValue(state, out _currentValue))
        {
            _onStateChange?.Invoke(this, new(_currentState!, state, _currentValue));
            _currentState = state;
        }
    }

    public void AddStateChangeHandler(EventHandler<StateChangedEventArgs<TState, TValue>> action)
    {
        _onStateChange += action;
    }

    public void RemoveStateChangeHandler(EventHandler<StateChangedEventArgs<TState, TValue>> action)
    {
        _onStateChange -= action;
    }

    public virtual void Register(TState state, TValue value)
    {
        _stateValues.TryAdd(state, value);
    }

    public virtual void ForEach(Action<TState, TValue> action)
    {
        foreach (var (state, value) in _stateValues)
            action?.Invoke(state, value);
    }

    public virtual void ApplyFor(TState state, Action<TValue> action)
    {
        if (_stateValues.TryGetValue(state, out var value))
            action?.Invoke(value);
    }
}

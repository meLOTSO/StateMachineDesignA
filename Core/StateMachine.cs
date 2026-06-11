public class StateMachine<TState, TValue> where TState : notnull
{
    public StateMachineContext<TState, TValue> Context { get; private set; } = new();
    private volatile bool _isRunning = false;
    private Action<StateMachineContext<TState, TValue>>? _onUse;
    private Dictionary<TState, Action<StateMachine<TState, TValue>>> _onMap = new();

    public StateMachine(StateMachineContext<TState, TValue> context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual void Use(Action<StateMachineContext<TState, TValue>> action)
    {
        _onUse += action;
    }

    public virtual void RemoveUse(Action<StateMachineContext<TState, TValue>> action)
    {
        _onUse -= action;
    }

    public virtual void Map(TState state, Action<StateMachine<TState, TValue>> action)
    {
        if (!_onMap.TryAdd(state, action))
            _onMap[state] += action;
    }

    public virtual void Run()
    {
        if (_isRunning) throw new InvalidOperationException("Already running");
        _isRunning = true;

        while (_isRunning)
        {
            _onUse?.Invoke(this.Context);
            Thread.Sleep(16);
        }
    }

    public virtual void Stop() => _isRunning = false;
}

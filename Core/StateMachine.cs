namespace StateMachine.Core;

public class StateMachine
{
    protected StateMachineContextBuilder Context { get; set; } = new();
    private volatile bool _isRunning = false;

    public RequestDelegate? _useHandlers;
    public StateMachineContext StateMachineContext { get => (StateMachineContext)Context; }

    public StateMachine(StateMachineContextBuilder context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual void Use(RequestDelegate action)
    {
        _useHandlers += action;
    }

    public virtual void Map(string state, RequestDelegate action)
    {
        Context.Append(state, action);
    }

    public virtual void Run(TimeSpan dt)
    {
        if (_isRunning) throw new InvalidOperationException("Already running");
        _isRunning = true;

        while (_isRunning)
        {
            _useHandlers?.Invoke(this.Context);
            Thread.Sleep(dt);
        }
    }

    public virtual void Stop() => _isRunning = false;
}

using StateMachine.Abstracts;

namespace StateMachine.Core;

public class StateMachine
{
    protected volatile bool IsRunning = false;
    protected StateMachineContext MutableContext { get; init; }

    public event RequestDelegate? OnRun;
    public TimeSpan DeltaTime { get; set; } = TimeSpan.FromSeconds(1 / 60);
    public IStateMachineContext<string, RequestDelegate> Context { get => MutableContext; }
    public Dictionary<string, object?> Data { get; } = new();

    public StateMachine(StateMachineContext context)
    {
        MutableContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    // StateMachine: (context) => ...
    public virtual void Use(RequestDelegate action)
    {
        OnRun += action;
    }
    public virtual void Map(string state, RequestDelegate action)
    {
        MutableContext.RegisterState(state);
        MutableContext.Mutate((stateMap) =>
        {
            stateMap[state].Add(action);
        });
    }

    public virtual void Run()
    {
        if (IsRunning) throw new InvalidOperationException("Already running");

        IsRunning = true;

        while (IsRunning)
        {
            OnRun?.Invoke(Context);
            MutableContext.CurrentValues.ForEach((act) => act?.Invoke(Context));
            Thread.Sleep(DeltaTime);
        }
    }
    public virtual void Stop() => IsRunning = false;
}
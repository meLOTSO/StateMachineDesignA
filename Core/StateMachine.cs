using StateMachine.Core.Delegate;
using StateMachine.Abstracts;

namespace StateMachine.Core;

public class StateMachine
{
    protected volatile bool IsRunning = false;
    protected MutableStateMachineContextBase MutableContext { get; init; }

    public event RequestDelegate? OnRun;
    public TimeSpan DeltaTime { get; set; } = TimeSpan.FromSeconds(1 / 60);
    public StateMachineContextBase Context { get => MutableContext; }

    public StateMachine(MutableStateMachineContextBase context)
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
        MutableContext.Mutate((stateValues) =>
        {
            if (!stateValues.TryAdd(state, action))
                stateValues[state] += action;
        });
    }

    public virtual void Run()
    {
        if (IsRunning) throw new InvalidOperationException("Already running");

        IsRunning = true;

        while (IsRunning)
        {
            OnRun?.Invoke(Context);
            MutableContext.CurrentValue?.Invoke(Context);
            Thread.Sleep(DeltaTime);
        }
    }
    public virtual void Stop() => IsRunning = false;
}
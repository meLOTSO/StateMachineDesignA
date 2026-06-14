using StateMachine.Abstracts;

namespace StateMachine.Core;

public abstract class StateMachineBase<TState, TValue> where TState : notnull
{
    protected IMutableStateMachineContext<TState, TValue> MutableContext { get; init; }

    public IStateMachineContext<TState, TValue> Context { get => MutableContext; }
    public Dictionary<string, object?> Data { get; } = [];

    public StateMachineBase(IMutableStateMachineContext<TState, TValue> context)
    {
        MutableContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public abstract void Use(TValue action);
    public abstract void Map(string state, TValue action);

    public abstract void Run();
    public abstract void Stop();
}
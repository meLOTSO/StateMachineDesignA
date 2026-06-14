using StateMachine.Abstracts;
using StateMachine.Delegates;

namespace StateMachine.Abstracts;

public abstract class StateMachineBase
{
    protected IMutableStateMachineContext MutableContext { get; init; }

    public IStateMachineContext Context { get => MutableContext; }
    public Dictionary<string, object?> Data { get; } = [];

    public StateMachineBase(IMutableStateMachineContext context)
    {
        MutableContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public abstract void Use(StateHandler action);
    public abstract void Map(string state, StateHandler action);

    public abstract void Run();
    public abstract void Stop();
}

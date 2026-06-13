using StateMachine.Abstracts;

namespace StateMachine.Abstracts;

public interface IStateMachineContext<TState, TValue> : IStateValueProvider<TState, TValue> where TState : notnull
{
    Dictionary<string, object?> Metadata { get; }

    void SetState(TState state);
}

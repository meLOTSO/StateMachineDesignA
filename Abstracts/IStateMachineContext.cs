namespace StateMachine.Abstracts;

public interface IStateMachineContext<TState, TValue> : IStateValueProvider<TState, TValue> where TState : notnull
{
    Dictionary<string, object?> Metadata { get; }

    bool SetState(TState state);
}

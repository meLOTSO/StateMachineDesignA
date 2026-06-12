using StateMachine.Abstracts;

public interface IStateMachineContext<TState, TValue> : IStateValueProvider<TState, TValue> where TState : notnull
{
    Dictionary<string, object?> Data { get; }
    void SetState(TState state);
}

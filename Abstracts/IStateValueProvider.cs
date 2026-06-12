using StateMachine.Core;

namespace StateMachine.Abstracts;

public interface IStateValueProvider<TState, TValue> where TState : notnull
{
    IReadOnlyDictionary<TState, StateValuesContainer<TValue>> StateValues { get; }
    TState? CurrentState { get; }
    StateValuesContainer<TValue> CurrentValue { get; }

    StateValuesContainer<TValue>? GetValuesCollection(TState state);
    bool TryGetValuesCollection(TState state, out StateValuesContainer<TValue> value);
}

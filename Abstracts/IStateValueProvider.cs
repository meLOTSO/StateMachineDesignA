public interface IStateValueProvider<TState, TValue> where TState : notnull
{
    IReadOnlyDictionary<TState, TValue> StateValues { get; }
    TState? CurrentState { get; }
    TValue? CurrentValue { get; }

    TValue? GetValue(TState state);
    bool TryGetValue(TState state, out TValue value);
}
using StateMachine.Core;

namespace StateMachine.Abstracts;

public interface IStateValueProvider<TState, TValue> where TState : notnull
{
    IReadOnlyDictionary<TState, StateBag<TValue>> StateMap { get; }
    TState CurrentState { get; }
    StateBag<TValue> CurrentBag { get; }
    List<TValue> CurrentValues { get; }
    Dictionary<string, object?> CurrentMetadata { get; }

    StateBag<TValue>? GetBag(TState state);
    List<TValue>? GetValues(TState state);
    Dictionary<string, object?>? GetMetadata(TState state);
    T? GetMetadata<T>(TState state, string key);

    bool TryGetBag(TState state, out StateBag<TValue> value);
    bool TryGetValues(TState state, out List<TValue> value);
    bool TryGetMetadata(TState state, out Dictionary<string, object?> data);
    bool TryGetMetadata<T>(TState state, string key, out T? value);
}

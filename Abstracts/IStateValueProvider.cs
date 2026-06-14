using StateMachine.Core;

namespace StateMachine.Abstracts;

public interface IStateValueProvider<TState, TValue> where TState : notnull
{
    IReadOnlyDictionary<TState, StateBag<TValue>> StateMap { get; }
    TState CurrentState { get; }
    StateBag<TValue> CurrentBag { get; }
    List<TValue> CurrentValues { get; }
    Dictionary<string, object?> CurrentData { get; }

    StateBag<TValue>? GetBag(TState state);
    List<TValue>? GetValues(TState state);
    Dictionary<string, object?>? GetData(TState state);
    T? GetData<T>(TState state, string key);

    bool TryGetBag(TState state, out StateBag<TValue> value);
    bool TryGetValues(TState state, out List<TValue> value);
    bool TryGetData(TState state, out Dictionary<string, object?> data);
    bool TryGetData<T>(TState state, string key, out T? value);
}

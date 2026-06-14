using StateMachine.Abstracts;
using StateMachine.Delegates;

namespace StateMachine.Core;

public class MutableStateMachineContext : IMutableStateMachineContext
{
    private static readonly List<StateHandler> EmptyList = new();
    private static readonly Dictionary<string, object?> EmptyDictionary = new();

    private Dictionary<string, StateBag<StateHandler>> _stateMap { get; } = new();
    private string _currentState = string.Empty;
    private StateBag<StateHandler> _bag = new();
    private bool _autoCreateStates = true;

    public Dictionary<string, object?> Metadata { get; } = new();
    public IReadOnlyDictionary<string, StateBag<StateHandler>> StateMap => _stateMap;
    public string CurrentState => _currentState;
    public StateBag<StateHandler> CurrentBag => _bag;
    public List<StateHandler> CurrentValues => _bag.Values;
    public Dictionary<string, object?> CurrentMetadata => _bag.Metadata;

    public StateBag<StateHandler>? GetBag(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return null;
        return _stateMap.GetValueOrDefault(state);
    }
    public List<StateHandler>? GetValues(string state)
    {
        return GetBag(state)?.Values;
    }
    public Dictionary<string, object?>? GetMetadata(string state)
    {
        return GetBag(state)?.Metadata;
    }
    public T? GetMetadata<T>(string state, string key)
    {
        var data = GetMetadata(state);
        if (data is not null && data.TryGetValue(key, out var obj) && obj is T value)
            return value;
        return default;
    }

    public void Mutate(Action<Dictionary<string, StateBag<StateHandler>>> action)
        => action?.Invoke(_stateMap);

    public bool SetState(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return false;

        if (!_stateMap.TryGetValue(state, out var bag))
        {
            if (!_autoCreateStates)
                throw new InvalidOperationException($"State '{state}' is not registered. Auto-creation is disabled.");

            bag = new StateBag<StateHandler>();
            _stateMap[state] = bag;
        }
        _currentState = state;
        _bag = bag;
        return true;
    }

    public void DisableAutoCreate() => _autoCreateStates = false;
    public void EnableAutoCreate() => _autoCreateStates = true;

    public bool TryGetBag(string state, out StateBag<StateHandler> bag)
    {
        if (string.IsNullOrWhiteSpace(state) || !_stateMap.TryGetValue(state, out var value))
        {
            bag = null!;
            return false;
        }
        bag = value;
        return true;
    }

    public bool TryGetValues(string state, out List<StateHandler> value)
    {
        if (TryGetBag(state, out var bag))
        {
            value = bag.Values;
            return true;
        }
        value = EmptyList;
        return false;
    }

    public bool TryGetMetadata(string state, out Dictionary<string, object?> data)
    {
        if (TryGetBag(state, out var bag))
        {
            data = bag.Metadata;
            return true;
        }
        data = EmptyDictionary;
        return false;
    }
    public bool TryGetMetadata<T>(string state, string key, out T? value)
    {
        if (!TryGetMetadata(state, out var data) || data[key] is not T val)
        {
            value = default;
            return false;
        }
        value = val;
        return true;
    }
}

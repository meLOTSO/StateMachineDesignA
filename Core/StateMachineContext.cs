using StateMachine.Abstracts;
using StateMachine.Delegates;

namespace StateMachine.Core;

public class StateMachineContext : IMutableStateMachineContext<string, RequestDelegate>
{
    private static readonly List<RequestDelegate> EmptyList = new();
    private static readonly Dictionary<string, object?> EmptyDictionary = new();

    private Dictionary<string, StateBag<RequestDelegate>> _stateMap { get; } = new();
    private string _currentState = string.Empty;
    private StateBag<RequestDelegate> _bag = new();
    private bool _autoCreateStates = true;

    public Dictionary<string, object?> Metadata { get; } = new();
    public IReadOnlyDictionary<string, StateBag<RequestDelegate>> StateMap => _stateMap;
    public string CurrentState => _currentState;
    public StateBag<RequestDelegate> CurrentBag => _bag;
    public List<RequestDelegate> CurrentValues => _bag.Values;
    public Dictionary<string, object?> CurrentData => _bag.Data;

    public StateBag<RequestDelegate>? GetBag(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return null;
        return _stateMap.GetValueOrDefault(state);
    }
    public List<RequestDelegate>? GetValues(string state)
    {
        return GetBag(state)?.Values;
    }
    public Dictionary<string, object?>? GetData(string state)
    {
        return GetBag(state)?.Data;
    }
    public T? GetData<T>(string state, string key)
    {
        var data = GetData(state);
        if (data is not null && data.TryGetValue(key, out var obj) && obj is T value)
            return value;
        return default;
    }

    public void Mutate(Action<Dictionary<string, StateBag<RequestDelegate>>> action)
        => action?.Invoke(_stateMap);

    public bool SetState(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return false;

        if (!_stateMap.TryGetValue(state, out var bag))
        {
            if (!_autoCreateStates)
                throw new InvalidOperationException($"State '{state}' is not registered. Auto-creation is disabled.");

            bag = new StateBag<RequestDelegate>();
            _stateMap[state] = bag;
        }
        _currentState = state;
        _bag = bag;
        return true;
    }

    public void DisableAutoCreate() => _autoCreateStates = false;
    public void EnableAutoCreate() => _autoCreateStates = true;

    public bool TryGetBag(string state, out StateBag<RequestDelegate> bag)
    {
        if (string.IsNullOrWhiteSpace(state) || !_stateMap.TryGetValue(state, out var value))
        {
            bag = null!;
            return false;
        }
        bag = value;
        return true;
    }

    public bool TryGetValues(string state, out List<RequestDelegate> value)
    {
        if (TryGetBag(state, out var bag))
        {
            value = bag.Values;
            return true;
        }
        value = EmptyList;
        return false;
    }

    public bool TryGetData(string state, out Dictionary<string, object?> data)
    {
        if (TryGetBag(state, out var bag))
        {
            data = bag.Data;
            return true;
        }
        data = EmptyDictionary;
        return false;
    }
    public bool TryGetData<T>(string state, string key, out T? value)
    {
        if (!TryGetData(state, out var data) || data[key] is not T val)
        {
            value = default;
            return false;
        }
        value = val;
        return true;
    }
}

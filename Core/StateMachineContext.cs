using StateMachine.Abstracts;

namespace StateMachine.Core;

public class StateMachineContext : IMutableStateMachineContext<
    string,
    RequestDelegate,
    Dictionary<string, StateBag<RequestDelegate>>>
{
    protected Dictionary<string, StateBag<RequestDelegate>> StateContainers { get; } = new();
    protected string State = string.Empty;

    public Dictionary<string, object?> Metadata { get; } = new();
    public IReadOnlyDictionary<string, StateBag<RequestDelegate>> StateMap => StateContainers;
    public string CurrentState => State;
    public StateBag<RequestDelegate>? CurrentBag => GetBag(CurrentState);
    public List<RequestDelegate>? CurrentValues => GetValues(CurrentState);
    public Dictionary<string, object?>? CurrentData => GetData(CurrentState);

    public StateBag<RequestDelegate>? GetBag(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return null;
        return StateContainers.GetValueOrDefault(state);
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
        if (GetData(state)?.GetValueOrDefault(key) is T value) return value;
        return default;
    }

    public void Mutate(Action<Dictionary<string, StateBag<RequestDelegate>>> action)
        => action?.Invoke(StateContainers);

    public void SetState(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return;
        State = state;
    }

    public bool TryGetBag(string state, out StateBag<RequestDelegate> bag)
    {
        if (string.IsNullOrWhiteSpace(state) || !StateContainers.TryGetValue(state, out var value))
        {
            bag = new();
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
        value = [];
        return false;
    }

    public bool TryGetData(string state, out Dictionary<string, object?> data)
    {
        if (TryGetBag(state, out var bag))
        {
            data = bag.Data;
            return true;
        }
        data = new();
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

    public bool RegisterState(string state)
    {
        var bag = new StateBag<RequestDelegate>();
        return StateContainers.TryAdd(state, bag);
    }
}

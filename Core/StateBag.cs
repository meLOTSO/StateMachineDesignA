namespace StateMachine.Core;

public class StateBag<TValue>
{
    public List<TValue> Values { get; }
    public Dictionary<string, object?> Data { get; }

    public StateBag()
    {
        Values = new List<TValue>();
        Data = new Dictionary<string, object?>();
    }

    public void Add(TValue value)
    {
        if (value is not null)
            Values.Add(value);
    }

    public void SetData(string key, object? value) => Data[key] = value;
    public T? GetData<T>(string key)
    {
        return Data.TryGetValue(key, out var value) ? (T?)value : default;
    }
}

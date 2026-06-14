namespace StateMachine.Core;

public class StateBag<TValue>
{
    public List<TValue> Values { get; }
    public Dictionary<string, object?> Metadata { get; }

    public StateBag()
    {
        Values = new List<TValue>();
        Metadata = new Dictionary<string, object?>();
    }

    public void Add(TValue value)
    {
        if (value is not null)
            Values.Add(value);
    }

    public void SetMetadata(string key, object? value) => Metadata[key] = value;
    public T? GetMetadata<T>(string key)
    {
        return Metadata.TryGetValue(key, out var value) ? (T?)value : default;
    }
}

namespace StateMachine.Core;

public struct StateValuesContainer<TValue>
{
    public List<TValue> Values { get; }
    public Dictionary<string, object?> Data { get; }
}

public class StateChangedEventArgs<TState, TValue> : EventArgs where TState : notnull
{
    public TState FromState { get; }
    public TState ToState { get; }
    public TValue? Value { get; }
    public DateTime Timestamp { get; }

    public StateChangedEventArgs(TState from, TState to, TValue? value)
    {
        FromState = from;
        ToState = to;
        Value = value;
        Timestamp = DateTime.Now;
    }
}
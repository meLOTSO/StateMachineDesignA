public class StateMachineBuilder<TState, TValue> where TState : notnull
{
    protected StateMachineContext<TState, TValue> Context { get; } = new();

    public virtual void Register(TState state, TValue value)
    {
        this.Context.Register(state, value);
    }

    public StateMachine<TState, TValue> Build()
    {
        return new StateMachine<TState, TValue>(this.Context);
    }
}

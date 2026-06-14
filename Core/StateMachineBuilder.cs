namespace StateMachine.Core;

public class StateMachineBuilder
{
    protected MutableStateMachineContext Context { get; } = new();

    public StateMachine Build()
    {
        return new StateMachine(this.Context);
    }
}

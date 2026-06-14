namespace StateMachine.Core;

public class StateMachineBuilder
{
    protected StateMachineContext Context { get; } = new();

    public StateMachine Build()
    {
        return new StateMachine(this.Context);
    }
}

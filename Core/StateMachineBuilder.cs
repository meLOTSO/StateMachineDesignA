namespace StateMachine.Core;

public class StateMachineBuilder
{
    protected StateMachineContextBuilder Context { get; } = new();

    public StateMachine Build()
    {
        return new StateMachine(this.Context);
    }
}

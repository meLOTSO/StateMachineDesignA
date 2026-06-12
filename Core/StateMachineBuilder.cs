using StateMachine.Abstracts;

namespace StateMachine.Core;

public class StateMachineBuilder
{
    protected StateMachineContext Context { get; }

    public StateMachine Build()
    {
        return new StateMachine(this.Context);
    }
}

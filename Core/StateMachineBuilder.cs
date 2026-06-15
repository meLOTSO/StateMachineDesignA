using StateMachine.Abstracts;
using StateMachine.ServiceCore;

namespace StateMachine.Core;

public class StateMachineBuilder
{
    protected MutableStateMachineContext Context { get; } = new();
    public IServiceCollection Services { get; } = new ServiceCollection();

    public StateMachine Build()
    {
        var serviceProvider = new ServiceProvider(Services);
        return new StateMachine(this.Context, serviceProvider);
    }
}

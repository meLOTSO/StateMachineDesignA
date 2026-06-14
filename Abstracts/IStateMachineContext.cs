using StateMachine.Delegates;

namespace StateMachine.Abstracts;

public interface IStateMachineContext : IStateValueProvider<string, StateHandler>
{
    Dictionary<string, object?> Metadata { get; }

    bool SetState(string state);
}

using StateMachine.Core;
using StateMachine.Delegates;

namespace StateMachine.Abstracts;

public interface IMutableStateMachineContext : IStateMachineContext, IMutable<Dictionary<string, StateBag<StateHandler>>>
{
}

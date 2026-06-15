using StateMachine.Abstracts;

namespace StateMachine.Delegates;

public delegate void StateHandler(IStateMachineContext context, IServiceProvider services);

using StateMachine.Abstracts;

namespace StateMachine.Delegates;

public delegate void StateHandler(IStateMachineContext<string, StateHandler> context);

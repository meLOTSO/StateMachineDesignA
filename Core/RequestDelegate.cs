using StateMachine.Abstracts;

namespace StateMachine.Core;

public delegate void RequestDelegate(IStateMachineContext<string, RequestDelegate> context);

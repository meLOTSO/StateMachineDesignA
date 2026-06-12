namespace StateMachine.Core;

public delegate void RequestDelegate(IStateMachineContext<string, RequestDelegate> context);

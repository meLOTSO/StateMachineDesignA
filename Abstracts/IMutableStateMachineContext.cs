using StateMachine.Core;

namespace StateMachine.Abstracts;

public interface IMutableStateMachineContext<TState, TValue> : IStateMachineContext<TState, TValue>, IMutable<Dictionary<TState, StateBag<TValue>>> where TState : notnull
{
}

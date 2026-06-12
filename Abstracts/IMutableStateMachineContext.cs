public interface IMutableStateMachineContext<TState, TValue, TMutableData> : IStateMachineContext<TState, TValue>, IMutable<TMutableData> where TState : notnull where TMutableData : notnull
{

}
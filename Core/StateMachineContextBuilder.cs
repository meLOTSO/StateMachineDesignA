using StateMachine.Core;

public class StateMachineContextBuilder : StateMachineContext
{
    public void Append(string state, RequestDelegate action)
    {
        if (!stateValues.TryAdd(state, action))
            stateValues[state] += action;
    }
}
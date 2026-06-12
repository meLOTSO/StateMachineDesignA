namespace StateMachine.Core;

using System.Collections.Generic;

public class StateMachineContext : IStateMachineContext<string, RequestDelegate>, IMutable<Dictionary<string, StateValuesCollection<RequestDelegate>>>
{
    protected Dictionary<string, StateValuesCollection<RequestDelegate>> StateValuesDictionary { get; } = new();
    protected string State = string.Empty;
    protected StateValuesCollection<RequestDelegate> Value;

    public Dictionary<string, object?> Data { get; } = new();
    public IReadOnlyDictionary<string, StateValuesCollection<RequestDelegate>> StateValuesData => StateValuesDictionary;
    public string CurrentState => State;
    public StateValuesCollection<RequestDelegate> CurrentValue => Value;

    public List<RequestDelegate>? GetValuesCollection(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            return null;

        return StateValuesDictionary.GetValueOrDefault(state).Values;
    }

    public void Mutate(Action<Dictionary<string, StateValuesCollection<RequestDelegate>>> action)
        => action?.Invoke(StateValuesDictionary);

    public void SetState(string state)
    {
        if (string.IsNullOrWhiteSpace(state)) return;

        if (StateValuesDictionary.TryGetValue(state, out Value))
            State = state;
    }

    public bool TryGetValuesCollection(string state, out List<RequestDelegate> value)
    {
        if (string.IsNullOrWhiteSpace(state))
        {
            value = [];
            return false;
        }
        return StateValuesDictionary.TryGetValue(state, out value);
    }
}

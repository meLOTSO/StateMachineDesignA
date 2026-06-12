namespace StateMachine.Core;

public delegate void StateChanger(string state);
public delegate void RequestDelegate(StateMachineContext context);

public class StateMachineContext : IStateValueProvider<string, RequestDelegate>
{
    private string? _currentState;
    private RequestDelegate? _currentValue;

    protected Dictionary<string, RequestDelegate> stateValues { get; set; } = new();

    public IReadOnlyDictionary<string, RequestDelegate> StateValues { get => stateValues; }
    public string? CurrentState { get => _currentState; }
    public RequestDelegate? CurrentValue { get => _currentValue; }

    public RequestDelegate? GetValue(string state)
    {
        var value = stateValues.GetValueOrDefault(state);
        return value;
    }

    public bool TryGetValue(string state, out RequestDelegate value)
    {
        return stateValues.TryGetValue(state, out value!);
    }

    public virtual void SetState(string state)
    {
        if (stateValues.TryGetValue(state, out _currentValue))
        {
            _currentState = state;
        }
    }
}

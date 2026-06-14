using StateMachine.Abstracts;
using StateMachine.Delegates;

namespace StateMachine.Core;

public class StateMachine : StateMachineBase<string, RequestDelegate>
{
    protected volatile bool IsRunning = false;
    public event RequestDelegate? OnRun;
    public TimeSpan DeltaTime { get; set; } = TimeSpan.FromSeconds(1 / 60);

    public StateMachine(StateMachineContext context) : base(context)
    {
    }

    public override void Use(RequestDelegate action)
    {
        OnRun += action;
    }
    public override void Map(string state, RequestDelegate action)
    {
        MutableContext.Mutate((stateMap) =>
        {
            if (!stateMap.TryGetValue(state, out var bag))
            {
                bag = new StateBag<RequestDelegate>();
                stateMap[state] = bag;
            }
            bag.Add(action);
        });
    }

    public override void Run()
    {
        if (IsRunning) throw new InvalidOperationException("Already running");

        IsRunning = true;

        while (IsRunning)
        {
            OnRun?.Invoke(Context);
            MutableContext?.CurrentValues?.ForEach((act) => act?.Invoke(Context));
            Thread.Sleep(DeltaTime);
        }
    }
    public override void Stop() => IsRunning = false;
}
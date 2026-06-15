using System.Runtime.Serialization;
using StateMachine.Abstracts;
using StateMachine.Delegates;
using StateMachine.Extensions;
using StateMachine.ServiceCore;

namespace StateMachine.Core;

public class StateMachine : StateMachineBase
{
    private IServiceProvider _serviceProvider;
    protected volatile bool IsRunning = false;
    public event StateHandler? OnRun;
    public TimeSpan DeltaTime { get; set; } = TimeSpan.FromSeconds(1 / 60);

    public StateMachine(IMutableStateMachineContext context, IServiceProvider? serviceProvider = null) : base(context)
    {
        if (serviceProvider is null)
            _serviceProvider = new ServiceProvider(new ServiceCollection());
        else
            _serviceProvider = serviceProvider;
    }

    public override void Use(StateHandler action)
    {
        OnRun += action;
    }
    public override void Map(string state, StateHandler action)
    {
        MutableContext.Mutate((stateMap) =>
        {
            if (!stateMap.TryGetValue(state, out var bag))
            {
                bag = new StateBag<StateHandler>();
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

    public void RemoveEmptyStates() => MutableContext.RemoveEmptyStates();
}

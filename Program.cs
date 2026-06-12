using StateMachine.Core;

var builder = new StateMachineBuilder();

var sm = builder.Build();
sm.Context.SetState("menu");
int n = 100;

sm.Map("menu", (context) =>
{
    n--;
    context.Data["key"] = Guid.NewGuid();
});
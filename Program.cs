using StateMachine.Core;

var builder = new StateMachineBuilder();

var sm = builder.Build();

sm.Use((context) =>
{

});

DateTime start = DateTime.Now;
TimeSpan interval = new(0, 0, 3);
bool act = false;
int step = 10;
sm.Use((context) =>
{
    DateTime time = DateTime.Now;
    var text = context.CurrentValue;
    if (time - start >= interval)
    {
        act = true;
        context.SetState(GameStates.Game);
        start = time;
    }
    Console.WriteLine($"[{time.ToLongTimeString()}]: {text}");
    if (act)
    {
        step--;
        if (step == 0)
            sm.Stop();
    }
});

sm.Run(TimeSpan.FromMilliseconds(1 / 60));

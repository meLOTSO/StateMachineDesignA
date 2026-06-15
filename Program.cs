using System.Collections;
using System.IO.Pipes;
using System.Text;
using StateMachine.Core;

var builder = new StateMachineBuilder();

var sm = builder.Build();
sm.DeltaTime = TimeSpan.FromMilliseconds(1);
sm.Context.SetState("menu");
StringBuilder sb = new("");

sm.Map("menu", (context, _) =>
{
    Console.WriteLine("MENU");
    Console.WriteLine("Enter [Space] to start");

    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
    {
        context.SetState("load");
        sm.Data["start"] = DateTime.Now;
    }
});

string points = "";
TimeSpan sp = TimeSpan.FromSeconds(3);
sm.Map("load", (context, _) =>
{
    if (sm.Data["start"] is DateTime d && DateTime.Now - d >= sp)
        context.SetState("game");
    Console.Clear();
    Console.WriteLine("Load" + points);
    if (points.Length == 3)
        points = "";
    else points += ".";
});

const int WIDTH = 20;
const int HEIGHT = 10;

char[] buffer = new char[WIDTH * HEIGHT];
for (int i = 0; i < buffer.Length; i++)
    buffer[i] = '.';

sm.Map("game", (context, _) =>
{
    Console.Clear();
    Console.WriteLine(buffer);
});

sm.Map("end", (context, _) =>
{
    if (sb.Length == 0)
    {
        context.SetState("menu");
        return;
    }
    sb.Remove(0, 1);
    Console.WriteLine("{0}{1}", sb.ToString(), context.CurrentState);
});

sm.Run();
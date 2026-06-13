namespace StateMachine.Abstracts;

public interface IMutable<TMutableData> where TMutableData : notnull
{
    public void Mutate(Action<TMutableData> action);
}
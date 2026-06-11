public abstract class MachineAction<TState> where TState : notnull
{
    protected StateChanger<TState> changer = null!;

    public abstract void Invoke();
}

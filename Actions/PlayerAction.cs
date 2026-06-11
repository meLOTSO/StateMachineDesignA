public class PlayerAction : MachineAction<GameStates>
{
    public override void Invoke()
    {
        changer.Invoke(GameStates.Game);
    }
}

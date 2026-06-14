namespace StateMachine.Extensions;

public static class StateMachineExtensions
{
    public static void RemoveEmptyStates(this StateMachine.Abstracts.IMutableStateMachineContext mutableContext)
    {
        mutableContext.Mutate((map) =>
        {
            var emptyStates = map
                .Where(kvp => kvp.Value.Values.Count == 0 && kvp.Value.Metadata.Count == 0)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var state in emptyStates)
                map.Remove(state);
        });
    }
}
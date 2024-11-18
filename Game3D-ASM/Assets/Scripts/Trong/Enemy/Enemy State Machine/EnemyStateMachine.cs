public class EnemyStateMachine
{
    public EnemyState currentState { get; set; }
    public void Initialize(EnemyState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public void SwitchStage(EnemyState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
}

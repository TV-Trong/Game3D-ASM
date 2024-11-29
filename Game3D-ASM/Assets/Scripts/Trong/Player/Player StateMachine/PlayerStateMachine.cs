
public class PlayerStateMachine
{
    public PlayerState currentState {  get; set; }
    public void Initialize(PlayerState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void SwitchState(PlayerState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}

using BladesOfDeceptionCapstoneProject;

public class StateMachine
{
    public PlayerState currentState;

    // Initialize the state machine with a starting state
    public void Initialize(PlayerState startingState, Character character)
    {
        currentState = startingState;
        startingState.EnterState(character);
    }

    // Change to a new state
    public void ChangeState(PlayerState newState, Character character)
    {
        currentState.ExitState(character);
        currentState = newState;
        newState.EnterState(character);
    }
}
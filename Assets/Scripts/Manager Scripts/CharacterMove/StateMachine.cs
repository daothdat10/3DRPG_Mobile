public class StateMachine
{

    public State currentState;


    public void Intialize(State startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }
    public void ChangeState(State newState)
    {
        currentState.Exit();

        currentState = newState;

        newState.Enter();
    }
}
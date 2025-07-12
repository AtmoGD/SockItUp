using UnityEngine;

public class StateMaschine : MonoBehaviour
{
    protected State currentState;

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    protected void Update()
    {
        if (currentState != null)
        {
            currentState.FrameUpdate();
        }
    }

    protected void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.PhysicsUpdate();
        }
    }
}

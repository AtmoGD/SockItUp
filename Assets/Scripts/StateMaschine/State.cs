using UnityEngine;

public class State
{
    protected StateMaschine stateMachine;
    // Called when the state is entered
    public virtual void Enter(StateMaschine stateMachine)
    {
        // Override this method in derived classes to implement state-specific behavior
        this.stateMachine = stateMachine;
    }

    // Called every frame while the state is active
    public virtual void FrameUpdate()
    {
        // Override this method in derived classes to implement state-specific behavior
    }

    public virtual void PhysicsUpdate()
    {
        // Override this method in derived classes to implement state-specific behavior
    }

    // Called when the state is exited
    public virtual void Exit()
    {
        // Override this method in derived classes to implement state-specific behavior
    }

    public virtual void CheckState()
    {
        // Override this method in derived classes to implement state-specific behavior
        // This method can be used to check conditions and transition to other states
    }
}

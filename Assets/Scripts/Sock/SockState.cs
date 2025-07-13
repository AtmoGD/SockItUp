using UnityEngine;

public class SockState
{
    protected Sock sock;
    protected float timeInState;

    public SockState(Sock _sock)
    {
        sock = _sock;
    }

    public virtual void Enter()
    {
        timeInState = 0f;
        // Debug.Log($"Entering state: {this.GetType().Name}");
    }

    public virtual void Exit()
    {
        // Debug.Log($"Exiting state: {this.GetType().Name}");
    }

    public virtual void FrameUpdate()
    {
        timeInState += Time.deltaTime;
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void CheckState()
    {
        // Debug.Log($"BASE - Checking state: {this.GetType().Name}");
    }

    public virtual bool CanChangeStateTo(SockState newState)
    {
        return false;
    }
}

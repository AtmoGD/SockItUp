using UnityEngine;

public class SockState
{
    protected Sock sock;
    protected float timeInState;

    public virtual void Enter(Sock sock)
    {
        // base.Enter(sock);
        timeInState = 0f;
        Debug.Log($"Entering state: {this.GetType().Name}");
    }

    public virtual void Exit()
    {
        // base.Exit();
        sock = null;
        Debug.Log($"Exiting state: {this.GetType().Name}");
    }

    public virtual void FrameUpdate()
    {
        // base.FrameUpdate();
        timeInState += Time.deltaTime;
    }

    public virtual void PhysicsUpdate()
    {
        // base.PhysicsUpdate();
    }

    public virtual void CheckState()
    {
        // base.CheckState();
        Debug.Log($"BASE - Checking state: {this.GetType().Name}");
    }

    public virtual bool CanChangeStateTo(SockState newState)
    {
        return false;
    }
}

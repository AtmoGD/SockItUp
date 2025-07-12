using UnityEngine;

public class SockMove : SockState
{
    private Vector2 moveDirection;
    protected new virtual void Enter(Sock sock)
    {
        base.Enter(sock);
    }

    protected new virtual void Exit()
    {
        base.Exit();
    }

    protected new virtual void FrameUpdate()
    {
        base.FrameUpdate();
    }

    protected new virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected new virtual void CheckState()
    {
        base.CheckState();
    }

    public override bool CanChangeStateTo(SockState newState)
    {
        return false;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
}

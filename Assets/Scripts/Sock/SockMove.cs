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

        Move();
    }

    protected new virtual void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected new virtual void CheckState()
    {
        base.CheckState();

        if (!sock.IsGrounded)
        {
            sock.ChangeState(sock.SockFall);
        }

        if (sock.IsOnWall)
        {
            sock.ChangeState(sock.SockIdle);
        }
    }

    public override bool CanChangeStateTo(SockState newState)
    {
        return false;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void Move()
    {
        if (sock == null || sock.Rb == null)
            return;

        Vector3 moveVector = new Vector3(moveDirection.x, 0, 0);
        float acceleration = sock.MoveCurve.Evaluate(timeInState);
        sock.Rb.linearVelocity = moveVector * acceleration;
    }
}

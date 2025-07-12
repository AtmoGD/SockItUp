using UnityEngine;

public class SockJump : SockState
{
    public SockJump(Sock _sock) : base(_sock)
    {
    }

    public override void Enter(Sock sock)
    {
        base.Enter(sock);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Jump();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (sock.IsGrounded && sock.Rb.linearVelocity.y <= 0)
        {
            sock.ChangeState(sock.SockIdle);
            return;
        }

        if (timeInState >= sock.JumpTime)
        {
            sock.ChangeState(sock.SockFall);
            return;
        }
    }

    public override bool CanChangeStateTo(SockState newState)
    {
        if (sock.IsGrounded)
        {
            if (newState is SockMove || newState is SockIdle)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (newState is SockFall)
            {
                return true;
            }
        }

        return false;
    }

    private void Jump()
    {
        float currentJumpVelocity = sock.JumpCurve.Evaluate(timeInState) * sock.JumpForce;
        sock.Rb.linearVelocity = new Vector2(sock.Rb.linearVelocity.x, currentJumpVelocity);
    }
}

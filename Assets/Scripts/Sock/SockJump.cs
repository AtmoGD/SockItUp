using UnityEngine;

public class SockJump : SockState
{
    private float currentMofier = 1f;
    public SockJump(Sock _sock) : base(_sock)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sock.Anim.SetBool("Jump", true);

        sock.Rb.excludeLayers = sock.ExcludeLayersForJump;

        currentMofier = sock.JumpModifier; // Store the current jump modifier
        sock.SetJumpModifier(1f); // Apply the jump modifier
    }

    public override void Exit()
    {
        base.Exit();

        sock.Anim.SetBool("Jump", false);
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
        float currentJumpVelocity = sock.JumpCurve.Evaluate(timeInState) * sock.JumpForce * currentMofier;
        sock.Rb.linearVelocity = new Vector2(sock.Rb.linearVelocity.x, currentJumpVelocity);
    }
}

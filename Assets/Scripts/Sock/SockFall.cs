using Unity.VisualScripting;
using UnityEngine;

public class SockFall : SockState
{
    public SockFall(Sock _sock) : base(_sock)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sock.Anim.SetBool("Fall", true);
    }

    public override void Exit()
    {
        base.Exit();

        sock.Anim.SetBool("Fall", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (sock.Rb.linearVelocity.y <= 0)
        {
            sock.Rb.excludeLayers = new LayerMask(); // Clear excluded layers when not falling
        }
        else
        {
            sock.Rb.excludeLayers = sock.ExcludeLayersForJump;
        }
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
    }
}

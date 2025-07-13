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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (sock.IsGrounded)
        {
            sock.ChangeState(sock.SockIdle);
            return;
        }
    }
}

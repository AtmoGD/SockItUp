using Unity.VisualScripting;
using UnityEngine;

public class SockFall : SockState
{
    public SockFall(Sock _sock) : base(_sock)
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

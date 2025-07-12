using UnityEngine;

public class SockFall : SockState
{
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

        if (sock.IsGrounded)
        {
            sock.ChangeState(sock.SockIdle);
        }
    }
}

using UnityEngine;

public class SockIdle : SockState
{
    public SockIdle(Sock _sock) : base(_sock)
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

        sock.Rb.linearVelocity = Vector3.zero;
    }

    public override void CheckState()
    {
        if (Game.Manager.CurrentState != GameState.Playing)
            return;

        base.CheckState();

        if (!sock.IsGrounded)
        {
            sock.ChangeState(sock.SockFall);
            return;
        }
    }

    public override bool CanChangeStateTo(SockState newState)
    {
        return newState is SockMove || newState is SockJump;
    }
}

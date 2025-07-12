using UnityEngine;

public class SockIdle : SockState
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

        sock.Rb.linearVelocity = Vector3.zero;
    }

    protected new virtual void CheckState()
    {
        if (Game.Manager.CurrentState != GameState.Playing)
            return;


        Debug.Log($"IsOnWall: {sock.IsOnWall}, IsGrounded: {sock.IsGrounded}");

        if (!sock.IsGrounded)
        {
            sock.ChangeState(sock.SockFall);
        }
        base.CheckState();
    }
}

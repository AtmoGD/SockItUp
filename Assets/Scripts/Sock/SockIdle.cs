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
        Debug.Log($"Checking state: {this.GetType().Name}");

        if (Game.Manager.CurrentState != GameState.Playing)
            return;


        Debug.Log($"Sock is in idle state: {this.GetType().Name}");

        base.CheckState();

        // if (!sock.Character.isGrounded)
        // {
        //     sock.ChangeState(sock.SockFall);
        // }
    }
}

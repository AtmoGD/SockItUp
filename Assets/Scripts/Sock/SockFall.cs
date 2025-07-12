using UnityEngine;

public class SockFall : SockState
{
    private float currentFallVelocity = 0f;
    protected new virtual void Enter(Sock sock)
    {
        base.Enter(sock);
        currentFallVelocity = 0f;
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

        Fall();
    }

    protected new virtual void CheckState()
    {
        base.CheckState();

        // if (sock.Character.isGrounded)
        // {
        //     sock.ChangeState(sock.SockIdle);
        // }
    }

    private void Fall()
    {
        // Apply gravity to the sock
        currentFallVelocity = sock.FallCurve.Evaluate(timeInState);
        sock.transform.position += new Vector3(0, currentFallVelocity, 0);
    }
}

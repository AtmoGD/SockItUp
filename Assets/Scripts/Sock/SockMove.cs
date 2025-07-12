using UnityEngine;
using UnityEngine.UIElements;

public class SockMove : SockState
{
    private Vector2 moveDirection = Vector2.zero;

    public SockMove(Sock _sock) : base(_sock)
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

        Move();

        Rotate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void CheckState()
    {
        base.CheckState();

        if (!sock.IsGrounded)
        {
            sock.ChangeState(sock.SockFall);
            return;
        }

        if (sock.IsOnWall && IsFacingRightDirection())
        {
            sock.ChangeState(sock.SockIdle);
            return;
        }
    }

    public override bool CanChangeStateTo(SockState newState)
    {
        return false;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    private bool IsFacingRightDirection()
    {
        if (moveDirection.x < 0 && sock.transform.eulerAngles.y > 170 && sock.transform.eulerAngles.y < 190)
        {
            return true;
        }
        if (moveDirection.x > 0 && (sock.transform.eulerAngles.y < 10 || sock.transform.eulerAngles.y > 350))
        {
            return true;
        }

        return false;
    }

    private void Move()
    {
        if (sock == null || sock.Rb == null)
            return;

        Vector3 moveVector = new Vector3(moveDirection.x, 0, 0);
        float acceleration = sock.MoveCurve.Evaluate(timeInState);
        sock.Rb.linearVelocity = acceleration * sock.MoveSpeed * moveVector;
    }

    private void Rotate()
    {
        if (sock == null || sock.Rb == null || moveDirection == Vector2.zero)
            return;

        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        sock.transform.rotation = Quaternion.Slerp(sock.transform.rotation, targetRotation, sock.TurnSpeed * Time.deltaTime);
    }
}

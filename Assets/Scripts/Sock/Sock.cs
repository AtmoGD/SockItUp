using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sock : MonoBehaviour
{
    public SockIdle SockIdle { get; private set; } = new SockIdle();
    public SockMove SockMove { get; private set; } = new SockMove();
    public SockJump SockJump { get; private set; } = new SockJump();
    public SockFall SockFall { get; private set; } = new SockFall();

    [field: SerializeReference] public AnimationCurve MoveCurve { get; private set; } = AnimationCurve.Linear(0, 0, 1, 1);
    [field: SerializeReference] public float TurnSpeed { get; private set; } = 2f;
    [field: SerializeReference] public AnimationCurve JumpCurve { get; private set; } = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [field: SerializeReference] public AnimationCurve FallCurve { get; private set; } = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [field: SerializeReference] public Transform GroundCheckTransform { get; private set; }
    [field: SerializeReference] public LayerMask GroundLayer { get; private set; }
    [field: SerializeReference] public float GroundCheckRadius { get; private set; } = 0.1f;
    [field: SerializeReference] public Transform WallCheckTransform { get; private set; }
    [field: SerializeReference] public LayerMask WallLayer { get; private set; }
    [field: SerializeReference] public float WallCheckRadius { get; private set; } = 0.1f;

    public bool IsGrounded
    {
        get
        {
            if (GroundCheckTransform == null)
                return false;

            return Physics.CheckSphere(GroundCheckTransform.position, GroundCheckRadius, GroundLayer);
        }
    }

    public Rigidbody Rb { get; private set; } = null;
    protected SockState currentState;
    protected SockState targetedNextState;

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }


    public void ChangeState(SockState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.FrameUpdate();
        }

        if (targetedNextState != null && currentState.CanChangeStateTo(targetedNextState))
        {
            ChangeState(targetedNextState);
            targetedNextState = null;
        }

        if (currentState != null)
        {
            currentState.CheckState();
        }
    }

    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.PhysicsUpdate();
        }
    }

    public void MoveCharacter(Vector2 direction)
    {
        if (!currentState.CanChangeStateTo(SockMove) || Game.Manager.CurrentState != GameState.Playing)
            return;

        ChangeState(SockMove);
        SockMove.SetDirection(direction);
    }

    private void OnDrawGizmos()
    {
        if (GroundCheckTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GroundCheckTransform.position, GroundCheckRadius);
        }

        if (WallCheckTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(WallCheckTransform.position, WallCheckRadius);
        }
    }
}
